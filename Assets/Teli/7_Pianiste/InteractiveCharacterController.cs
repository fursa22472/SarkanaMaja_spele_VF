using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

public class InteractiveCharacterController : MonoBehaviour

{

public static event Action<GameObject> OnDialogueEnd;


    [Header("Animation Settings")]
    public Animator animator;
    public string idleAnimation = "Idle";
    public string standUpAnimation = "StandUp";
    public string talkAnimation = "Talk";

    [Header("Audio Settings")]
    public AudioSource dialogueAudioSource;
    public Dictionary<string, AudioClip> dialogueAudioMap;
    private AudioClip currentDialogueClip;

    [Header("Dialogue Settings")]
    public TextAsset inkJSONAsset;
    public TextAsset inkJSONAsset2;
    public Canvas canvas;
    public Text textPrefab;
    public Button buttonPrefab;
    public Transform cameraTargetPosition;

    [Header("Player Settings")]
    public Transform playerTransform;
    public CharacterMovement2 playerMovement;

    

    [Header("Ambient Audio")]
public AudioSource ambientAudioSource;
public float ambientMaxVolume = 1f;
public float ambientFadeSpeed = 2f;
public float ambientMaxDistance = 10f; //sis mergis atbild par to piano skanas fade in and out kad pieet tuvak (nonemt ja viss slikti)


    private Story story;
    private CameraTransition cameraTransition;
    private bool isDialogueActive = false;
    private bool playedFirstDialogue = false;
    private bool playerInRange = false;
    private int selectedChoiceIndex = 0;
    private List<Button> choiceButtons = new List<Button>();

    private void Start()
    {
        cameraTransition = Camera.main.GetComponent<CameraTransition>();

        animator.applyRootMotion = false;


        if (canvas != null)
        {
            canvas.gameObject.SetActive(false);
        }

        InitializeDialogueAudioMap();
    }

