using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource; // The AudioSource for character dialogue
    public AudioClip audioA; // The specific character audio
    public GameObject objectA; // The character to appear

    private bool hasPlayedAudioA = false;

    private void Start()
    {
        objectA.SetActive(false); // Ensure character starts hidden
    }

    private void Update()
    {
        // If the assigned audio starts playing, show the character
        if (audioSource.isPlaying && audioSource.clip == audioA && !hasPlayedAudioA)
        {
            objectA.SetActive(true); // Show the character
            hasPlayedAudioA = true; // Ensure the character stays visible after the first activation
            Debug.Log("Character Activated: " + objectA.name);
        }
    }
}
