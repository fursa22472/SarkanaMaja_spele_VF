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

        if (!playedFirstDialogue)
        {
            story = new Story(inkJSONAsset.text);
            playedFirstDialogue = true;
        }
        else
        {
            story = new Story(inkJSONAsset2.text);
        }

        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        if (cameraTransition != null && cameraTargetPosition != null)
        {
            cameraTransition.MoveToTarget(cameraTargetPosition);
        }

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
        {
            canvas.gameObject.SetActive(true);
        }

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
    
        dialogueAudioMap.Add("Pianiste_7_N_00", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_N_00"));
        dialogueAudioMap.Add("Pianiste_7_N_01", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_N_01"));
        dialogueAudioMap.Add("Pianiste_7_N_02", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_N_02"));
        dialogueAudioMap.Add("Pianiste_7_N_03", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_N_03"));
        dialogueAudioMap.Add("Pianiste_7_N_04", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_N_04"));
        dialogueAudioMap.Add("Pianiste_7_N_05", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_N_05"));
        dialogueAudioMap.Add("Pianiste_7_N_06", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_N_06"));
        dialogueAudioMap.Add("Pianiste_7_N_07", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_N_07"));
        dialogueAudioMap.Add("Pianiste_7_N_08", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_N_08"));
        dialogueAudioMap.Add("Pianiste_7_N_09", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_N_09"));
        dialogueAudioMap.Add("Pianiste_7_N_10", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_N_10"));
        dialogueAudioMap.Add("Pianiste_7_N_11", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_N_11"));
        dialogueAudioMap.Add("Pianiste_7_N_12", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_N_12"));
        dialogueAudioMap.Add("Pianiste_7_N_13", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_N_13"));
        dialogueAudioMap.Add("Pianiste_7_N_14", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_N_14"));
        dialogueAudioMap.Add("Pianiste_7_N_15", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_N_15"));
        dialogueAudioMap.Add("Pianiste_7_N_16", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_N_16"));
        dialogueAudioMap.Add("Pianiste_7_N_17", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_N_17"));
        dialogueAudioMap.Add("Pianiste_7_N_18", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_N_18"));
        dialogueAudioMap.Add("Pianiste_7_N_19", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_N_19"));
        dialogueAudioMap.Add("Pianiste_7_N_20", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_N_20"));
        dialogueAudioMap.Add("Pianiste_7_N_21", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_N_21"));
        dialogueAudioMap.Add("Pianiste_7_N_22", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_N_22"));
        dialogueAudioMap.Add("Pianiste_7_N_23", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_N_23"));
        dialogueAudioMap.Add("Pianiste_7_N_24", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_N_24"));
        dialogueAudioMap.Add("Pianiste_7_N_25", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_N_25"));
        dialogueAudioMap.Add("Pianiste_7_N_26", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_N_26"));

        dialogueAudioMap.Add("Pianiste_7_Neg_01", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_Neg_01"));
        dialogueAudioMap.Add("Pianiste_7_Neg_02", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_Neg_02"));

        dialogueAudioMap.Add("Pianiste_7_GO_00", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_GO_00"));
        dialogueAudioMap.Add("Pianiste_7_GO_01", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_GO_01"));
        dialogueAudioMap.Add("Pianiste_7_GO_02", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_GO_02"));
        dialogueAudioMap.Add("Pianiste_7_GO_03", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_GO_03"));
        dialogueAudioMap.Add("Pianiste_7_GO_04", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_GO_04"));
        dialogueAudioMap.Add("Pianiste_7_GO_05", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_GO_05"));
        dialogueAudioMap.Add("Pianiste_7_GO_06", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_GO_06"));
        dialogueAudioMap.Add("Pianiste_7_GO_07", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_GO_07"));
        dialogueAudioMap.Add("Pianiste_7_GO_08", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_GO_08"));

        dialogueAudioMap.Add("Pianiste_7_Cimds_00", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_Cimds_00"));
        dialogueAudioMap.Add("Pianiste_7_Cimds_01", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_Cimds_01"));
        dialogueAudioMap.Add("Pianiste_7_Cimds_02", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_Cimds_02"));

        dialogueAudioMap.Add("Pianiste_7_Ziedi_00", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_Ziedi_00"));
        dialogueAudioMap.Add("Pianiste_7_Ziedi_01", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_Ziedi_01"));
        dialogueAudioMap.Add("Pianiste_7_Ziedi_02", Resources.Load<AudioClip>("Audio/PianisteAudio/Pianiste_7_Ziedi_02"));

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
