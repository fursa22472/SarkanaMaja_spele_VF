using UnityEngine;

public class ObjektsPazud : MonoBehaviour
{
    public AudioSource audioSource; // The AudioSource that plays the audio
    public AudioClip targetClip;    // The specific audio clip that triggers the functionality
    public GameObject targetObject; // The object to make disappear

    private bool audioPlayed = false; // Flag to track if the target audio has been played
    private bool objectInView = true; // Tracks if the object is currently in the camera's view

    void Update()
    {
        // Check if the specific audio clip has finished playing
        if (audioSource.clip == targetClip && !audioSource.isPlaying && !audioPlayed)
        {
            audioPlayed = true; // Set the flag once the audio has finished playing
        }

        // If the audio has played, check if the object is in view
        if (audioPlayed)
        {
            objectInView = IsObjectInView();

            // If the object is no longer in view, deactivate it
            if (!objectInView)
            {
                targetObject.SetActive(false);
            }
        }
    }

    bool IsObjectInView()
    {
        // Check if the object is in view using the camera's viewport
        if (targetObject == null) return false;

        Camera mainCamera = Camera.main;
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(targetObject.transform.position);

        // Check if the object is within the camera's viewport
        bool isInView = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

        return isInView;
    }
}
