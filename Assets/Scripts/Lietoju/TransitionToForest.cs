using UnityEngine;

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

    private GameObject player;

    void Start()
    {
        // Find the player by tag
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player not found! Please tag the player as 'Player'.");
        }

        // Ensure the background music starts playing
        if (backgroundMusic != null)
        {
            backgroundMusic.volume = 1f; // Start at full volume
            backgroundMusic.Play();
        }

        // Ensure teleport music is not playing initially
        if (teleportMusic != null)
        {
            teleportMusic.Stop();
        }
    }

    void Update()
    {
        if (player == null || backgroundMusic == null) return;

        // Adjust background music volume based on distance, fading smoothly
        float distance = Vector3.Distance(transform.position, player.transform.position);
        float targetVolume = 1f;

        // Calculate target volume based on distance, but only start fading when within fadeDistance
        if (distance <= fadeDistance)
        {
            targetVolume = Mathf.Clamp01(1f - (distance / fadeDistance));
        }
        
        // Smoothly interpolate to the target volume
        backgroundMusic.volume = Mathf.Lerp(backgroundMusic.volume, targetVolume, fadeSpeed * Time.deltaTime);

        // Check if the player is close enough to teleport
        if (distance <= interactionDistance && Input.GetKeyDown(KeyCode.E))
        {
            TeleportPlayer();
        }
    }

    void TeleportPlayer()
    {
        if (teleportTarget == null) return;

        // Stop the background music
        if (backgroundMusic.isPlaying)
        {
            backgroundMusic.Stop();
        }

        // Play the teleport music
        if (teleportMusic != null)
        {
            teleportMusic.Play();
        }

        // Teleport the player
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

        Debug.Log("Player teleported to " + teleportTarget.position);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactionDistance);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, fadeDistance);
    }
}
