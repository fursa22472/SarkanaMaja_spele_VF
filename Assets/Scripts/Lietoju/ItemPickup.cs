using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [Header("Pickup Settings")]
    public string itemName; // Name of the item
    public AudioClip pickupSound; // Sound to play when picked up
    public GameObject object1; // Default object
    public GameObject object2; // Object to show when sound plays

    private bool isPlayerNearby = false; // Tracks if the player is near the item
    private bool isSoundPlaying = false; // Tracks if the pickup sound is playing
    private AudioSource audioSource; // Reference to an audio source for playback

    public InventoryManager inventoryManager; // Reference to InventoryManager

    void Start()
    {
        // Ensure object1 is active and object2 is hidden at the start
        if (object1 != null) object1.SetActive(true);
        if (object2 != null) object2.SetActive(false);

        // Create an AudioSource component for playing sounds
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        // Check if the player is nearby and presses the E key
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            PickUpItem();
        }

        // Switch objects based on whether the sound is playing
        if (isSoundPlaying)
        {
            SwitchToObject2();
        }
        else
        {
            SwitchToObject1();
        }
    }

    private void PickUpItem()
    {
        // Play the pickup sound
        if (pickupSound != null)
        {
            Debug.Log("Attempting to play pickup sound: " + pickupSound.name);
            audioSource.clip = pickupSound;
            audioSource.Play();
            isSoundPlaying = true;

            // Stop tracking the sound after it finishes
            Invoke(nameof(StopSound), pickupSound.length);
        }
        else
        {
            Debug.LogWarning("No pickup sound assigned for: " + itemName);
        }

        // Add the item to the inventory
        if (inventoryManager != null)
        {
            inventoryManager.AddItemToInventory(itemName);
        }

        // Log the pickup action
        Debug.Log("Picked up: " + itemName);

        // Hide the item from the scene
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            Debug.Log("Player entered trigger for: " + itemName);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object leaving the trigger is the player
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            Debug.Log("Player exited trigger for: " + itemName);
        }
    }

    private void SwitchToObject1()
    {
        if (object1 != null && !object1.activeSelf)
        {
            object1.SetActive(true);
            if (object2 != null) object2.SetActive(false);
        }
    }

    private void SwitchToObject2()
    {
        if (object2 != null && !object2.activeSelf)
        {
            object2.SetActive(true);
            if (object1 != null) object1.SetActive(false);
        }
    }

    private void StopSound()
    {
        isSoundPlaying = false;
    }
}
