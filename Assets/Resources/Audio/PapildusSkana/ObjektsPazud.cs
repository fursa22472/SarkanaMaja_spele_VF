using UnityEngine;
using System.Collections;

public class ObjektsPazud : MonoBehaviour
{
    public AudioSource audioSource; // The AudioSource that plays the audio
    public AudioClip targetClip;    // The specific audio clip that triggers the functionality
    public GameObject targetObject; // The object to make disappear

    private bool audioFinished = false; // Flag to track if the audio has finished
    private bool isVisible = true; // Tracks if the object is visible

    void Start()
    {
        if (audioSource == null || targetObject == null || targetClip == null)
        {
            Debug.LogError("❌ Missing references in ObjektsPazud script!");
            return;
        }

        StartCoroutine(CheckAudioCompletion());
    }
    

    // Coroutine to check when the specific audio clip finishes playing
    private IEnumerator CheckAudioCompletion()
    {
        Debug.Log("🎵 Waiting for target audio to start...");

        // Wait until the audio source starts playing the target clip
        yield return new WaitUntil(() => audioSource.clip == targetClip && audioSource.isPlaying);

        Debug.Log("🎶 Target audio started playing...");

        // Wait until the audio clip actually finishes playing
        yield return new WaitUntil(() => !audioSource.isPlaying);

        audioFinished = true;
        Debug.Log("✅ Audio has finished playing.");

        // If the object is already invisible, hide it
        if (!isVisible)
        {
            HideObject();
        }
    }

void Update()
{
    if (audioFinished && !IsObjectInView())
    {
        HideObject();
    }
}

// Manually check if the object is in the camera view
bool IsObjectInView()
{
    Camera mainCamera = Camera.main;
    Vector3 screenPoint = mainCamera.WorldToViewportPoint(targetObject.transform.position);

    bool isInView = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

    Debug.Log($"👁️ Object is in view: {isInView}");

    return isInView;
}


    // Called when the object is no longer visible by the camera
    private void OnBecameInvisible()
    {
        isVisible = false;
        Debug.Log("🙈 Object is no longer visible.");

        // If the audio has already finished, hide the object
        if (audioFinished)
        {
            HideObject();
        }
    }

    private void HideObject()
    {
        Debug.Log($"🛑 Hiding object: {targetObject.name}");
        targetObject.SetActive(false);
    }
}
