using System;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class ScriptB : MonoBehaviour
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
    public bool IsStoryInitialized { get; private set; } = false; // To track if the story has been initialized

    void Awake()
    {
        Debug.Log("ScriptB Awake() called for " + gameObject.name);
        RemoveChildren();
        InitializeDialogueAudioMap();
    }

    void Start()
    {
        Debug.Log("ScriptB Start() called for " + gameObject.name);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            characterMovement = player.GetComponent<CharacterMovement2>();
            Debug.Log("CharacterMovement2 component found on Player");
        }

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
        if (IsStoryInitialized) return;

        Debug.Log("StartStoryOnClick called for " + gameObject.name);

        if (interactiveCharacter != null)
        {
            interactiveCharacter.SetDialogueActive(true);
        }

        if (characterMovement != null)
        {
            characterMovement.enabled = false;
            Debug.Log("Character movement stopped");
        }

        if (cameraTransition != null && cameraTargetPosition != null)
        {
            cameraTransition.MoveToTarget(cameraTargetPosition);
            Debug.Log("Camera moving to target position");
        }

        if (characterAnimator != null && !string.IsNullOrEmpty(animationTrigger))
        {
            characterAnimator.SetTrigger(animationTrigger);
            Debug.Log("Character animation started");
        }

        story = new Story(inkJSONAsset.text);
        IsStoryInitialized = true;

        OnCreateStory?.Invoke(story);

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

                button.onClick.AddListener(() => OnClickChoiceButton(choice));
            }
        }
        else
        {
            Button choice = CreateChoiceView("Close");
            choice.onClick.AddListener(() =>
            {
                RemoveChildren();
                if (characterMovement != null)
                {
                    characterMovement.enabled = true;
                    Debug.Log("Character movement resumed");
                }

                if (cameraTransition != null)
                {
                    cameraTransition.ResetCamera();
                    Debug.Log("Camera reset to original position");
                }

                if (interactiveCharacter != null)
                {
                    interactiveCharacter.SetDialogueActive(false);
                }
                IsStoryInitialized = false; // Reset initialization status
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
        Text storyText = Instantiate(textPrefab);
        storyText.text = text;
        storyText.transform.SetParent(canvas.transform, false);
    }

    Button CreateChoiceView(string text)
    {
        Debug.Log("CreateChoiceView called with text: " + text);
        Button choice = Instantiate(buttonPrefab);
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
        dialogueAudioMap = new Dictionary<string, AudioClip>
        {
            { "Intro", Resources.Load<AudioClip>("Audio/bomzisAudio/Intro") },
            { "AncientReligion", Resources.Load<AudioClip>("Audio/bomzisAudio/AncientReligion") },
            { "CurrentLandscape", Resources.Load<AudioClip>("Audio/bomzisAudio/CurrentLandscape") },
            { "PersonalStory", Resources.Load<AudioClip>("Audio/bomzisAudio/PersonalStory") },
            { "RitualsAndTraditions", Resources.Load<AudioClip>("Audio/bomzisAudio/RitualsAndTraditions") },
            { "AnotherStory", Resources.Load<AudioClip>("Audio/bomzisAudio/AnotherStory") },
            { "End", Resources.Load<AudioClip>("Audio/bomzisAudio/End") }
        };
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
