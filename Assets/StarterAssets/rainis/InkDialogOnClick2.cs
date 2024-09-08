// Dialogue script for handling the interaction
using System;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class InkDialogOnClick2 : MonoBehaviour
{
    public static event Action<Story> OnCreateStory;

    [SerializeField] private TextAsset inkJSONAsset = null;
    [SerializeField] private Canvas canvas = null;
    [SerializeField] private Text textPrefab = null;
    [SerializeField] private Button buttonPrefab = null;
    [SerializeField] private Transform cameraTargetPosition; // Target position for the camera
    [SerializeField] private Animator characterAnimator; // Animator for the character
    [SerializeField] private string animationTrigger; // Animation trigger name

    private Story story;
    private CharacterMovement2 characterMovement; // Reference to the player's movement script
    private CameraTransition cameraTransition; // Reference to the camera transition script

    void Awake()
    {
        Debug.Log("InkDialogOnClick Awake() called");
        RemoveChildren();
    }

    void Start()
    {
        Debug.Log("InkDialogOnClick Start() called");

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
        Debug.Log("Mouse clicked on object");
        StartStoryOnClick();
    }

    public void StartStoryOnClick()
    {
        Debug.Log("StartStoryOnClick called");

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
}
