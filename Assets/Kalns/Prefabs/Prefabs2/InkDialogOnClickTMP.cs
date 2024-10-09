using System;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro; // Import TextMeshPro namespace
using UnityEngine;
using UnityEngine.UI;

public class InkDialogOnClickTMP : MonoBehaviour
{
    public static event Action<Story> OnCreateStory;

    [SerializeField] private TextAsset inkJSONAsset = null;
    [SerializeField] private Canvas canvas = null;
    [SerializeField] private GameObject panel = null; // Reference to any panel
    [SerializeField] private TextMeshProUGUI textPrefab = null; // Use TextMeshProUGUI for dialogue text
    [SerializeField] private Button buttonPrefab = null;
    [SerializeField] private Transform cameraTargetPosition; // Target position for the camera
    [SerializeField] private Animator characterAnimator; // Animator for the character
    [SerializeField] private string animationTrigger; // Animation trigger name
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private InteractiveCharacter interactiveCharacter; // Reference to the InteractiveCharacter script

    private Story story;
    private CharacterMovement2 characterMovement; // Reference to the player's movement script
    private CameraTransition cameraTransition; // Reference to the camera transition script

    void Awake()
    {
        Debug.Log("InkDialogOnClickTMP Awake() called for " + gameObject.name);
        RemoveChildren();
    }

    void Start()
    {
        Debug.Log("InkDialogOnClickTMP Start() called for " + gameObject.name);

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

        // Ensure the panel is initially inactive
        if (panel != null)
        {
            panel.SetActive(false);
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

        // Activate the canvas and panel
        if (canvas != null)
        {
            canvas.gameObject.SetActive(true);
            Debug.Log("Canvas activated.");
        }
        else
        {
            Debug.LogError("Canvas is not assigned!");
        }

        if (panel != null)
        {
            panel.SetActive(true); // Ensure the panel is visible
            Debug.Log("Panel activated.");
        }
        else
        {
            Debug.LogError("Panel is not assigned!");
        }

        // Notify InteractiveCharacter to hide the indicator and disable the collider
        if (interactiveCharacter != null)
        {
            interactiveCharacter.SetDialogueActive(true);
        }

        if (characterMovement != null)
        {
            characterMovement.enabled = false; // Stop the player's movement
            Debug.Log("Character movement stopped.");
        }

        if (cameraTransition != null && cameraTargetPosition != null)
        {
            cameraTransition.MoveToTarget(cameraTargetPosition); // Move the camera to the target position
            Debug.Log("Camera moving to target position.");
        }

        if (characterAnimator != null && !string.IsNullOrEmpty(animationTrigger))
        {
            characterAnimator.SetTrigger(animationTrigger); // Play the character's animation
            Debug.Log("Character animation started.");
        }

        // Start the Ink story
        story = new Story(inkJSONAsset.text);

        if (OnCreateStory != null)
            OnCreateStory(story);

        RefreshView();
    }

    void RefreshView()
    {
        Debug.Log("RefreshView called.");
        RemoveChildren();

        while (story.canContinue)
        {
            string text = story.Continue().Trim();
            CreateContentView(text);
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
                CloseDialogue();
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

        // Instantiate the TextMeshProUGUI object
        TextMeshProUGUI storyText = Instantiate(textPrefab, panel.transform);
        storyText.text = text; // Set the text
        storyText.gameObject.SetActive(true); // Ensure it's active
    }

    Button CreateChoiceView(string text)
    {
        Debug.Log("CreateChoiceView called with text: " + text);

        // Instantiate the Button object
        Button choice = Instantiate(buttonPrefab, panel.transform);
        TextMeshProUGUI choiceText = choice.GetComponentInChildren<TextMeshProUGUI>(); // Use TextMeshProUGUI

        if (choiceText != null)
        {
            choiceText.text = text; // Set the button text
            choice.gameObject.SetActive(true); // Ensure it's active
            Debug.Log("Button created with text: " + text);
        }
        else
        {
            Debug.LogError("No TextMeshProUGUI component found in button prefab!");
        }

        return choice;
    }

    void RemoveChildren()
    {
        Debug.Log("RemoveChildren called.");
        foreach (Transform child in panel.transform)
        {
            Destroy(child.gameObject); // Destroy all children of the panel
        }
    }

    void CloseDialogue()
    {
        RemoveChildren();

        if (characterMovement != null)
        {
            characterMovement.enabled = true; // Resume the player's movement when closing the story
            Debug.Log("Character movement resumed.");
        }

        if (cameraTransition != null)
        {
            cameraTransition.ResetCamera(); // Reset the camera to its original position
            Debug.Log("Camera reset to original position.");
        }

        // Notify InteractiveCharacter to show the indicator again and enable the collider
        if (interactiveCharacter != null)
        {
            interactiveCharacter.SetDialogueActive(false);
        }

        // Hide the panel when the dialogue ends
        if (panel != null)
        {
            panel.SetActive(false);
            Debug.Log("Panel deactivated.");
        }
    }
}
