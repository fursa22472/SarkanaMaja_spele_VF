using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource; // Reference to your AudioSource
    public AudioClip audiopoz; // Reference to your AudioClip 123
    public AudioClip audioneg; // Reference to your AudioClip 234

    public GameObject objectA; // Reference to GameObject A
    public GameObject objectB; // Reference to GameObject B

    private void Start()
    {
        // Ensure objects are inactive at the start
        objectA.SetActive(false);
        objectB.SetActive(false);
    }

    private void Update()
    {
        if (audioSource.isPlaying)
        {
            CheckAudioClip();
        }
    }

    private void CheckAudioClip()
    {
        if (audioSource.clip == audiopoz)
        {
            ShowObject(objectA);
        }
        else if (audioSource.clip == audioneg)
        {
            ShowObject(objectB);
        }
    }

    private void ShowObject(GameObject obj)
    {
        // Hide both objects first (optional, if you want only one visible at a time)
        objectA.SetActive(false);
        objectB.SetActive(false);

        // Show the specified object
        obj.SetActive(true);
    }
}
