using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource; // Reference to your AudioSource
    public AudioClip audio123; // Reference to your AudioClip 123
    public AudioClip audio234; // Reference to your AudioClip 234

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
        if (audioSource.clip == audio123)
        {
            ShowObject(objectA);
        }
        else if (audioSource.clip == audio234)
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
