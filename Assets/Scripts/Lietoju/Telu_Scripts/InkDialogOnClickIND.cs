using System;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;

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
    private bool playerInRange = false; // Track if the player is within the collider
    private int selectedChoiceIndex = 0; // Track the currently selected choice
    private List<Button> choiceButtons = new List<Button>();
    private bool isDialogueActive = false; // Track whether dialogue is active

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
        {
            interactiveCharacter.SetDialogueActive(true);
        }

        if (characterMovement != null)
        {
            characterMovement.enabled = false; // Stop the player's movement
        }

        if (cameraTransition != null && cameraTargetPosition != null)
        {
            cameraTransition.MoveToTarget(cameraTargetPosition); // Move the camera to the target position
        }

        if (characterAnimator != null && !string.IsNullOrEmpty(animationTrigger))
        {
            characterAnimator.SetTrigger(animationTrigger); // Play the character's animation
        }

        story = new Story(isFirstStory ? inkJSONAsset1.text : inkJSONAsset2.text);

        if (OnCreateStory != null)
            OnCreateStory(story);

        isFirstStory = false; // After the first story, toggle to the second JSON file
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

    if (characterMovement != null)
    {
        characterMovement.enabled = true; // Resume player movement
    }

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
        dialogueAudioMap.Add("Rainis_1_N_00", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_N_00"));
        dialogueAudioMap.Add("Rainis_1_N_01", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_N_01"));
        dialogueAudioMap.Add("Rainis_1_N_03", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_N_03"));
        dialogueAudioMap.Add("Rainis_1_N_04", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_N_04"));
        dialogueAudioMap.Add("Rainis_1_N_05", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_N_05"));
        dialogueAudioMap.Add("Rainis_1_N_06", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_N_06"));
        dialogueAudioMap.Add("Rainis_1_N_07", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_N_07"));
        dialogueAudioMap.Add("Rainis_1_N_08", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_N_08"));
        dialogueAudioMap.Add("Rainis_1_N_09", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_N_09"));
        dialogueAudioMap.Add("Rainis_1_N_10", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_N_10"));
        dialogueAudioMap.Add("Rainis_1_N_11", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_N_11"));
        dialogueAudioMap.Add("Rainis_1_N_12", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_N_12"));
        dialogueAudioMap.Add("Rainis_1_N_13", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_N_13"));
        dialogueAudioMap.Add("Rainis_1_N_14", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_N_14"));
        dialogueAudioMap.Add("Rainis_1_N_15", Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_N_15"));
        dialogueAudioMap.Add("Rainis_1_N_16",Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_N_16"));
        dialogueAudioMap.Add("Rainis_1_N_17",Resources.Load<AudioClip>("Audio/RainisAudio/Rainis_1_N_17"));


       //priesteris
       
        dialogueAudioMap.Add("Atkal_Priesteris_2_N_000", Resources.Load<AudioClip>("Audio/PriesterisAudio/Atkal_Priesteris_2_N_000"));
        dialogueAudioMap.Add("Atkal_Priesteris_2_N_00", Resources.Load<AudioClip>("Audio/PriesterisAudio/Atkal_Priesteris_2_N_00"));
        dialogueAudioMap.Add("Atkal_Priesteris_2_N_01", Resources.Load<AudioClip>("Audio/PriesterisAudio/Atkal_Priesteris_2_N_01"));
        dialogueAudioMap.Add("Atkal_Priesteris_2_N_02", Resources.Load<AudioClip>("Audio/PriesterisAudio/Atkal_Priesteris_2_N_02"));
        dialogueAudioMap.Add("Atkal_Priesteris_2_N_03", Resources.Load<AudioClip>("Audio/PriesterisAudio/Atkal_Priesteris_2_N_03"));
        dialogueAudioMap.Add("Atkal_Priesteris_2_N_04", Resources.Load<AudioClip>("Audio/PriesterisAudio/Atkal_Priesteris_2_N_04"));
        dialogueAudioMap.Add("Atkal_Priesteris_2_N_05", Resources.Load<AudioClip>("Audio/PriesterisAudio/Atkal_Priesteris_2_N_05"));
        dialogueAudioMap.Add("Atkal_Priesteris_2_N_06", Resources.Load<AudioClip>("Audio/PriesterisAudio/Atkal_Priesteris_2_N_06"));
        dialogueAudioMap.Add("Atkal_Priesteris_2_N_07", Resources.Load<AudioClip>("Audio/PriesterisAudio/Atkal_Priesteris_2_N_07"));
        dialogueAudioMap.Add("Atkal_Priesteris_2_N_08", Resources.Load<AudioClip>("Audio/PriesterisAudio/Atkal_Priesteris_2_N_08"));
        dialogueAudioMap.Add("Atkal_Priesteris_2_N_09", Resources.Load<AudioClip>("Audio/PriesterisAudio/Atkal_Priesteris_2_N_09"));

        dialogueAudioMap.Add("Priesteris_2_N_00", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_N_00"));
        dialogueAudioMap.Add("Priesteris_2_N_01", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_N_01"));
        dialogueAudioMap.Add("Priesteris_2_N_03", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_N_03"));
        dialogueAudioMap.Add("Priesteris_2_N_04", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_N_04"));
        dialogueAudioMap.Add("Priesteris_2_N_05", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_N_05"));
        dialogueAudioMap.Add("Priesteris_2_N_06", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_N_06"));
        dialogueAudioMap.Add("Priesteris_2_N_07", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_N_07"));
        dialogueAudioMap.Add("Priesteris_2_N_08", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_N_08"));
        dialogueAudioMap.Add("Priesteris_2_N_09", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_N_09"));
        dialogueAudioMap.Add("Priesteris_2_N_10", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_N_10"));
        dialogueAudioMap.Add("Priesteris_2_N_11", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_N_11"));
        dialogueAudioMap.Add("Priesteris_2_N_12", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_N_12"));
        dialogueAudioMap.Add("Priesteris_2_N_13", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_N_13"));
        dialogueAudioMap.Add("Priesteris_2_N_14", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_N_14"));
        dialogueAudioMap.Add("Priesteris_2_N_15", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_N_15"));
        dialogueAudioMap.Add("Priesteris_2_N_16", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_N_16"));
        dialogueAudioMap.Add("Priesteris_2_N_17", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_N_17"));
        dialogueAudioMap.Add("Priesteris_2_N_18", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_N_18"));
        dialogueAudioMap.Add("Priesteris_2_N_19", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_N_19"));
        dialogueAudioMap.Add("Priesteris_2_N_20", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_N_20"));
        dialogueAudioMap.Add("Priesteris_2_N_21", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_N_21"));
        dialogueAudioMap.Add("Priesteris_2_N_22", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_N_22"));
        dialogueAudioMap.Add("Priesteris_2_N_23", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_N_23"));
        dialogueAudioMap.Add("Priesteris_2_N_24", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_N_24"));
        dialogueAudioMap.Add("Priesteris_2_N_25", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_N_25"));
        dialogueAudioMap.Add("Priesteris_2_N_26", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_N_26"));
        dialogueAudioMap.Add("Priesteris_2_N_27", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_N_27"));

        dialogueAudioMap.Add("Priesteris_2_N_Aploksne", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_N_Aploksne"));
        dialogueAudioMap.Add("Priesteris_2_Piezime_00", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_Piezime_00"));
        dialogueAudioMap.Add("Priesteris_2_Piezime_01", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_Piezime_01"));
        dialogueAudioMap.Add("Priesteris_2_Piezime_02", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_Piezime_02"));

        dialogueAudioMap.Add("T_Atkal_Priesteris_2_N_00", Resources.Load<AudioClip>("Audio/PriesterisAudio/T_Atkal_Priesteris_2_N_00"));
        dialogueAudioMap.Add("T_Atkal_Priesteris_2_N_01", Resources.Load<AudioClip>("Audio/PriesterisAudio/T_Atkal_Priesteris_2_N_01"));
        dialogueAudioMap.Add("T_Atkal_Priesteris_2_N_02", Resources.Load<AudioClip>("Audio/PriesterisAudio/T_Atkal_Priesteris_2_N_02"));

        dialogueAudioMap.Add("T_Priesteris_2_N_00", Resources.Load<AudioClip>("Audio/PriesterisAudio/T_Priesteris_2_N_00"));
        dialogueAudioMap.Add("T_Priesteris_2_N_01", Resources.Load<AudioClip>("Audio/PriesterisAudio/T_Priesteris_2_N_01"));
        dialogueAudioMap.Add("T_Priesteris_2_N_02", Resources.Load<AudioClip>("Audio/PriesterisAudio/T_Priesteris_2_N_02"));

        dialogueAudioMap.Add("Priesteris_2_Kaklarota_000", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_Kaklarota_000"));
        dialogueAudioMap.Add("Priesteris_2_Kaklarota_00", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_Kaklarota_00"));
        dialogueAudioMap.Add("Priesteris_2_Kaklarota_01", Resources.Load<AudioClip>("Audio/PriesterisAudio/Priesteris_2_Kaklarota_01"));

        

//bomzis 
    
        dialogueAudioMap.Add("Bomzis_3_Aploksne_00", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_Aploksne_00"));
        dialogueAudioMap.Add("Bomzis_3_Aploksne_01", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_Aploksne_01"));
        dialogueAudioMap.Add("Bomzis_3_Aploksne_02", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_Aploksne_02"));


        dialogueAudioMap.Add("Bomzis_3_N_00", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_N_00"));
        dialogueAudioMap.Add("Bomzis_3_N_01", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_N_01"));
        dialogueAudioMap.Add("Bomzis_3_N_02", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_N_02"));
        dialogueAudioMap.Add("Bomzis_3_N_03", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_N_03"));
        dialogueAudioMap.Add("Bomzis_3_N_04", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_N_04"));
        dialogueAudioMap.Add("Bomzis_3_N_05", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_N_05"));
        dialogueAudioMap.Add("Bomzis_3_N_06", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_N_06"));
        dialogueAudioMap.Add("Bomzis_3_N_07", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_N_07"));
        dialogueAudioMap.Add("Bomzis_3_N_08", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_N_08"));
        dialogueAudioMap.Add("Bomzis_3_N_09", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_N_09"));
        dialogueAudioMap.Add("Bomzis_3_N_10", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_N_10"));
        dialogueAudioMap.Add("Bomzis_3_N_11", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_N_11"));
        dialogueAudioMap.Add("Bomzis_3_N_12", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_N_12"));
        dialogueAudioMap.Add("Bomzis_3_N_13", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_N_13"));
        dialogueAudioMap.Add("Bomzis_3_N_14", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_N_14"));
        dialogueAudioMap.Add("Bomzis_3_N_15", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_N_15"));
        dialogueAudioMap.Add("Bomzis_3_N_16", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_N_16"));
        dialogueAudioMap.Add("Bomzis_3_N_17", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_N_17"));
        dialogueAudioMap.Add("Bomzis_3_N_18", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_N_18"));
        dialogueAudioMap.Add("Bomzis_3_N_19", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_N_19"));
        dialogueAudioMap.Add("Bomzis_3_N_20", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_N_20"));
        dialogueAudioMap.Add("Bomzis_3_N_21", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_N_21"));
        dialogueAudioMap.Add("Bomzis_3_N_23", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_N_23"));
        dialogueAudioMap.Add("Bomzis_3_N_24", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_N_24"));
        dialogueAudioMap.Add("Bomzis_3_N_25", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_N_25"));
        dialogueAudioMap.Add("Bomzis_3_N_26", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_N_26"));
        dialogueAudioMap.Add("Bomzis_3_N_27", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_N_27"));
        dialogueAudioMap.Add("Bomzis_3_N_28", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_N_28"));
        dialogueAudioMap.Add("Bomzis_3_N_29", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_N_29"));
        dialogueAudioMap.Add("Bomzis_3_N_30", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_N_30"));
        dialogueAudioMap.Add("Bomzis_3_N_31", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_N_31"));
        dialogueAudioMap.Add("Bomzis_3_N_32", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_N_32"));
        dialogueAudioMap.Add("Bomzis_3_N_33", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_N_33"));
        dialogueAudioMap.Add("Bomzis_3_N_34", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_N_34"));
        dialogueAudioMap.Add("Bomzis_3_N_35", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_N_35"));
        dialogueAudioMap.Add("Bomzis_3_N_36", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_N_36"));
        dialogueAudioMap.Add("Bomzis_3_N_37", Resources.Load<AudioClip>("Audio/BomzisAudio/Bomzis_3_N_37"));

        dialogueAudioMap.Add("T_Bomzis_3_N_00", Resources.Load<AudioClip>("Audio/BomzisAudio/T_Bomzis_3_N_00"));
        dialogueAudioMap.Add("T_Bomzis_3_N_01", Resources.Load<AudioClip>("Audio/BomzisAudio/T_Bomzis_3_N_01"));
        dialogueAudioMap.Add("T_Bomzis_3_N_02", Resources.Load<AudioClip>("Audio/BomzisAudio/T_Bomzis_3_N_02"));
        dialogueAudioMap.Add("T_Bomzis_3_N_03", Resources.Load<AudioClip>("Audio/BomzisAudio/T_Bomzis_3_N_03"));

//makslinieks

        dialogueAudioMap.Add("Makslinieks_4_N_00", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_N_00"));
        dialogueAudioMap.Add("Makslinieks_4_N_01", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_N_01"));
        dialogueAudioMap.Add("Makslinieks_4_N_02", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_N_02"));
        dialogueAudioMap.Add("Makslinieks_4_N_03", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_N_03"));
        dialogueAudioMap.Add("Makslinieks_4_N_04", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_N_04"));
        dialogueAudioMap.Add("Makslinieks_4_N_05", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_N_05"));
        dialogueAudioMap.Add("Makslinieks_4_N_06", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_N_06"));
        dialogueAudioMap.Add("Makslinieks_4_N_07", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_N_07"));
        dialogueAudioMap.Add("Makslinieks_4_N_08", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_N_08"));
        dialogueAudioMap.Add("Makslinieks_4_N_09", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_N_09"));
        dialogueAudioMap.Add("Makslinieks_4_N_10", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_N_10"));
        dialogueAudioMap.Add("Makslinieks_4_N_11", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_N_11"));
        dialogueAudioMap.Add("Makslinieks_4_N_11_1", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_N_11_1"));
        dialogueAudioMap.Add("Makslinieks_4_N_12", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_N_12"));
        dialogueAudioMap.Add("Makslinieks_4_N_13", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_N_13"));
        dialogueAudioMap.Add("Makslinieks_4_N_14", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_N_14"));
        dialogueAudioMap.Add("Makslinieks_4_N_14_1", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_N_14_1"));
        dialogueAudioMap.Add("Makslinieks_4_N_15", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_N_15"));
        dialogueAudioMap.Add("Makslinieks_4_N_16", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_N_16"));
        dialogueAudioMap.Add("Makslinieks_4_N_17", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_N_17"));
        dialogueAudioMap.Add("Makslinieks_4_N_17_1", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_N_17_1"));
        dialogueAudioMap.Add("Makslinieks_4_N_18", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_N_18"));
        dialogueAudioMap.Add("Makslinieks_4_N_19", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_N_19"));
        dialogueAudioMap.Add("Makslinieks_4_N_20", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_N_20"));
        dialogueAudioMap.Add("Makslinieks_4_N_21", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_N_21"));
        dialogueAudioMap.Add("Makslinieks_4_N_22", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_N_22"));
        dialogueAudioMap.Add("Makslinieks_4_N_23", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_N_23"));
        dialogueAudioMap.Add("Makslinieks_4_N_24", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_N_24"));
        dialogueAudioMap.Add("Makslinieks_4_N_25", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_N_25"));
        dialogueAudioMap.Add("Makslinieks_4_N_26", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_N_26"));
        dialogueAudioMap.Add("Makslinieks_4_N_26_1", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_N_26_1"));
        dialogueAudioMap.Add("Makslinieks_4_N_27", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_N_27"));
        
        dialogueAudioMap.Add("Makslinieks_4_NEG_00", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_NEG_00"));
        dialogueAudioMap.Add("Makslinieks_4_NEG_01", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_NEG_01"));
        dialogueAudioMap.Add("Makslinieks_4_NEG_02", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_NEG_02"));
        dialogueAudioMap.Add("Makslinieks_4_NEG_03", Resources.Load<AudioClip>("Audio/MakslinieksAudio/Makslinieks_4_NEG_03"));
        dialogueAudioMap.Add("T_Makslinieks_4_N_00", Resources.Load<AudioClip>("Audio/MakslinieksAudio/T_Makslinieks_4_N_00"));



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
        

        dialogueAudioMap.Add("Panks_5_N_00", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_5_N_00"));
        dialogueAudioMap.Add("Panks_5_N_01", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_5_N_01"));
        dialogueAudioMap.Add("Panks_5_N_02", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_5_N_02"));
        dialogueAudioMap.Add("Panks_5_N_03", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_5_N_03"));
        dialogueAudioMap.Add("Panks_5_N_04", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_5_N_04"));
        dialogueAudioMap.Add("Panks_5_N_05", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_5_N_05"));
        dialogueAudioMap.Add("Panks_5_N_06", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_5_N_06"));
        dialogueAudioMap.Add("Panks_5_N_07", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_5_N_07"));
        dialogueAudioMap.Add("Panks_5_N_08", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_5_N_08"));
        dialogueAudioMap.Add("Panks_5_N_09", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_5_N_09"));
        dialogueAudioMap.Add("Panks_5_N_10", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_5_N_10"));
        dialogueAudioMap.Add("Panks_5_N_11", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_5_N_11"));
        dialogueAudioMap.Add("Panks_5_N_12", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_5_N_12"));
        dialogueAudioMap.Add("Panks_5_N_13", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_5_N_13"));
        dialogueAudioMap.Add("Panks_5_N_14", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_5_N_14"));
        dialogueAudioMap.Add("Panks_5_N_15", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_5_N_15"));
        dialogueAudioMap.Add("Panks_5_N_16", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_5_N_16"));
        dialogueAudioMap.Add("Panks_5_N_17", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_5_N_17"));
        dialogueAudioMap.Add("Panks_5_N_18", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_5_N_18"));
        dialogueAudioMap.Add("Panks_5_Neg_19", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_5_Neg_19"));
        dialogueAudioMap.Add("Panks_5_Neg_20", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_5_Neg_20"));
        dialogueAudioMap.Add("Panks_5_Neg_21", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_5_Neg_21"));
        dialogueAudioMap.Add("Panks_5_Neg_22", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_5_Neg_22"));
        dialogueAudioMap.Add("Panks_5_Neg_23", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_5_Neg_23"));
        dialogueAudioMap.Add("Panks_5_Neg_24", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_5_Neg_24"));
        dialogueAudioMap.Add("Panks_5_Neg_25", Resources.Load<AudioClip>("Audio/PanksAudio/Panks_5_Neg_25"));
        dialogueAudioMap.Add("T_Panks_5_N_00", Resources.Load<AudioClip>("Audio/PanksAudio/T_Panks_5_N_00"));
        dialogueAudioMap.Add("T_Panks_5_N_01", Resources.Load<AudioClip>("Audio/PanksAudio/T_Panks_5_N_01"));
        dialogueAudioMap.Add("T_Panks_5_N_02", Resources.Load<AudioClip>("Audio/PanksAudio/T_Panks_5_N_02"));

        dialogueAudioMap.Add("Gongs", Resources.Load<AudioClip>("Audio/PapildusSkana/Gongs"));



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
