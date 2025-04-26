using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class EndingVideoAfterAudio : MonoBehaviour
{
    [System.Serializable]
    public class AudioVideoPair
    {
        public AudioClip audioClip;
        public VideoClip videoClip;
    }

    [Header("References")]
    public AudioSource[] monitoredAudios; // ✅ Monitor these audio sources
    public VideoPlayer videoPlayer;       // 🎬 Assign this in Inspector
    public Image blackScreenImage;        // 🖤 UI image used for fade

    [Header("Ending Pairs")]
    public AudioVideoPair[] endings;      // 🎧 Audio → 🎥 Video pairs

    [Header("Settings")]
    public string mainMenuSceneName = "MainMenu";
    public float fadeDuration = 1.5f;

    [Header("UI References")]
public GameObject dialogueCanvas; // 🎯 Drag your dialogue UI here


    private bool isRunning = false;
    private AudioVideoPair currentMatch = null;
    private AudioSource currentSource = null;

    void Update()
    {
        if (isRunning) return;

        foreach (AudioSource source in monitoredAudios)
        {
            if (source != null && source.isPlaying)
            {
                foreach (var pair in endings)
                {
                    if (source.clip == pair.audioClip)
                    {
                        currentMatch = pair;
                        currentSource = source;
                        StartCoroutine(HandleEnding(pair));
                        return;
                    }
                }
            }
        }
    }

    private IEnumerator HandleEnding(AudioVideoPair pair)
    {
        isRunning = true;

        Debug.Log($"🎧 Matched audio: {pair.audioClip.name} → 🎥 Playing video: {pair.videoClip.name}");

        // Wait for the matched audio to finish
        while (currentSource != null && currentSource.isPlaying)
        {
            yield return null;
        }

        // Fade to black
        yield return StartCoroutine(FadeImage(0f, 1f));
        yield return new WaitForSeconds(0.2f);

      // Assign video clip
videoPlayer.clip = pair.videoClip;

// ✅ Disable dialogue UI
if (dialogueCanvas != null)
{
    dialogueCanvas.SetActive(false);
}

// Set up video audio
SetupVideoAudio();


        // Prepare and play
        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }

        videoPlayer.Play();
        Debug.Log("🎬 Video started");

        // Fade in
        yield return StartCoroutine(FadeImage(1f, 0f));

        // Wait for video to finish or any key press
        while (videoPlayer.isPlaying && !Input.anyKeyDown)
        {
            yield return null;
        }

        if (videoPlayer.isPlaying)
        {
            Debug.Log("🎬 Video skipped by player.");
        }

        // Optional: Stop video if skipped
        videoPlayer.Stop();

        // Load main menu
        SceneManager.LoadScene(mainMenuSceneName);
    }

    private void SetupVideoAudio()
    {
        if (videoPlayer.audioOutputMode != VideoAudioOutputMode.AudioSource)
        {
            videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        }

        // Add or get AudioSource
        AudioSource videoAudio = videoPlayer.GetComponent<AudioSource>();
        if (videoAudio == null)
        {
            videoAudio = videoPlayer.gameObject.AddComponent<AudioSource>();
        }

        videoAudio.playOnAwake = false;
        videoAudio.volume = 1f;
        videoAudio.loop = false;
        videoAudio.mute = false;

        // Link audio track to source
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, videoAudio);
    }

    private IEnumerator FadeImage(float fromAlpha, float toAlpha)
    {
        float time = 0f;
        Color color = blackScreenImage.color;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(fromAlpha, toAlpha, time / fadeDuration);
            blackScreenImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        blackScreenImage.color = new Color(color.r, color.g, color.b, toAlpha);
    }
}
