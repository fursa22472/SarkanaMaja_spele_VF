using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TransitionToForest : MonoBehaviour
{
    [Header("Teleport Settings")]
    public Transform teleportTarget; // The point to teleport the player to
    public float interactionDistance = 3f; // Maximum distance for interaction

    [Header("Audio Settings")]
    public AudioSource backgroundMusic; // The initial background music
    public AudioSource teleportMusic; // The music to play after teleporting
    public float fadeDistance = 5f; // Distance at which the music starts fading
    public float fadeSpeed = 1f; // Speed of the fade effect

    [Header("UI Settings")]
    public Image fadeScreen; // UI Image for fade effect
    public float fadeDuration = 1.5f;

    private GameObject player;
    private InkDialogOnClickIND dialogueManager;
    private bool hasTeleported = false; // Ensures teleportation happens only once

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player not found! Please tag the player as 'Player'.");
        }

        dialogueManager = FindObjectOfType<InkDialogOnClickIND>();
        if (dialogueManager == null)
        {
            Debug.LogError("InkDialogueManager not found in scene!");
        }

        // Subscribe to dialogue end event
        InkDialogOnClickIND.OnDialogueEnd += HandleDialogueEnd;

        if (backgroundMusic != null)
        {
            backgroundMusic.volume = 1f;
            backgroundMusic.Play();
        }

        if (teleportMusic != null)
        {
            teleportMusic.Stop();
        }

        if (fadeScreen == null)
        {
            fadeScreen = GameObject.Find("FadeScreen")?.GetComponent<Image>();
            if (fadeScreen == null)
            {
                Debug.LogError("FadeScreen UI Image not found! Make sure it's in the scene.");
            }
        }

        if (fadeScreen != null)
        {
            fadeScreen.color = new Color(0, 0, 0, 0);
        }
    }

    void HandleDialogueEnd(GameObject character)
    {
        if (hasTeleported) return; // Prevent multiple teleports

        Debug.Log("Dialogue ended. Checking teleport conditions...");

        // Ensure teleport only happens once
        hasTeleported = true;

        // Unsubscribe from the event so it won't trigger again
        InkDialogOnClickIND.OnDialogueEnd -= HandleDialogueEnd;

        StartCoroutine(FadeAndTeleport());
    }

    IEnumerator FadeAndTeleport()
    {
        yield return StartCoroutine(FadeScreen(1f));

        if (backgroundMusic.isPlaying) backgroundMusic.Stop();
        if (teleportMusic != null) teleportMusic.Play();

        if (teleportTarget != null)
        {
            CharacterController controller = player.GetComponent<CharacterController>();
            Rigidbody rb = player.GetComponent<Rigidbody>();
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
        }

        yield return StartCoroutine(FadeScreen(0f));
    }

    IEnumerator FadeScreen(float targetAlpha)
    {
        float startAlpha = fadeScreen.color.a;
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            fadeScreen.color = new Color(0, 0, 0, newAlpha);
            yield return null;
        }
        fadeScreen.color = new Color(0, 0, 0, targetAlpha);
    }
}
