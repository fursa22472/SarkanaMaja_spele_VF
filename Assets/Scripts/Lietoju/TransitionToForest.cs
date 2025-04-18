using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TransitionToForest : MonoBehaviour
{
    [Header("Teleport Settings")]
    public Transform teleportTarget;

    [Header("Audio Settings")]
    public AudioClip teleportMusic; // Only the clip now
    public float fadeDuration = 1.5f;

    [Header("UI Settings")]
    public Image fadeScreen;
    public float fadeScreenDuration = 1.5f;

    [Header("Zone Music Settings")]
    public List<ZoneMusicData> zones = new List<ZoneMusicData>();

    private GameObject player;
    private bool hasTeleported = false;
    private string currentZone = "";

    private InkDialogOnClickIND dialogueManager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player not found! Tag it as 'Player'.");
            return;
        }

        dialogueManager = FindObjectOfType<InkDialogOnClickIND>();
        if (dialogueManager == null)
        {
            Debug.LogError("InkDialogOnClickIND not found!");
            return;
        }

        InkDialogOnClickIND.OnDialogueEnd += HandleDialogueEnd;

        if (fadeScreen == null)
        {
            fadeScreen = GameObject.Find("FadeScreen")?.GetComponent<Image>();
            if (fadeScreen == null)
                Debug.LogError("FadeScreen UI Image not found!");
        }

        if (fadeScreen != null)
            fadeScreen.color = new Color(0, 0, 0, 0);

        RenderSettings.fog = false;
    }

    void Update()
    {
        if (hasTeleported)
        {
            CheckZoneMusic();
        }
    }

    void HandleDialogueEnd(GameObject character)
    {
        if (hasTeleported) return;
        hasTeleported = true;

        InkDialogOnClickIND.OnDialogueEnd -= HandleDialogueEnd;
        StartCoroutine(FadeAndTeleport());
    }

    IEnumerator FadeAndTeleport()
    {
        yield return StartCoroutine(FadeScreen(1f));

        // Play teleport music using persistent manager
        if (teleportMusic != null && PersistentMusicManager.Instance != null)
        {
            PersistentMusicManager.Instance.PlayMusic(teleportMusic);
        }

        // Move the player
        if (teleportTarget != null)
        {
            var controller = player.GetComponent<CharacterController>();
            var rb = player.GetComponent<Rigidbody>();

            if (controller != null)
            {
                controller.enabled = false;
                player.transform.position = teleportTarget.position;
                controller.enabled = true;
            }
            else if (rb != null)
            {
                rb.position = teleportTarget.position;
            }
            else
            {
                player.transform.position = teleportTarget.position;
            }

            RenderSettings.fog = true;
        }

        yield return StartCoroutine(FadeScreen(0f));
    }

    IEnumerator FadeScreen(float targetAlpha)
    {
        float startAlpha = fadeScreen.color.a;
        float elapsed = 0f;

        while (elapsed < fadeScreenDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / fadeScreenDuration);
            fadeScreen.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        fadeScreen.color = new Color(0, 0, 0, targetAlpha);
    }

    void CheckZoneMusic()
    {
        foreach (var zone in zones)
        {
            if (zone.zoneCollider == null || zone.musicClip == null) continue;

            if (zone.zoneCollider.bounds.Contains(player.transform.position))
            {
                if (currentZone != zone.zoneID)
                {
                    currentZone = zone.zoneID;
                    PersistentMusicManager.Instance?.PlayMusic(zone.musicClip);
                }

                return; // early exit if inside a zone
            }
        }
    }

    [System.Serializable]
    public class ZoneMusicData
    {
        public string zoneID;
        public Collider zoneCollider;
        public AudioClip musicClip;
    }
}
