using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [Header("General Settings")]
    public string itemName; // The name of the item

    [Header("Pickup Settings (For Items)")]
    public bool isPickableItem = false; // If true, this is a pickup item
    public GameObject objectToEnable; // The new object that appears after picking up
    public GameObject objectToDisable; // The old object that disappears after picking up

    [Header("Interaction Settings (For Objects)")]
    public bool isInteractableObject = false; // If true, this is an interactable object
    public string requiredItemName; // The item required to interact

    [Header("Audio & Effects")]
    public AudioClip pickupSound;
    private AudioSource audioSource;

    private bool isPlayerNearby = false;
    public InventoryManager inventoryManager; // Reference to InventoryManager

    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (isPickableItem)
            {
                PickUpItem();
            }
            else if (isInteractableObject)
            {
                TryInteract();
            }
        }
    }

    private void PickUpItem()
    {
        if (inventoryManager != null)
        {
            inventoryManager.AddItemToInventory(itemName);
        }

        Debug.Log($"🛍️ Picked up: {itemName}");
        if (pickupSound != null) audioSource.PlayOneShot(pickupSound);

        // ✅ Enable new object (e.g., making an interactable object appear)
        if (objectToEnable != null) objectToEnable.SetActive(true);
        if (objectToDisable != null) objectToDisable.SetActive(false);

        gameObject.SetActive(false); // Hide the item after pickup
    }

    private void TryInteract()
    {
        if (inventoryManager == null) return;

        if (inventoryManager.HasItem(requiredItemName))
        {
            Debug.Log($"✅ {requiredItemName} used on {gameObject.name}");
            inventoryManager.RemoveItem(requiredItemName);
        }
        else
        {
            Debug.LogWarning($"❌ You need {requiredItemName} to interact with {gameObject.name}");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}
