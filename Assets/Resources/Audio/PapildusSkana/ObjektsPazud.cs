using UnityEngine;

public class ObjektsPazud : MonoBehaviour
{
    public AudioSource audioSource;     // The AudioSource that plays the audio
    public AudioClip targetClip;        // The specific audio clip that triggers the functionality
    public GameObject targetObject;     // The object to make disappear

    private bool hasDisappeared = false;

    void Start()
    {
        if (audioSource == null || targetObject == null || targetClip == null)
        {
            Debug.LogError("❌ Missing references in ObjektsPazud script!");
            return;
        }
    }

    void Update()
    {
        // Check if the correct clip is playing, and it hasn't disappeared yet
        if (!hasDisappeared && audioSource.isPlaying && audioSource.clip == targetClip)
        {
            HideObject();
        }
    }

    private void HideObject()
    {
        if (targetObject != null)
        {
            Debug.Log($"🛑 Hiding object: {targetObject.name}");
            targetObject.SetActive(false);
            hasDisappeared = true;
        }
    }
}
