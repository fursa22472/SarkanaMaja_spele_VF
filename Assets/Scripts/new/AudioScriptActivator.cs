using UnityEngine;

public class AudioBasedScriptActivator : MonoBehaviour
{
    public AudioSource audioSource;  // Attach the AudioSource component
    public AudioClip audioClipA;     // Assign the specific AudioClip for ScriptA
    public AudioClip audioClipB;     // Assign the specific AudioClip for ScriptB
    public ScriptA scriptA;          // Assign ScriptA in the Inspector
    public ScriptB scriptB;          // Assign ScriptB in the Inspector

    void Start()
    {
        // Ensure both scripts are disabled initially
        scriptA.enabled = false;
        scriptB.enabled = false;
    }

    void Update()
    {
        // Check which audio clip is currently playing
        if (audioSource.isPlaying)
        {
            // Enable ScriptA if audioClipA is playing
            if (audioSource.clip == audioClipA)
            {
                scriptA.enabled = true;
                scriptB.enabled = false;
            }
            // Enable ScriptB if audioClipB is playing
            else if (audioSource.clip == audioClipB)
            {
                scriptA.enabled = false;
                scriptB.enabled = true;
            }
        }
        else
        {
            // Disable both scripts when no audio is playing
            scriptA.enabled = false;
            scriptB.enabled = false;
        }
    }
}
