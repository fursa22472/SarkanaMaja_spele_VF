using UnityEngine;
using System.Collections.Generic;

public class ItemPickup : MonoBehaviour
{
    [Header("General Settings")]
    public string itemName;

    [Header("Pickup Settings (For Items)")]
    public bool isPickableItem = false;
    public GameObject characterObject; // The character variation to switch to
    public string characterTag; // Tag used to track character variations

    private GameObject previousCharacter; // Stores the previous variation
    private Vector3 previousCharacterPosition; // Stores the original position of the previous character
    private bool characterChanged = false;
    private bool isCharacterObjectActive = false; // Tracks if the characterObject is active
    
    [Header("Custom Location for Swap")]
    public Vector3 swapLocation = new Vector3(200f, 200f, 200f); // Assign custom location manually in Inspector

    [Header("Interaction Settings (For Objects)")]
    public bool isInteractableObject = false;
    public string requiredItemName;

    [Header("Audio & Effects")]
    public AudioClip pickupSound;
    private AudioSource audioSource;
    private bool isPlayerNearby = false;
    public InventoryManager inventoryManager;

    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        // ✅ Listen for when the dialogue ends from both dialogue systems
        InkDialogOnClickIND.OnDialogueEnd += (talkedCharacter) => HandleDialogueEnd(talkedCharacter);
        InteractiveCharacterController.OnDialogueEnd += (talkedCharacter) => HandleDialogueEnd(talkedCharacter);
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

        ChangeCharacterVariation();
        gameObject.SetActive(false);
    }

    private void ChangeCharacterVariation()
    {
        Debug.Log("Checking active character in scene with tag: " + characterTag);
        GameObject[] characters = GameObject.FindGameObjectsWithTag(characterTag);
        previousCharacter = null;

        foreach (GameObject character in characters)
        {
            if (character.activeSelf)
            {
                previousCharacter = character;
                previousCharacterPosition = previousCharacter.transform.position; // Store its original position
                Debug.Log("Previous character found and stored: " + previousCharacter.name);
                
                // Move previous character to swap location instead of disabling it
                previousCharacter.transform.position = swapLocation;
                
                // Move characterObject to the previous character's position
                characterObject.transform.position = previousCharacterPosition;
                characterObject.SetActive(true);
                isCharacterObjectActive = true;

                characterChanged = true;
                Debug.Log("Character changed to: " + characterObject.name);
                break;
            }
        }
    }

    public void RestorePreviousVariation()
    {
        Debug.Log("Attempting to restore previous variation...");

        if (!characterChanged)
        {
            Debug.LogWarning("❌ Error: characterChanged is false. No change detected.");
            return;
        }

        if (previousCharacter != null)
        {
            Debug.Log("Restoring previous character: " + previousCharacter.name);

            // Move characterObject to swap location and disable it immediately
            characterObject.transform.position = swapLocation;
            characterObject.SetActive(false);
            isCharacterObjectActive = false;

            // Move previous character back to its original position
            previousCharacter.transform.position = previousCharacterPosition;
            previousCharacter.SetActive(true);

            characterChanged = false;

            Debug.Log("🗑️ Removed " + itemName + " from inventory.");
        }
        else
        {
            Debug.LogWarning("❌ Error: previousCharacter is NULL! Ensure it's assigned in ChangeCharacterVariation().");
        }
    }

    public void HandleDialogueEnd(GameObject talkedCharacter)
    {
        // ✅ Ensure the item and character are removed only if dialogue was with the specific characterObject
        if (isCharacterObjectActive && talkedCharacter == characterObject)
        {
            Debug.Log("💥 Dialogue ended with assigned characterObject, removing it and restoring previous variation...");
            RestorePreviousVariation();

            // ✅ Remove item from inventory after swap
            if (inventoryManager != null && inventoryManager.HasItem(itemName))
            {
                inventoryManager.RemoveItem(itemName);
            }
        }
    }

    void OnDestroy()
    {
        InkDialogOnClickIND.OnDialogueEnd -= (talkedCharacter) => HandleDialogueEnd(talkedCharacter);
        InteractiveCharacterController.OnDialogueEnd -= (talkedCharacter) => HandleDialogueEnd(talkedCharacter);
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
