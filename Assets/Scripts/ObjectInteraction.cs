using UnityEngine;
using UnityEngine.UI;

public class ObjectInteraction : MonoBehaviour
{
    public Text dialogueText; // Reference to the UI Text object
    public AudioClip interactionSound; // Sound to play when interacting with the object
    private AudioSource audioSource; // Reference to the AudioSource component

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnMouseDown()
    {
        // Toggle the visibility of the UI Text object
        dialogueText.enabled = !dialogueText.enabled;

        // Play the interaction sound
        if (interactionSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(interactionSound);
        }
    }
}
