using System;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class InkDialogOnClickIND : MonoBehaviour
{
    public static event Action<Story> OnCreateStory;
    public static event Action<GameObject> OnDialogueEnd;



    [SerializeField] private TextAsset inkJSONAsset1 = null;  // First JSON file
    [SerializeField] private TextAsset inkJSONAsset2 = null;  // Second JSON file
    [SerializeField] private Canvas canvas = null;
    [SerializeField] private Text textPrefab = null;
    [SerializeField] private Button buttonPrefab = null;
    [SerializeField] private Transform cameraTargetPosition; // Target position for the camera
    [SerializeField] private Animator characterAnimator; // Animator for the character
    [SerializeField] private string animationTrigger; // Animation trigger name
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private InteractiveCharacter interactiveCharacter; // Reference to the InteractiveCharacter script

    private Dictionary<string, AudioClip> dialogueAudioMap;
    private Story story;
    private CharacterMovement2 characterMovement; // Reference to the player's movement script
    private CameraTransition cameraTransition; // Reference to the camera transition script
    private bool isFirstStory = true; // Flag to toggle between first and second JSON files
   [HideInInspector] public bool playerInRange = false; // Track if the player is within the collider
    private int selectedChoiceIndex = 0; // Track the currently selected choice
    private List<Button> choiceButtons = new List<Button>();
   [HideInInspector] public bool isDialogueActive = false; // Track whether dialogue is active

    void Awake()
    {
        RemoveChildren();
        InitializeDialogueAudioMap();
    }

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            characterMovement = player.GetComponent<CharacterMovement2>();
        }

        GameObject mainCamera = Camera.main.gameObject;
        if (mainCamera != null)
        {
            cameraTransition = mainCamera.GetComponent<CameraTransition>();
        }
    }

    void Update()
    {
        if (playerInRange && !isDialogueActive && Input.GetKeyDown(KeyCode.E))
        {
            StartStoryOnClick();
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

public void StartStoryOnClick()
{
if (interactiveCharacter != null)
    interactiveCharacter.SetDialogueActive(true);

if (characterMovement != null)
{
    characterMovement.isDialogueMode = true; // ✅ Dialogue mode on
    // characterMovement.enabled = false; ❌ REMOVE THIS
}

if (characterAnimator != null)
{
    characterAnimator.SetFloat("Speed", 0f);
    characterAnimator.SetBool("InDialogue", true);
    characterAnimator.CrossFade("Standing Idle", 0f);
}





    if (cameraTransition != null && cameraTargetPosition != null)
        cameraTransition.MoveToTarget(cameraTargetPosition);

    if (characterAnimator != null && !string.IsNullOrEmpty(animationTrigger))
        characterAnimator.SetTrigger(animationTrigger);

    // Decide which JSON to use (first or second)
    TextAsset selectedAsset = isFirstStory ? inkJSONAsset1 : inkJSONAsset2;

    if (selectedAsset == null)
    {
        Debug.LogError("❌ No Ink JSON assigned for the current state!");
        return;
    }

    // Try to get the AudioInkFlagSetter from the same GameObject
    var flagSetter = GetComponent<AudioInkFlagSetter>();
    string jsonPath = "";

    // If the AudioInkFlagSetter exists and its assigned JSON matches the selected asset, use its runtime file path
    if (flagSetter != null && flagSetter.assignedJsonFile == selectedAsset)
    {
        jsonPath = flagSetter.GetRuntimeJsonPath();
    }
    else
    {
        // Fallback: generate the runtime path directly from the selected asset
        string filename = selectedAsset.name + "_runtime.json";
        jsonPath = System.IO.Path.Combine(Application.persistentDataPath, filename);

        // Create the runtime file if it doesn’t exist
        if (!File.Exists(jsonPath))
        {
            File.WriteAllText(jsonPath, selectedAsset.text);
            Debug.Log("📄 Created runtime JSON for alternate file: " + jsonPath);
        }
    }

    // Load and initialize the Ink story from the runtime file
    if (File.Exists(jsonPath))
    {
        string json = File.ReadAllText(jsonPath);
        story = new Ink.Runtime.Story(json);
        Debug.Log("✅ Loaded Ink story from: " + jsonPath);
        Debug.Log("🤖 PiekritiPriesterim = " + story.variablesState["PiekritiPriesterim"]);
    }
    else
    {
        Debug.LogError("❌ Runtime Ink JSON not found: " + jsonPath);
        return;
    }

    OnCreateStory?.Invoke(story);
    isFirstStory = false; // Next time, it'll use the second file
    isDialogueActive = true;

    RefreshView();
}




    void RefreshView()
    {
        RemoveChildren();
        choiceButtons.Clear();
        selectedChoiceIndex = 0; // Always reset to the top choice

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
            }
            HighlightChoiceButton(); // Highlight the top choice
        }
        else
        {
            Button choice = CreateChoiceView("Aizvērt");
            choice.onClick.AddListener(delegate
            {
                EndDialogue();
            });
            choiceButtons.Add(choice);
            HighlightChoiceButton();
        }
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

void EndDialogue()
{
    isDialogueActive = false;
    RemoveChildren();

   

    if (cameraTransition != null)
    {
        cameraTransition.ResetCamera(); // Reset camera to default position
    }

    if (interactiveCharacter != null)
    {
        interactiveCharacter.SetDialogueActive(false);
    }

    // ✅ Pass the character that was spoken to
    OnDialogueEnd?.Invoke(gameObject);





    characterMovement.isDialogueMode = false;

}




    void CreateContentView(string text)
    {
        Text storyText = Instantiate(textPrefab) as Text;
        storyText.text = text;
        storyText.transform.SetParent(canvas.transform, false);
    }

    Button CreateChoiceView(string text)
    {
        Button choice = Instantiate(buttonPrefab) as Button;
        choice.transform.SetParent(canvas.transform, false);

        Text choiceText = choice.GetComponentInChildren<Text>();
        choiceText.text = text;

        HorizontalLayoutGroup layoutGroup = choice.GetComponent<HorizontalLayoutGroup>();
        if (layoutGroup != null)
        {
            layoutGroup.childForceExpandHeight = false;
        }

        return choice;
    }

    void RemoveChildren()
    {
        int childCount = canvas.transform.childCount;
        for (int i = childCount - 1; i >= 0; --i)
        {
            Destroy(canvas.transform.GetChild(i).gameObject);
        }
    }

    void InitializeDialogueAudioMap()
    {
        dialogueAudioMap = new Dictionary<string, AudioClip>();

        // Example mapping (replace with your actual mappings)
        
        dialogueAudioMap.Add("Rainis_1_00", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_00"));
        dialogueAudioMap.Add("Rainis_1_01", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_01"));
        dialogueAudioMap.Add("Rainis_1_02", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_02"));
        dialogueAudioMap.Add("Rainis_1_03", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_03"));
        dialogueAudioMap.Add("Rainis_1_04", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_04"));
        dialogueAudioMap.Add("Rainis_1_05", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_05"));
        dialogueAudioMap.Add("Rainis_1_06", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_06"));
        dialogueAudioMap.Add("Rainis_1_07", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_07"));
        dialogueAudioMap.Add("Rainis_1_08", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_08"));
        dialogueAudioMap.Add("Rainis_1_09", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_09"));
        dialogueAudioMap.Add("Rainis_1_10", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_10"));
        dialogueAudioMap.Add("Rainis_1_11", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_11"));
        dialogueAudioMap.Add("Rainis_1_12", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_12"));
        dialogueAudioMap.Add("Rainis_1_13", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_13"));
        dialogueAudioMap.Add("Rainis_1_14", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_14"));
        dialogueAudioMap.Add("Rainis_1_15", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_15"));
        dialogueAudioMap.Add("Rainis_1_16", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_16"));
        dialogueAudioMap.Add("Rainis_1_17", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_17"));
        dialogueAudioMap.Add("Rainis_1_18", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_18"));
        dialogueAudioMap.Add("Rainis_1_19", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_19"));
        dialogueAudioMap.Add("Rainis_1_20", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_20"));
        dialogueAudioMap.Add("Rainis_1_21", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_21"));
        dialogueAudioMap.Add("Rainis_1_22", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_22"));
        dialogueAudioMap.Add("Rainis_1_23", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_23"));
        dialogueAudioMap.Add("Rainis_1_24", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_24"));
        dialogueAudioMap.Add("Rainis_1_25", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_25"));
        dialogueAudioMap.Add("Rainis_1_26", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_26"));
        dialogueAudioMap.Add("Rainis_1_27", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_27"));
        dialogueAudioMap.Add("Rainis_1_28", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_28"));
        dialogueAudioMap.Add("Rainis_1_29", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_29"));
        dialogueAudioMap.Add("Rainis_1_30", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_30"));
        dialogueAudioMap.Add("Rainis_1_31", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_31"));
        dialogueAudioMap.Add("Rainis_1_32", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_32"));
        dialogueAudioMap.Add("Rainis_1_33_beigas", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_33_beigas"));

        dialogueAudioMap.Add("Rainis_1_OBJ_Cepums_1", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_OBJ_Cepums_1"));
        dialogueAudioMap.Add("Rainis_1_OBJ_Cepums_2", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_OBJ_Cepums_2"));

        dialogueAudioMap.Add("Rainis_1_Loop_1", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_Loop_1"));

       //priesteris

        dialogueAudioMap.Add("Priesteris_2_00", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_00"));
        dialogueAudioMap.Add("Priesteris_2_001", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_001"));
        dialogueAudioMap.Add("Priesteris_2_01", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_01"));
        dialogueAudioMap.Add("Priesteris_2_02_beigas1", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_02_beigas1"));
        dialogueAudioMap.Add("Priesteris_2_03", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_03"));
        dialogueAudioMap.Add("Priesteris_2_04", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_04"));
        dialogueAudioMap.Add("Priesteris_2_05", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_05"));
        dialogueAudioMap.Add("Priesteris_2_06", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_06"));
        dialogueAudioMap.Add("Priesteris_2_07", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_07"));
        dialogueAudioMap.Add("Priesteris_2_08", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_08"));
        dialogueAudioMap.Add("Priesteris_2_09", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_09"));
        dialogueAudioMap.Add("Priesteris_2_10", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_10"));
        dialogueAudioMap.Add("Priesteris_2_11", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_11"));
        dialogueAudioMap.Add("Priesteris_2_12", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_12"));
        dialogueAudioMap.Add("Priesteris_2_13", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_13"));
        dialogueAudioMap.Add("Priesteris_2_14", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_14"));
        dialogueAudioMap.Add("Priesteris_2_15", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_15"));
        dialogueAudioMap.Add("Priesteris_2_16", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_16"));
        dialogueAudioMap.Add("Priesteris_2_17", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_17"));
        dialogueAudioMap.Add("Priesteris_2_18", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_18"));
        dialogueAudioMap.Add("Priesteris_2_19", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_19"));
        dialogueAudioMap.Add("Priesteris_2_20", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_20"));
        dialogueAudioMap.Add("Priesteris_2_21", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_21"));
        dialogueAudioMap.Add("Priesteris_2_22", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_22"));
        dialogueAudioMap.Add("Priesteris_2_23", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_23"));
        dialogueAudioMap.Add("Priesteris_2_24", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_24"));
        dialogueAudioMap.Add("Priesteris_2_25", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_25"));
        dialogueAudioMap.Add("Priesteris_2_26", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_26"));
        dialogueAudioMap.Add("Priesteris_2_27_beigas", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_27_beigas"));

        dialogueAudioMap.Add("Priesteris_2_OBJ_PIEZ_1", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_OBJ_PIEZ_1"));
        dialogueAudioMap.Add("Priesteris_2_OBJ_PIEZ_2", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_OBJ_PIEZ_2"));
        dialogueAudioMap.Add("Priesteris_2_OBJ_PIEZ_3", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_OBJ_PIEZ_3"));

        dialogueAudioMap.Add("Priesteris_2_OBJ_KAK_1", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_OBJ_KAK_1"));
        dialogueAudioMap.Add("Priesteris_2_OBJ_KAK_2", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_OBJ_KAK_2"));
        dialogueAudioMap.Add("Priesteris_2_OBJ_KAK_3", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_OBJ_KAK_3"));
        dialogueAudioMap.Add("Priesteris_2_OBJ_KAK_4", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_OBJ_KAK_4"));
        dialogueAudioMap.Add("Priesteris_2_OBJ_KAK_5", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_OBJ_KAK_5"));
        dialogueAudioMap.Add("Priesteris_2_OBJ_KAK_6", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_OBJ_KAK_6"));

        dialogueAudioMap.Add("Priesteris_2_Loop_1", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_Loop_1"));

        

//bomzis 
    
        dialogueAudioMap.Add("Bomzis_3_00", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_00"));
        dialogueAudioMap.Add("Bomzis_3_01", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_01"));
        dialogueAudioMap.Add("Bomzis_3_02", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_02"));
        dialogueAudioMap.Add("Bomzis_3_03", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_03"));
        dialogueAudioMap.Add("Bomzis_3_04", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_04"));
        dialogueAudioMap.Add("Bomzis_3_04_V2", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_04_V2"));
        dialogueAudioMap.Add("Bomzis_3_05", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_05"));
        dialogueAudioMap.Add("Bomzis_3_06_beigas1", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_06_beigas1"));
        dialogueAudioMap.Add("Bomzis_3_07", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_07"));
        dialogueAudioMap.Add("Bomzis_3_08", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_08"));
        dialogueAudioMap.Add("Bomzis_3_09", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_09"));
        dialogueAudioMap.Add("Bomzis_3_09_V2", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_09_V2"));
        dialogueAudioMap.Add("Bomzis_3_10", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_10"));
        dialogueAudioMap.Add("Bomzis_3_11", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_11"));
        dialogueAudioMap.Add("Bomzis_3_12", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_12"));
        dialogueAudioMap.Add("Bomzis_3_13", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_13"));
        dialogueAudioMap.Add("Bomzis_3_13_V2", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_13_V2"));
        dialogueAudioMap.Add("Bomzis_3_14", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_14"));
        dialogueAudioMap.Add("Bomzis_3_15", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_15"));
        dialogueAudioMap.Add("Bomzis_3_16", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_16"));
        dialogueAudioMap.Add("Bomzis_3_17_beigas2", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_17_beigas2"));
        dialogueAudioMap.Add("Bomzis_3_18", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_18"));
        dialogueAudioMap.Add("Bomzis_3_19", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_19"));
        dialogueAudioMap.Add("Bomzis_3_20", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_20"));
        dialogueAudioMap.Add("Bomzis_3_21", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_21"));
        dialogueAudioMap.Add("Bomzis_3_22_Piekritibeigas", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_22_Piekritibeigas"));
        dialogueAudioMap.Add("Bomzis_3_22_Piekritibeigas_V2", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_22_Piekritibeigas_V2"));
        dialogueAudioMap.Add("Bomzis_3_23_Nepiekritibeigas", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_23_Nepiekritibeigas"));

        dialogueAudioMap.Add("Bomzis_3_OBJ_APL_1", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_OBJ_APL_1"));
        dialogueAudioMap.Add("Bomzis_3_OBJ_APL_2", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_OBJ_APL_2"));
        dialogueAudioMap.Add("Bomzis_3_OBJ_APL_3", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_OBJ_APL_3"));

        dialogueAudioMap.Add("Bomzis_3_Loop", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_Loop"));
        dialogueAudioMap.Add("Bomzis_3_Loop_V2", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_Loop_V2"));
    

//makslinieks

        dialogueAudioMap.Add("Makslinieks_4_00", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_00"));
        dialogueAudioMap.Add("Makslinieks_4_01", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_01"));
        dialogueAudioMap.Add("Makslinieks_4_02", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_02"));
        dialogueAudioMap.Add("Makslinieks_4_03", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_03"));
        dialogueAudioMap.Add("Makslinieks_4_04", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_04"));
        dialogueAudioMap.Add("Makslinieks_4_05", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_05"));
        dialogueAudioMap.Add("Makslinieks_4_06", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_06"));
        dialogueAudioMap.Add("Makslinieks_4_07", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_07"));
        dialogueAudioMap.Add("Makslinieks_4_08", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_08"));
        dialogueAudioMap.Add("Makslinieks_4_09", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_09"));
        dialogueAudioMap.Add("Makslinieks_4_10_beigas1", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_10_beigas1"));
        dialogueAudioMap.Add("Makslinieks_4_11", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_11"));
        dialogueAudioMap.Add("Makslinieks_4_12", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_12"));
        dialogueAudioMap.Add("Makslinieks_4_13", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_13"));
        dialogueAudioMap.Add("Makslinieks_4_14", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_14"));
        dialogueAudioMap.Add("Makslinieks_4_15", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_15"));
        dialogueAudioMap.Add("Makslinieks_4_16", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_16"));
        dialogueAudioMap.Add("Makslinieks_4_17", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_17"));
        dialogueAudioMap.Add("Makslinieks_4_18", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_18"));
        dialogueAudioMap.Add("Makslinieks_4_19", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_19"));
        dialogueAudioMap.Add("Makslinieks_4_20", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_20"));
        dialogueAudioMap.Add("Makslinieks_4_21", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_21"));
        dialogueAudioMap.Add("Makslinieks_4_22", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_22"));
        dialogueAudioMap.Add("Makslinieks_4_23", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_23"));
        dialogueAudioMap.Add("Makslinieks_4_24", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_24"));
        dialogueAudioMap.Add("Makslinieks_4_25", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_25"));
        dialogueAudioMap.Add("Makslinieks_4_26", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_26"));
        dialogueAudioMap.Add("Makslinieks_4_27", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_27"));
        dialogueAudioMap.Add("Makslinieks_4_28_Piekritibeigas", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_28_Piekritibeigas"));
        dialogueAudioMap.Add("Makslinieks_4_29_Nepiekritibeigas", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_29_Nepiekritibeigas"));

        dialogueAudioMap.Add("Makslinieks_4_OBJ_ZIMEJ_01", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_OBJ_ZIMEJ_01"));
        dialogueAudioMap.Add("Makslinieks_4_OBJ_ZIMEJ_02", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_OBJ_ZIMEJ_02"));
        dialogueAudioMap.Add("Makslinieks_4_OBJ_ZIMEJ_03", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_OBJ_ZIMEJ_03"));

        dialogueAudioMap.Add("Makslinieks_4_OBJ_ZIMUL_01", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_OBJ_ZIMUL_01"));
        dialogueAudioMap.Add("Makslinieks_4_OBJ_ZIMUL_02", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_OBJ_ZIMUL_02"));
        dialogueAudioMap.Add("Makslinieks_4_OBJ_ZIMUL_03", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_OBJ_ZIMUL_03"));
        dialogueAudioMap.Add("Makslinieks_4_OBJ_ZIMUL_04", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_OBJ_ZIMUL_04"));
        dialogueAudioMap.Add("Makslinieks_4_OBJ_ZIMUL_05", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_OBJ_ZIMUL_05"));
        dialogueAudioMap.Add("Makslinieks_4_OBJ_ZIMUL_06", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_OBJ_ZIMUL_06"));
        dialogueAudioMap.Add("Makslinieks_4_OBJ_ZIMUL_07", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_OBJ_ZIMUL_07"));
        dialogueAudioMap.Add("Makslinieks_4_OBJ_ZIMUL_08", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_OBJ_ZIMUL_08"));

        dialogueAudioMap.Add("Makslinieks_4_Loop_1", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_Loop_1"));
        dialogueAudioMap.Add("Makslinieks_4_Loop_2", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_Loop_2"));


//tante
        dialogueAudioMap.Add("Tante_5_beigas", Resources.Load<AudioClip>("Audio/TanteAudio/Tante_5_beigas"));
        dialogueAudioMap.Add("Tante_5_IedodCepumus", Resources.Load<AudioClip>("Audio/TanteAudio/Tante_5_IedodCepumus"));
        dialogueAudioMap.Add("Tante_5_Loop_01", Resources.Load<AudioClip>("Audio/TanteAudio/Tante_5_Loop_01"));
        dialogueAudioMap.Add("Tante_5_Loop_02", Resources.Load<AudioClip>("Audio/TanteAudio/Tante_5_Loop_02"));
        dialogueAudioMap.Add("Tante_5_N_00", Resources.Load<AudioClip>("Audio/TanteAudio/Tante_5_N_00"));
        dialogueAudioMap.Add("Tante_5_N_01", Resources.Load<AudioClip>("Audio/TanteAudio/Tante_5_N_01"));
        dialogueAudioMap.Add("Tante_5_N_02", Resources.Load<AudioClip>("Audio/TanteAudio/Tante_5_N_02"));
        dialogueAudioMap.Add("Tante_5_N_03", Resources.Load<AudioClip>("Audio/TanteAudio/Tante_5_N_03"));
        dialogueAudioMap.Add("Tante_5_N_04", Resources.Load<AudioClip>("Audio/TanteAudio/Tante_5_N_04"));
        dialogueAudioMap.Add("Tante_5_N_05", Resources.Load<AudioClip>("Audio/TanteAudio/Tante_5_N_05"));
        dialogueAudioMap.Add("Tante_5_N_06", Resources.Load<AudioClip>("Audio/TanteAudio/Tante_5_N_06"));
        dialogueAudioMap.Add("Tante_5_N_07", Resources.Load<AudioClip>("Audio/TanteAudio/Tante_5_N_07"));
        dialogueAudioMap.Add("Tante_5_N_08", Resources.Load<AudioClip>("Audio/TanteAudio/Tante_5_N_08"));
        dialogueAudioMap.Add("Tante_5_N_09", Resources.Load<AudioClip>("Audio/TanteAudio/Tante_5_N_09"));
        dialogueAudioMap.Add("Tante_5_N_10", Resources.Load<AudioClip>("Audio/TanteAudio/Tante_5_N_10"));
        dialogueAudioMap.Add("Tante_5_N_11", Resources.Load<AudioClip>("Audio/TanteAudio/Tante_5_N_11"));
        dialogueAudioMap.Add("Tante_5_N_12", Resources.Load<AudioClip>("Audio/TanteAudio/Tante_5_N_12"));
        dialogueAudioMap.Add("Tante_5_N_13", Resources.Load<AudioClip>("Audio/TanteAudio/Tante_5_N_13"));
        dialogueAudioMap.Add("Tante_5_N_14", Resources.Load<AudioClip>("Audio/TanteAudio/Tante_5_N_14"));
        dialogueAudioMap.Add("Tante_5_N_15", Resources.Load<AudioClip>("Audio/TanteAudio/Tante_5_N_15"));
        dialogueAudioMap.Add("Tante_5_N_16", Resources.Load<AudioClip>("Audio/TanteAudio/Tante_5_N_16"));
        dialogueAudioMap.Add("Tante_5_N_17", Resources.Load<AudioClip>("Audio/TanteAudio/Tante_5_N_17"));
        dialogueAudioMap.Add("Tante_5_N_18", Resources.Load<AudioClip>("Audio/TanteAudio/Tante_5_N_18"));
        dialogueAudioMap.Add("Tante_5_N_19", Resources.Load<AudioClip>("Audio/TanteAudio/Tante_5_N_19"));
        dialogueAudioMap.Add("Tante_5_N_20", Resources.Load<AudioClip>("Audio/TanteAudio/Tante_5_N_20"));
        dialogueAudioMap.Add("Tante_5_N_21", Resources.Load<AudioClip>("Audio/TanteAudio/Tante_5_N_21"));
        dialogueAudioMap.Add("Tante_5_N_22", Resources.Load<AudioClip>("Audio/TanteAudio/Tante_5_N_22"));
        dialogueAudioMap.Add("Tante_5_N_23", Resources.Load<AudioClip>("Audio/TanteAudio/Tante_5_N_23"));
        dialogueAudioMap.Add("Tante_5_OBJ_01", Resources.Load<AudioClip>("Audio/TanteAudio/Tante_5_OBJ_01"));
        dialogueAudioMap.Add("Tante_5_OBJ_02", Resources.Load<AudioClip>("Audio/TanteAudio/Tante_5_OBJ_02"));
        dialogueAudioMap.Add("Tante_5_OBJ_03", Resources.Load<AudioClip>("Audio/TanteAudio/Tante_5_OBJ_03"));
        dialogueAudioMap.Add("Tante_5_OBJ_04", Resources.Load<AudioClip>("Audio/TanteAudio/Tante_5_OBJ_04"));
        dialogueAudioMap.Add("Tante_5_OBJ_05", Resources.Load<AudioClip>("Audio/TanteAudio/Tante_5_OBJ_05"));
        dialogueAudioMap.Add("Tante_5_OBJPAP_01", Resources.Load<AudioClip>("Audio/TanteAudio/Tante_5_OBJPAP_01"));
        dialogueAudioMap.Add("Tante_5_OBJPAP_02", Resources.Load<AudioClip>("Audio/TanteAudio/Tante_5_OBJPAP_02"));
        dialogueAudioMap.Add("Tante_5_OBJPAP_03", Resources.Load<AudioClip>("Audio/TanteAudio/Tante_5_OBJPAP_03"));
        dialogueAudioMap.Add("Tante_5_OBJPAP_04", Resources.Load<AudioClip>("Audio/TanteAudio/Tante_5_OBJPAP_04"));
        
//panks

        dialogueAudioMap.Add("Panks_6_00", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_00"));
        dialogueAudioMap.Add("Panks_6_01", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_01"));
        dialogueAudioMap.Add("Panks_6_02", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_02"));
        dialogueAudioMap.Add("Panks_6_03", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_03"));
        dialogueAudioMap.Add("Panks_6_04", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_04"));
        dialogueAudioMap.Add("Panks_6_05", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_05"));
        dialogueAudioMap.Add("Panks_6_06_beigas", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_06_beigas"));
        dialogueAudioMap.Add("Panks_6_07", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_07"));
        dialogueAudioMap.Add("Panks_6_08", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_08"));
        dialogueAudioMap.Add("Panks_6_09", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_09"));
        dialogueAudioMap.Add("Panks_6_10", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_10"));
        dialogueAudioMap.Add("Panks_6_11", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_11"));
        dialogueAudioMap.Add("Panks_6_12", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_12"));
        dialogueAudioMap.Add("Panks_6_13", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_13"));
        dialogueAudioMap.Add("Panks_6_14", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_14"));
        dialogueAudioMap.Add("Panks_6_15", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_15"));
        dialogueAudioMap.Add("Panks_6_16", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_16"));
        dialogueAudioMap.Add("Panks_6_17", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_17"));
        dialogueAudioMap.Add("Panks_6_18", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_18"));
        dialogueAudioMap.Add("Panks_6_19", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_19"));
        dialogueAudioMap.Add("Panks_6_20", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_20"));
        dialogueAudioMap.Add("Panks_6_20_V2", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_20_V2"));
        dialogueAudioMap.Add("Panks_6_21", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_21"));
        dialogueAudioMap.Add("Panks_6_22", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_22"));
        dialogueAudioMap.Add("Panks_6_23", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_23"));
        dialogueAudioMap.Add("Panks_6_24", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_24"));
        dialogueAudioMap.Add("Panks_6_24_V2", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_24_V2"));
        dialogueAudioMap.Add("Panks_6_25_Nepiekritbeigas", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_25_Nepiekritbeigas"));

        dialogueAudioMap.Add("Panks_6_25_Nepiekritbeigas_V2_1", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_25_Nepiekritbeigas_V2_1"));
        dialogueAudioMap.Add("Panks_6_25_Nepiekritbeigas_V2_2", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_25_Nepiekritbeigas_V2_2"));
        dialogueAudioMap.Add("Panks_6_25_Nepiekritbeigas_V2_3", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_25_Nepiekritbeigas_V2_3"));
        dialogueAudioMap.Add("Panks_6_25_Nepiekritbeigas_V2_4", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_25_Nepiekritbeigas_V2_4"));
        dialogueAudioMap.Add("Panks_6_25_Nepiekritbeigas_V2_5", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_25_Nepiekritbeigas_V2_5"));
        dialogueAudioMap.Add("Panks_6_25_Nepiekritbeigas_V2_6", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_25_Nepiekritbeigas_V2_6"));
        dialogueAudioMap.Add("Panks_6_25_Nepiekritbeigas_V2_7", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_25_Nepiekritbeigas_V2_7"));
        dialogueAudioMap.Add("Panks_6_25_Nepiekritbeigas_V2_8", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_25_Nepiekritbeigas_V2_8"));
        dialogueAudioMap.Add("Panks_6_25_Nepiekritbeigas_V2_9", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_25_Nepiekritbeigas_V2_9"));
        dialogueAudioMap.Add("Panks_6_25_Nepiekritbeigas_V2_10", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_25_Nepiekritbeigas_V2_10"));
        dialogueAudioMap.Add("Panks_6_25_Nepiekritbeigas_V2_11", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_25_Nepiekritbeigas_V2_11"));
        dialogueAudioMap.Add("Panks_6_25_Nepiekritbeigas_V2_12", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_25_Nepiekritbeigas_V2_12"));
        dialogueAudioMap.Add("Panks_6_25_Nepiekritbeigas_V2_13", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_25_Nepiekritbeigas_V2_13"));
        dialogueAudioMap.Add("Panks_6_25_Nepiekritbeigas_V2_14", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_25_Nepiekritbeigas_V2_14"));
        dialogueAudioMap.Add("Panks_6_25_Nepiekritbeigas_V2_15", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_25_Nepiekritbeigas_V2_15"));
        dialogueAudioMap.Add("Panks_6_26_Piekritbeigas", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_26_Piekritbeigas"));

        dialogueAudioMap.Add("Panks_6_OBJ_PUK_1", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_OBJ_PUK_1"));
        dialogueAudioMap.Add("Panks_6_OBJ_PUK_2", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_OBJ_PUK_2"));
        dialogueAudioMap.Add("Panks_6_OBJ_PUK_3", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_OBJ_PUK_3"));

        dialogueAudioMap.Add("Panks_6_OBJ_ETAL_1", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_OBJ_ETAL_1"));
        dialogueAudioMap.Add("Panks_6_OBJ_ETAL_2", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_OBJ_ETAL_2"));
        dialogueAudioMap.Add("Panks_6_OBJ_ETAL_3", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_OBJ_ETAL_3"));
        dialogueAudioMap.Add("Panks_6_OBJ_ETAL_4", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_OBJ_ETAL_4"));

        dialogueAudioMap.Add("Panks_6_OBJ_ROKAS_1", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_OBJ_ROKAS_1"));
        dialogueAudioMap.Add("Panks_6_OBJ_ROKAS_2", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_OBJ_ROKAS_2"));
        dialogueAudioMap.Add("Panks_6_OBJ_ROKAS_3", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_OBJ_ROKAS_3"));
        dialogueAudioMap.Add("Panks_6_OBJ_ROKAS_4", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_OBJ_ROKAS_4"));

        dialogueAudioMap.Add("Panks_6_Loop_1", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_Loop_1"));
        dialogueAudioMap.Add("Panks_6_Loop_2", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_6_Loop_2"));
    

        dialogueAudioMap.Add("Gongs", Resources.Load<AudioClip>("Audio/PapildusSkana/Gongs"));


        // Muris

        dialogueAudioMap.Add("Muris_0_s_01", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_s_01"));
        dialogueAudioMap.Add("Muris_0_s_02", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_s_02"));
        dialogueAudioMap.Add("Muris_0_s_03", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_s_03"));
        dialogueAudioMap.Add("Muris_0_s_04", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_s_04"));
        dialogueAudioMap.Add("Muris_0_s_05", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_s_05"));
        dialogueAudioMap.Add("Muris_0_s_06", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_s_06"));
        dialogueAudioMap.Add("Muris_0_s_07", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_s_07"));
        dialogueAudioMap.Add("Muris_0_s_08", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_s_08"));
        dialogueAudioMap.Add("Muris_0_s_09", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_s_09"));
        dialogueAudioMap.Add("Muris_0_s_10", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_s_10"));
        dialogueAudioMap.Add("Muris_0_s_11", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_s_11"));
        dialogueAudioMap.Add("Muris_0_s_12", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_s_12"));
        dialogueAudioMap.Add("Muris_0_s_13", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_s_13"));
        dialogueAudioMap.Add("Muris_0_s_14", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_s_14"));
        dialogueAudioMap.Add("Muris_0_s_15", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_s_15"));
        dialogueAudioMap.Add("Muris_0_s_16", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_s_16"));
        dialogueAudioMap.Add("Muris_0_s_17", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_s_17"));
        dialogueAudioMap.Add("Muris_0_s_18", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_s_18"));
        dialogueAudioMap.Add("Muris_0_s_19", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_s_19"));
        dialogueAudioMap.Add("Muris_0_s_20", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_s_20"));
        dialogueAudioMap.Add("Muris_0_s_21", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_s_21"));
        dialogueAudioMap.Add("Muris_0_s_22", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_s_22"));
        dialogueAudioMap.Add("Muris_0_s_23", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_s_23"));
        dialogueAudioMap.Add("Muris_0_s_24", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_s_24"));
        dialogueAudioMap.Add("Muris_0_s_25", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_s_25"));
        dialogueAudioMap.Add("Muris_0_s_26", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_s_26"));
        dialogueAudioMap.Add("Muris_0_s_27", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_s_27"));

        dialogueAudioMap.Add("Muris_0_b_01", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_01"));
        dialogueAudioMap.Add("Muris_0_b_02", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_02"));
        dialogueAudioMap.Add("Muris_0_b_03", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_03"));
        dialogueAudioMap.Add("Muris_0_b_04", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_04"));
        dialogueAudioMap.Add("Muris_0_b_05", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_05"));
        dialogueAudioMap.Add("Muris_0_b_06", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_06"));
        dialogueAudioMap.Add("Muris_0_b_07", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_07"));
        dialogueAudioMap.Add("Muris_0_b_08", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_08"));
        dialogueAudioMap.Add("Muris_0_b_09", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_09"));
        dialogueAudioMap.Add("Muris_0_b_10", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_10"));

        dialogueAudioMap.Add("Muris_0_b_Priest_P", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_Priest_P"));
        dialogueAudioMap.Add("Muris_0_b_Priest_N", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_Priest_N"));
        dialogueAudioMap.Add("Muris_0_b_Bomz_P", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_Bomz_P"));
        dialogueAudioMap.Add("Muris_0_b_Bomz_N", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_Bomz_N"));
        dialogueAudioMap.Add("Muris_0_b_Maksl_P", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_Maksl_P"));
        dialogueAudioMap.Add("Muris_0_b_Maksl_N", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_Maksl_N"));
        dialogueAudioMap.Add("Muris_0_b_Tante_P", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_Tante_P"));
        dialogueAudioMap.Add("Muris_0_b_Tante_N", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_Tante_N"));
        dialogueAudioMap.Add("Muris_0_b_Panks_P", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_Panks_P"));
        dialogueAudioMap.Add("Muris_0_b_Panks_N", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_Panks_N"));
        dialogueAudioMap.Add("Muris_0_b_Pianiste_P", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_Pianiste_P"));
        dialogueAudioMap.Add("Muris_0_b_Pianiste_N", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_Pianiste_N"));
        dialogueAudioMap.Add("Muris_0_b_labas_11", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_labas_11"));
        dialogueAudioMap.Add("Muris_0_b_labas_12", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_labas_12"));
        dialogueAudioMap.Add("Muris_0_b_labas_13", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_labas_13"));
        dialogueAudioMap.Add("Muris_0_b_labas_14", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_labas_14"));
        dialogueAudioMap.Add("Muris_0_b_labas_15_beigas", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_labas_15_beigas"));
        dialogueAudioMap.Add("Muris_0_b_labas_16_beigas", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_labas_16_beigas"));
        dialogueAudioMap.Add("Muris_0_b_labas_17", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_labas_17"));
        dialogueAudioMap.Add("Muris_0_b_labas_18", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_labas_18"));
        dialogueAudioMap.Add("Muris_0_b_labas_19", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_labas_19"));
        dialogueAudioMap.Add("Muris_0_b_labas_20", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_labas_20"));
        dialogueAudioMap.Add("Muris_0_b_labas_21", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_labas_21"));
        dialogueAudioMap.Add("Muris_0_b_labas_22_pareizs_BEIGAS", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_labas_22_pareizs_BEIGAS"));
        dialogueAudioMap.Add("Muris_0_b_labas_23_nepareizs_BEIGAS", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_labas_23_nepareizs_BEIGAS"));


        dialogueAudioMap.Add("Muris_0_b_sliktas_01", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_sliktas_01"));
        dialogueAudioMap.Add("Muris_0_b_sliktas_02", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_sliktas_02"));
        dialogueAudioMap.Add("Muris_0_b_sliktas_03", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_sliktas_03"));
        dialogueAudioMap.Add("Muris_0_b_sliktas_04", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_sliktas_04"));
        dialogueAudioMap.Add("Muris_0_b_sliktas_05", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_sliktas_05"));
        dialogueAudioMap.Add("Muris_0_b_sliktas_06", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_sliktas_06"));
        dialogueAudioMap.Add("Muris_0_b_sliktas_07", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_sliktas_07"));
        dialogueAudioMap.Add("Muris_0_b_sliktas_08", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_sliktas_08"));
        dialogueAudioMap.Add("Muris_0_b_sliktas_09", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_sliktas_09"));
        dialogueAudioMap.Add("Muris_0_b_sliktas_10", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_sliktas_10"));
        dialogueAudioMap.Add("Muris_0_b_sliktas_11", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_sliktas_11"));
        dialogueAudioMap.Add("Muris_0_b_sliktas_12_BEIGAS", Resources.Load<AudioClip>("Audio/MurisAudio/Muris_0_b_sliktas_12_BEIGAS"));

        dialogueAudioMap.Add("sfxmajacymbal", Resources.Load<AudioClip>("Audio/MurisAudio/sfxmajacymbal"));
        dialogueAudioMap.Add("sfxmajacymbalBad", Resources.Load<AudioClip>("Audio/MurisAudio/sfxmajacymbalBad"));


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
        if (audioSource != null && dialogueAudioMap.TryGetValue(audioTag, out AudioClip clip))
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }








public bool IsFirstStory()
{
    return isFirstStory;
}

}
