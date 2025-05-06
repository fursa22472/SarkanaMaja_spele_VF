using UnityEngine;
using UnityEngine.Video;
using System.Collections;
using UnityEngine.SceneManagement; // if not already at top


[RequireComponent(typeof(AudioSource))]
public class PersistentMusicManager : MonoBehaviour
{
    public static PersistentMusicManager Instance;

    [Header("Music Settings")]
    public float fadeDuration = 0.5f;
    public float targetVolume = 1f;

    [Header("Ending Videos to Watch")]
    public VideoClip videoA;
    public VideoClip videoB;

    private AudioSource audioSource;
    private Coroutine currentFade;
    private bool hasForcedFadeOut = false;
    private float volumeMultiplier = 1f; // 👈 Added for proximity dampening

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.volume = 0f;
    }

    void Update()
    {
        // 🎧 Apply real-time volume dampening
        if (audioSource.isPlaying && !hasForcedFadeOut)
        {
            audioSource.volume = targetVolume * volumeMultiplier;
        }

        if (hasForcedFadeOut) return;

        VideoPlayer[] allPlayers = FindObjectsOfType<VideoPlayer>();

        foreach (VideoPlayer vp in allPlayers)
        {
            if (vp.isPlaying && VideoMatches(vp.clip))
            {
                SetupVideoAudio(vp);

                hasForcedFadeOut = true;
                StartCoroutine(FadeOutAndStop());
                Debug.Log("🎬 Detected video playing — fading out zone music.");
                break;
            }
        }
    }

    private bool VideoMatches(VideoClip clip)
    {
        if (clip == null) return false;
        return clip == videoA || clip == videoB ||
               clip.name == videoA?.name || clip.name == videoB?.name;
    }

    private void SetupVideoAudio(VideoPlayer vp)
    {
        if (vp.audioOutputMode != VideoAudioOutputMode.AudioSource)
        {
            vp.audioOutputMode = VideoAudioOutputMode.AudioSource;
        }

        if (vp.GetTargetAudioSource(0) != null) return;

        AudioSource vidAudio = vp.GetComponent<AudioSource>();
        if (vidAudio == null)
        {
            vidAudio = vp.gameObject.AddComponent<AudioSource>();
        }

        vidAudio.playOnAwake = false;
        vidAudio.volume = 1f;
        vidAudio.loop = false;
        vidAudio.mute = false;

        vp.EnableAudioTrack(0, true);
        vp.SetTargetAudioSource(0, vidAudio);

        Debug.Log("🔊 Video audio enabled via AudioSource.");
    }

    private IEnumerator FadeOutAndStop()
    {
        float startVol = audioSource.volume;

        for (float t = 0f; t < fadeDuration; t += Time.deltaTime)
        {
            float lerped = Mathf.Lerp(startVol, 0f, t / fadeDuration);
            audioSource.volume = lerped * volumeMultiplier; // ✅ respect dampening
            yield return null;
        }

        audioSource.Stop();
        audioSource.clip = null;
        audioSource.volume = 0f;
    }

    public void PlayMusic(AudioClip newClip)
    {
        if (hasForcedFadeOut || newClip == null || audioSource.clip == newClip)
            return;

        if (currentFade != null)
            StopCoroutine(currentFade);

        currentFade = StartCoroutine(FadeToNewClip(newClip));
    }

    private IEnumerator FadeToNewClip(AudioClip newClip)
    {
        float startVol = audioSource.volume;

        for (float t = 0f; t < fadeDuration; t += Time.deltaTime)
        {
            float lerped = Mathf.Lerp(startVol, 0f, t / fadeDuration);
            audioSource.volume = lerped * volumeMultiplier;
            yield return null;
        }

        audioSource.Stop();
        audioSource.clip = newClip;
        audioSource.Play();

        for (float t = 0f; t < fadeDuration; t += Time.deltaTime)
        {
            float lerped = Mathf.Lerp(0f, targetVolume, t / fadeDuration);
            audioSource.volume = lerped * volumeMultiplier;
            yield return null;
        }

        audioSource.volume = targetVolume * volumeMultiplier;
    }

    // 👇 Exposed for proximity dampeners to call
    public void SetVolumeMultiplier(float value)
    {
        volumeMultiplier = Mathf.Clamp01(value);
    }








    void OnEnable()
{
    SceneManager.sceneLoaded += OnSceneLoaded;
}

void OnDisable()
{
    SceneManager.sceneLoaded -= OnSceneLoaded;
}

private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
{
    if (scene.name == "MainMenu")
    {
        hasForcedFadeOut = false;
        audioSource.volume = 0f;
        audioSource.clip = null;
        volumeMultiplier = 1f; // 🔥 Reset proximity dampening!
        Debug.Log("🎵 MusicManager reset — ready to play main menu music.");
    }
}


}
