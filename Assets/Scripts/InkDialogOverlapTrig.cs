using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

public class InkDialogOverlapTrig : MonoBehaviour
{
    [SerializeField] private TextAsset inkJSONAsset; // The Ink JSON file
    [SerializeField] private Canvas canvas; // Canvas where the text will be displayed
    [SerializeField] private Text textPrefab; // Text UI element to display the dialogue

    private Story story;
    private Text currentText; // Holds the reference to the instantiated text object
    private bool hasInteracted = false; // Track if the player has interacted
    private bool playerInRange = false; // Track if player is inside the trigger

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && inkJSONAsset != null && !hasInteracted)
        {
            ShowText();
            playerInRange = true; // Player entered trigger
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HideText();
            playerInRange = false; // Player left trigger
        }
    }

    private void Update()
    {
        if (playerInRange && !hasInteracted && Input.GetKeyDown(KeyCode.E))
        {
            StartDialogue();
        }
    }

    private void ShowText()
    {
        if (currentText == null)
        {
            story = new Story(inkJSONAsset.text);
            if (story.canContinue)
            {
                string text = story.Continue().Trim();
                currentText = Instantiate(textPrefab, canvas.transform);
                currentText.text = text;
            }
        }
    }

    private void HideText()
    {
        if (currentText != null)
        {
            Destroy(currentText.gameObject);
            currentText = null;
        }
    }

    private void StartDialogue()
    {
        HideText(); // Hide text when dialogue starts
        hasInteracted = true; // Mark as interacted so it won’t show again
        InkDialogOnClickIND.OnDialogueEnd += HandleDialogueEnd; // Listen for dialogue end
    }

    private void HandleDialogueEnd(GameObject character)
    {
        if (character == gameObject) // Ensure it's the correct object
        {
            hasInteracted = true; // Ensure text never appears again
            HideText();
            InkDialogOnClickIND.OnDialogueEnd -= HandleDialogueEnd; // Unsubscribe event
        }
    }

    private void OnDisable()
    {
        HideText(); // Make sure the text disappears when the object is disabled
    }

    private void OnDestroy()
    {
        HideText(); // Ensure the text is removed if the object is destroyed
    }
}
