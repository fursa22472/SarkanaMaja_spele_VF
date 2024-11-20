using UnityEngine;

public class BildeParadasGo : MonoBehaviour
{
    public AudioSource audioSource; // The AudioSource that plays the audio
    public AudioClip targetClip;    // The specific audio clip that triggers the image to appear
    public GameObject imageObject;  // The GameObject to show (attached to the camera)

    private bool isShown = false;   // Flag to ensure the object only appears once

    void Start()
    {
        if (imageObject != null)
        {
            imageObject.SetActive(false); // Ensure the image is initially hidden
        }
    }

    void Update()
    {
        // Check if the specific audio clip is playing
        if (audioSource.clip == targetClip && audioSource.isPlaying && !isShown)
        {
            ShowImage(); // Show the image
        }
    }

    void ShowImage()
    {
        if (imageObject != null)
        {
            imageObject.SetActive(true); // Enable the image object
            isShown = true;             // Prevent multiple activations
        }
    }
}
