using System.Collections.Generic;
using UnityEngine;

public class SevenDoors : MonoBehaviour
{
    [Header("Assign exactly 7 unique AudioClips to track.")]
    public List<AudioClip> trackedAudioClips;

    [Header("Object that disappears when all 7 have been played.")]
    public GameObject objectToDisappear;

    private HashSet<AudioClip> playedClips = new HashSet<AudioClip>();

    private void Update()
    {
        // Get all AudioSources, including on disabled GameObjects
        AudioSource[] allSources = Resources.FindObjectsOfTypeAll<AudioSource>();

        foreach (var source in allSources)
        {
            if (source == null || source.clip == null) continue;

            // Only count if it's playing AND the GameObject is active
            if (source.isPlaying && trackedAudioClips.Contains(source.clip))
            {
                if (!playedClips.Contains(source.clip))
                {
                    playedClips.Add(source.clip);
                    Debug.Log($"Audio played: {source.clip.name} ({playedClips.Count}/7)");

                    if (playedClips.Count == 7)
                    {
                        TriggerDisappearance();
                        return;
                    }
                }
            }
        }
    }

    private void TriggerDisappearance()
    {
        if (objectToDisappear != null && objectToDisappear.activeSelf)
        {
            objectToDisappear.SetActive(false);
            Debug.Log("✅ All 7 audio clips played. Object disappeared.");
        }

        enabled = false; // Stop checking after success
    }
}