    private void Update()
    {
        if (playerInRange && !isDialogueActive && Input.GetKeyDown(KeyCode.E))
        {
            StartDialogue();
        }
        else if (isDialogueActive)
        {
            HandleInput();
        }

        


//sis mergis atbild par to piano skanas fade in and out kad pieet tuvak(nonemt ja viss slikti)
        if (playerInRange && !isDialogueActive)
{
    if (!ambientAudioSource.isPlaying)
    {
        ambientAudioSource.Play();
    }

    float distance = Vector3.Distance(playerTransform.position, transform.position);
    float targetVolume = Mathf.Clamp01(1f - (distance / ambientMaxDistance)) * ambientMaxVolume;
    ambientAudioSource.volume = Mathf.MoveTowards(ambientAudioSource.volume, targetVolume, ambientFadeSpeed * Time.deltaTime);
}
else if (ambientAudioSource.isPlaying && (isDialogueActive || !playerInRange))
{
    ambientAudioSource.volume = Mathf.MoveTowards(ambientAudioSource.volume, 0f, ambientFadeSpeed * Time.deltaTime);
    if (ambientAudioSource.volume <= 0.01f)
    {
        ambientAudioSource.Stop();
    }
    
//beidzas mergis
}


    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

public void StartDialogue()
{
    isDialogueActive = true;

    // Load the correct JSON asset (first or second)
    TextAsset selectedAsset = !playedFirstDialogue ? inkJSONAsset : inkJSONAsset2;

    // Try to find AudioInkFlagSetter
    var flagSetter = GetComponent<AudioInkFlagSetter>();
    string jsonPath = "";

    if (flagSetter != null && flagSetter.assignedJsonFile == selectedAsset)
    {
        jsonPath = flagSetter.GetRuntimeJsonPath();
    }
    else
    {
        // Fallback if AudioInkFlagSetter not found
        string filename = selectedAsset.name + "_runtime.json";
        jsonPath = System.IO.Path.Combine(Application.persistentDataPath, filename);

        // Create the runtime file if it doesn’t exist
        if (!System.IO.File.Exists(jsonPath))
        {
            System.IO.File.WriteAllText(jsonPath, selectedAsset.text);
            Debug.Log("📄 Created runtime JSON for: " + jsonPath);
        }
    }

    if (System.IO.File.Exists(jsonPath))
    {
        string json = System.IO.File.ReadAllText(jsonPath);
        story = new Story(json); // ✅ LOADING FROM UPDATED JSON!
        Debug.Log("✅ Loaded Ink story from runtime JSON: " + jsonPath);
        Debug.Log($"🤖 PiekritiPianistei = {story.variablesState["PiekritiPianistei"]}");
    }
    else
    {
        Debug.LogError("❌ Runtime Ink JSON not found: " + jsonPath);
        return;
    }

    playedFirstDialogue = true;

    if (playerMovement != null)
        playerMovement.enabled = false;

    if (cameraTransition != null && cameraTargetPosition != null)
        cameraTransition.MoveToTarget(cameraTargetPosition);

    if (animator != null)
    {
        animator.ResetTrigger(idleAnimation);
        animator.SetTrigger(standUpAnimation);
        StartCoroutine(WaitForAnimation(standUpAnimation, () =>
        {
            animator.SetTrigger(talkAnimation);
        }));
    }

    if (canvas != null)
        canvas.gameObject.SetActive(true);

    RefreshView();
}


    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            NavigateChoices(1);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            NavigateChoices(-1);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if (story.currentChoices.Count > 0)
            {
                // Select the currently highlighted choice when pressing E
                OnClickChoiceButton(story.currentChoices[selectedChoiceIndex]);
            }
            else
            {
                EndDialogue();
            }
        }
    }

    void NavigateChoices(int direction)
    {
        if (choiceButtons.Count == 0) return;

        selectedChoiceIndex = (selectedChoiceIndex + direction + choiceButtons.Count) % choiceButtons.Count;
        HighlightChoiceButton();
    }

    void HighlightChoiceButton()
    {
        for (int i = 0; i < choiceButtons.Count; i++)
        {
            var colors = choiceButtons[i].colors;
            colors.normalColor = (i == selectedChoiceIndex) ? Color.yellow : Color.white;
            choiceButtons[i].colors = colors;
        }
    }

    void OnClickChoiceButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        RefreshView();
    }

    void RefreshView()
    {
        RemoveChildren();
        choiceButtons.Clear();
        selectedChoiceIndex = 0;

        while (story.canContinue)
        {
            string text = story.Continue().Trim();
            CreateContentView(text);
            PlayDialogueAudioFromTags(story.currentTags);
        }

        if (story.currentChoices.Count > 0)
        {
            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                Choice choice = story.currentChoices[i];
                Button button = CreateChoiceView(choice.text.Trim());

                choiceButtons.Add(button);

                int choiceIndex = i;
                button.onClick.AddListener(() => OnClickChoiceButton(story.currentChoices[choiceIndex]));
            }

            HighlightChoiceButton();
        }
        else
        {
            Button choice = CreateChoiceView("Close");
            choice.onClick.AddListener(EndDialogue);
            choiceButtons.Add(choice);
            HighlightChoiceButton();
        }
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        RemoveChildren();

        if (animator != null)
        {
            animator.ResetTrigger(standUpAnimation);
            animator.ResetTrigger(talkAnimation);
            animator.SetTrigger(idleAnimation);
        }

        if (cameraTransition != null)
        {
            cameraTransition.ResetCamera();
        }

        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        if (canvas != null)
        {
            canvas.gameObject.SetActive(false);
        }



        
    // ✅ Pass the character that was spoken to
    OnDialogueEnd?.Invoke(gameObject);
    }

    void RemoveChildren()
    {
        for (int i = canvas.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(canvas.transform.GetChild(i).gameObject);
        }
    }

    Text CreateContentView(string text)
    {
        Text storyText = Instantiate(textPrefab);
        storyText.text = text;
        storyText.transform.SetParent(canvas.transform, false);
        return storyText;
    }

    Button CreateChoiceView(string text)
    {
        Button choice = Instantiate(buttonPrefab);
        choice.transform.SetParent(canvas.transform, false);

        Text choiceText = choice.GetComponentInChildren<Text>();
        choiceText.text = text;

        return choice;
    }

    void InitializeDialogueAudioMap()
    {
        dialogueAudioMap = new Dictionary<string, AudioClip>();

        // Example mappings
    
        dialogueAudioMap.Add("Pianiste_7_01", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_01"));
        dialogueAudioMap.Add("Pianiste_7_02", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_02"));
        dialogueAudioMap.Add("Pianiste_7_03", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_03"));
        dialogueAudioMap.Add("Pianiste_7_04", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_04"));
        dialogueAudioMap.Add("Pianiste_7_05", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_05"));
        dialogueAudioMap.Add("Pianiste_7_06", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_06"));
        dialogueAudioMap.Add("Pianiste_7_07", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_07"));
        dialogueAudioMap.Add("Pianiste_7_08", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_08"));
        dialogueAudioMap.Add("Pianiste_7_09", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_09"));
        dialogueAudioMap.Add("Pianiste_7_10", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_10"));
        dialogueAudioMap.Add("Pianiste_7_11", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_11"));
        dialogueAudioMap.Add("Pianiste_7_12", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_12"));
        dialogueAudioMap.Add("Pianiste_7_13", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_13"));
        dialogueAudioMap.Add("Pianiste_7_14", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_14"));
        dialogueAudioMap.Add("Pianiste_7_15_beigas", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_15_beigas"));
        dialogueAudioMap.Add("Pianiste_7_16", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_16"));
        dialogueAudioMap.Add("Pianiste_7_17", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_17"));
        dialogueAudioMap.Add("Pianiste_7_18", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_18"));
        dialogueAudioMap.Add("Pianiste_7_19", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_19"));
        dialogueAudioMap.Add("Pianiste_7_20", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_20"));
        dialogueAudioMap.Add("Pianiste_7_21", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_21"));
        dialogueAudioMap.Add("Pianiste_7_22", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_22"));
        dialogueAudioMap.Add("Pianiste_7_23_V2", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_23_V2"));
        dialogueAudioMap.Add("Pianiste_7_24", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_24"));
        dialogueAudioMap.Add("Pianiste_7_25_V2", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_25_V2"));
        dialogueAudioMap.Add("Pianiste_7_26", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_26"));
        dialogueAudioMap.Add("Pianiste_7_27", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_27"));
        dialogueAudioMap.Add("Pianiste_7_28", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_28"));
        dialogueAudioMap.Add("Pianiste_7_29", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_29"));
        dialogueAudioMap.Add("Pianiste_7_30", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_30"));
        dialogueAudioMap.Add("Pianiste_7_31", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_31"));
        dialogueAudioMap.Add("Pianiste_7_32_Piekritibeigas", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_32_Piekritibeigas"));
        dialogueAudioMap.Add("Pianiste_7_33_Nepiekritibeigas", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_33_Nepiekritibeigas"));
        dialogueAudioMap.Add("Pianiste_7_beigas2", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_beigas2"));

        dialogueAudioMap.Add("Pianiste_7_OBJ_Cimd_1", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_OBJ_Cimd_1"));
        dialogueAudioMap.Add("Pianiste_7_OBJ_Cimd_2", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_OBJ_Cimd_2"));
        dialogueAudioMap.Add("Pianiste_7_OBJ_Cimd_3", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_OBJ_Cimd_3"));

        dialogueAudioMap.Add("Pianiste_7_OBJ_Zied_1", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_OBJ_Zied_1"));
        dialogueAudioMap.Add("Pianiste_7_OBJ_Zied_2", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_OBJ_Zied_2"));
        dialogueAudioMap.Add("Pianiste_7_OBJ_Zied_3", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_OBJ_Zied_3"));

        dialogueAudioMap.Add("humming", Resources.Load<AudioClip>("Audio/PianisteAudio/humming"));



    }

    void PlayDialogueAudioFromTags(List<string> tags)
    {
        foreach (string tag in tags)
        {
            if (tag.StartsWith("audio:"))
            {
                string audioTag = tag.Substring(6);
                PlayDialogueAudio(audioTag);
            }
        }
    }

    void PlayDialogueAudio(string audioTag)
    {
        if (dialogueAudioSource != null && dialogueAudioMap.TryGetValue(audioTag, out AudioClip clip))
        {
            if (currentDialogueClip != clip)
            {
                dialogueAudioSource.clip = clip;
                dialogueAudioSource.Play();
                currentDialogueClip = clip;
            }
        }
        else
        {
            Debug.LogError("Audio tag not found or audio source is null: " + audioTag);
        }
    }

    IEnumerator WaitForAnimation(string animationName, Action onComplete)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        while (stateInfo.IsName(animationName) && stateInfo.normalizedTime < 1.0f)
        {
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            yield return null;
        }

        onComplete?.Invoke();
    }
}
