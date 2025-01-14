using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [Header("Pickup Settings")]
    public string itemName; // Name of the item
    public AudioClip pickupSound; // Sound to play when picked up

    private bool isPlayerNearby = false; // Tracks if the player is near the item

    public InventoryManager inventoryManager; // Reference to InventoryManager

    void Update()
    {
        // Check if the player is nearby and presses the E key
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            PickUpItem();
        }
    }

    private void PickUpItem()
    {
        // Play the pickup sound, forced using PlayClipAtPoint
        if (pickupSound != null)
        {
            Debug.Log("Attempting to play pickup sound: " + pickupSound.name);
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
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
}
