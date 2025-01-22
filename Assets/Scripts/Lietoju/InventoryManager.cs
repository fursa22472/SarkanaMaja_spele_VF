using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    [System.Serializable]
    public class InventoryItem
    {
        public string itemName;           // Name of the item
        public Texture itemImage;         // Image of the item to display in the inventory
        public Texture descriptionImage;  // Image to show when the item is selected
        public AudioClip usageAudio;      // Audio clip that triggers item usage
        public Vector2 position;          // Normalized position (0 to 1) on the quad
        public Vector2 size = new Vector2(0.1f, 0.1f); // Normalized size
    }

    [System.Serializable]
    public class InteractableObject
    {
        public GameObject targetObject;  // The object to interact with
        public string requiredItemName; // The name of the required item
    }

    public InventoryItem[] allItems;           // All possible items in the game
    public GameObject inventoryQuad;           // Quad displaying the inventory background
    public Texture emptyInventoryImage;        // Background when inventory is empty
    public Camera mainCamera;                  // Reference to the main camera
    public AudioSource audioSource;            // Audio source to play audio clips
    public List<InteractableObject> interactableObjects = new(); // List of interactable objects

    private Renderer inventoryRenderer;
    private bool isInventoryOpen = false;
    private bool showingDescription = false;
    private int currentDescriptionIndex = -1;
    private List<GameObject> itemQuads = new();
    private List<InventoryItem> currentItems = new();

    void Start()
    {
        if (inventoryQuad != null)
        {
            inventoryRenderer = inventoryQuad.GetComponent<Renderer>();
            inventoryQuad.SetActive(false); // Start with inventory hidden
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Open/Close inventory
        {
            ToggleInventory();
        }

        if (isInventoryOpen)
        {
            HandleNumberKeyInput();
        }

        HandleObjectInteraction();
    }

    private void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;

        if (isInventoryOpen)
        {
            ShowInventoryOverview();
            PositionInventoryQuad();
            inventoryQuad.SetActive(true);
        }
        else
        {
            inventoryQuad.SetActive(false);
            ClearItemQuads();
        }
    }

    private void HandleNumberKeyInput()
    {
        for (int i = 0; i < currentItems.Count; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                if (showingDescription && currentDescriptionIndex == i)
                {
                    ShowInventoryOverview(); // Return to overview
                }
                else
                {
                    ShowItemDescription(i); // Show item description
                }
            }
        }
    }

    private void HandleObjectInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                foreach (var interactable in interactableObjects)
                {
                    if (hit.collider.gameObject == interactable.targetObject)
                    {
                        TryUseItem(interactable);
                        break;
                    }
                }
            }
        }
    }

    private void TryUseItem(InteractableObject interactable)
    {
        InventoryItem itemToUse = currentItems.Find(item => item.itemName == interactable.requiredItemName);

        if (itemToUse != null)
        {
            currentItems.Remove(itemToUse);
            Debug.Log($"Used {itemToUse.itemName} on {interactable.targetObject.name}.");

            // Perform some action with the targetObject (but leave it active)
            Debug.Log($"Interacted with {interactable.targetObject.name} successfully.");

            ShowInventoryOverview();
        }
        else
        {
            Debug.LogWarning($"Required item {interactable.requiredItemName} not found in inventory.");
        }
    }

    private void ShowInventoryOverview()
    {
        showingDescription = false;
        currentDescriptionIndex = -1;

        if (currentItems.Count == 0)
        {
            inventoryRenderer.material.mainTexture = emptyInventoryImage;
        }
        else
        {
            inventoryRenderer.material.mainTexture = emptyInventoryImage; // Use background as base
            DrawInventoryItems();
        }
    }

    private void DrawInventoryItems()
    {
        ClearItemQuads();

        foreach (var item in currentItems)
        {
            if (item.itemImage != null)
            {
                CreateItemQuad(item);
            }
        }
    }

    private void CreateItemQuad(InventoryItem item)
    {
        Vector3 quadSize = inventoryQuad.transform.localScale;

        Vector3 localPositionOnQuad = new Vector3(
            Mathf.Clamp01(item.position.x) * quadSize.x - quadSize.x / 2,
            Mathf.Clamp01(item.position.y) * quadSize.y - quadSize.y / 2,
            -0.01f // Slightly offset to avoid z-fighting
        );

        GameObject itemQuad = GameObject.CreatePrimitive(PrimitiveType.Quad);

        Material itemMaterial = new Material(Shader.Find("Standard"))
        {
            mainTexture = item.itemImage
        };

        // Set transparency settings for the material
        itemMaterial.SetFloat("_Mode", 3); // Transparent mode
        itemMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        itemMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        itemMaterial.SetInt("_ZWrite", 0); // Disable depth writing
        itemMaterial.DisableKeyword("_ALPHATEST_ON");
        itemMaterial.EnableKeyword("_ALPHABLEND_ON");
        itemMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        itemMaterial.renderQueue = 3000; // Transparency render queue
        itemMaterial.SetFloat("_Glossiness", 0.0f); // No glossiness
        itemMaterial.SetFloat("_Metallic", 0.0f);   // No metallic shine

        Renderer quadRenderer = itemQuad.GetComponent<Renderer>();
        quadRenderer.material = itemMaterial;

        Vector3 scaledSize = new Vector3(
            Mathf.Clamp01(item.size.x) * quadSize.x,
            Mathf.Clamp01(item.size.y) * quadSize.y,
            1
        );
        itemQuad.transform.localScale = scaledSize;

        itemQuad.transform.SetParent(inventoryQuad.transform, false);
        itemQuad.transform.localPosition = localPositionOnQuad;

        itemQuads.Add(itemQuad); // Add quad to list
    }

    private void ShowItemDescription(int index)
    {
        if (index >= 0 && index < currentItems.Count && currentItems[index].descriptionImage != null)
        {
            showingDescription = true;
            currentDescriptionIndex = index;

            // Hide all item quads
            foreach (var quad in itemQuads)
            {
                quad.SetActive(false); // Disable all item quads
            }

            // Show the description image
            inventoryRenderer.material.mainTexture = currentItems[index].descriptionImage;
        }
    }

    private void ClearItemQuads()
    {
        foreach (var quad in itemQuads)
        {
            Destroy(quad); // Destroy the quads
        }
        itemQuads.Clear(); // Clear the list of item quads
    }

    private void PositionInventoryQuad()
    {
        inventoryQuad.transform.position = mainCamera.transform.position + mainCamera.transform.forward * 2;
        inventoryQuad.transform.rotation = Quaternion.LookRotation(mainCamera.transform.forward);
    }

    public void AddItemToInventory(string itemName)
    {
        InventoryItem item = System.Array.Find(allItems, i => i.itemName == itemName);

        if (item != null && !currentItems.Contains(item))
        {
            currentItems.Add(item);
            Debug.Log($"Added {itemName} to inventory.");
        }
        else
        {
            Debug.LogWarning($"Item {itemName} not found or already in inventory.");
        }
    }
}
