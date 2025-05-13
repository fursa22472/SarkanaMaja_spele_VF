using System.Collections.Generic;
using UnityEngine;

public class SevenDoors : MonoBehaviour
{
    [Header("Assign exactly 7 unique AudioClips to track.")]
    public List<AudioClip> trackedAudioClips;

    [Header("The bonus clip that reappears the object.")]
    public AudioClip bonusClip;

    [Header("Object that disappears when all 7 have been played.")]
    public GameObject objectToDisappear;

    private HashSet<AudioClip> playedClips = new HashSet<AudioClip>();
    private bool hasDisappeared = false;

    private void Update()
    {
        // Get all AudioSources, including on disabled GameObjects
        AudioSource[] allSources = Resources.FindObjectsOfTypeAll<AudioSource>();

        foreach (var source in allSources)
        {
            if (source == null || source.clip == null) continue;

            AudioClip currentClip = source.clip;

            if (source.isPlaying)
            {
                // Bonus logic: If door is gone and bonus clip is played, bring it back
                if (hasDisappeared && currentClip == bonusClip)
                {
                    objectToDisappear.SetActive(true);
                    hasDisappeared = false;
                    Debug.Log("🎁 Bonus clip played. Object reappeared.");
                    return;
                }

                // Track progress toward disappearance
                if (!hasDisappeared && trackedAudioClips.Contains(currentClip))
                {
                    if (!playedClips.Contains(currentClip))
                    {
                        playedClips.Add(currentClip);
                        Debug.Log($"Audio played: {currentClip.name} ({playedClips.Count}/7)");

                        if (playedClips.Count == 7)
                        {
                            TriggerDisappearance();
                            return;
                        }
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
            hasDisappeared = true;
            Debug.Log("✅ All 7 audio clips played. Object disappeared.");
        }
    }
}
