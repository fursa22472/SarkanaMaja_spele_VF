using System;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class InkDialogOnClickIND : MonoBehaviour
{
    public static event Action<Story> OnCreateStory;

    [SerializeField] private TextAsset inkJSONAsset = null;
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

    void Awake()
    {
        Debug.Log("InkDialogOnClick Awake() called for " + gameObject.name);
        RemoveChildren();
        InitializeDialogueAudioMap();
    }

    void Start()
    {
        Debug.Log("InkDialogOnClick Start() called for " + gameObject.name);

        // Find the player object and get the CharacterMovement2 script
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            characterMovement = player.GetComponent<CharacterMovement2>();
            Debug.Log("CharacterMovement2 component found on Player");
        }

        // Find the main camera and get the CameraTransition script
        GameObject mainCamera = Camera.main.gameObject;
        if (mainCamera != null)
        {
            cameraTransition = mainCamera.GetComponent<CameraTransition>();
            Debug.Log("CameraTransition component found on Main Camera");
        }
    }

    void OnMouseDown()
    {
        Debug.Log("Mouse clicked on " + gameObject.name);
        StartStoryOnClick();
    }

    public void StartStoryOnClick()
    {
        Debug.Log("StartStoryOnClick called for " + gameObject.name);

        // Notify InteractiveCharacter to hide the indicator and disable the collider
        if (interactiveCharacter != null)
        {
            interactiveCharacter.SetDialogueActive(true);
        }

        if (characterMovement != null)
        {
            characterMovement.enabled = false; // Stop the player's movement
            Debug.Log("Character movement stopped");
        }

        if (cameraTransition != null && cameraTargetPosition != null)
        {
            cameraTransition.MoveToTarget(cameraTargetPosition); // Move the camera to the target position
            Debug.Log("Camera moving to target position");
        }

        if (characterAnimator != null && !string.IsNullOrEmpty(animationTrigger))
        {
            characterAnimator.SetTrigger(animationTrigger); // Play the character's animation
            Debug.Log("Character animation started");
        }

        story = new Story(inkJSONAsset.text);

        if (OnCreateStory != null)
            OnCreateStory(story);

        RefreshView();
    }

    void RefreshView()
    {
        Debug.Log("RefreshView called");
        RemoveChildren();

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

                button.onClick.AddListener(delegate {
                    OnClickChoiceButton(choice);
                });
            }
        }
        else
        {
            Button choice = CreateChoiceView("Close");
            choice.onClick.AddListener(delegate {
                RemoveChildren();
                if (characterMovement != null)
                {
                    characterMovement.enabled = true; // Resume the player's movement when closing the story
                    Debug.Log("Character movement resumed");
                }

                if (cameraTransition != null)
                {
                    cameraTransition.ResetCamera(); // Reset the camera to its original position
                    Debug.Log("Camera reset to original position");
                }

                // Notify InteractiveCharacter to show the indicator again and enable the collider
                if (interactiveCharacter != null)
                {
                    interactiveCharacter.SetDialogueActive(false);
                }
            });
        }
    }

    void OnClickChoiceButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        RefreshView();
    }

    void CreateContentView(string text)
    {
        Debug.Log("CreateContentView called with text: " + text);
        Text storyText = Instantiate(textPrefab) as Text;
        storyText.text = text;
        storyText.transform.SetParent(canvas.transform, false);
    }

    Button CreateChoiceView(string text)
    {
        Debug.Log("CreateChoiceView called with text: " + text);
        Button choice = Instantiate(buttonPrefab) as Button;
        choice.transform.SetParent(canvas.transform, false);

        Text choiceText = choice.GetComponentInChildren<Text>();
        choiceText.text = text;

        HorizontalLayoutGroup layoutGroup = choice.GetComponent<HorizontalLayoutGroup>();
        layoutGroup.childForceExpandHeight = false;

        return choice;
    }

    void RemoveChildren()
    {
        Debug.Log("RemoveChildren called");
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
        dialogueAudioMap.Add("Intro", Resources.Load<AudioClip>("Audio/bomzisAudio/Intro"));
        dialogueAudioMap.Add("AncientReligion", Resources.Load<AudioClip>("Audio/bomzisAudio/AncientReligion"));
        dialogueAudioMap.Add("CurrentLandscape", Resources.Load<AudioClip>("Audio/bomzisAudio/CurrentLandscape"));
        dialogueAudioMap.Add("PersonalStory", Resources.Load<AudioClip>("Audio/bomzisAudio/PersonalStory"));
        dialogueAudioMap.Add("RitualsAndTraditions", Resources.Load<AudioClip>("Audio/bomzisAudio/RitualsAndTraditions"));
        dialogueAudioMap.Add("AnotherStory", Resources.Load<AudioClip>("Audio/bomzisAudio/AnotherStory"));
        dialogueAudioMap.Add("End", Resources.Load<AudioClip>("Audio/bomzisAudio/End"));
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
            Debug.Log("Playing audio: " + audioTag);
        }
        else
        {
            Debug.LogError("Audio tag not found or audio source is null: " + audioTag);
        }
    }
}
