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

    public InventoryItem[] allItems;   // All possible items in the game
    public GameObject inventoryQuad;   // Quad displaying the inventory background
    public Texture emptyInventoryImage; // Background when inventory is empty
    public Camera mainCamera;          // Reference to the main camera
    public AudioSource audioSource;    // Audio source to play audio clips

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

        Material itemMaterial = new Material(Shader.Find("Standard"));
        itemMaterial.mainTexture = item.itemImage;

        itemMaterial.SetFloat("_Mode", 3);
        itemMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        itemMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        itemMaterial.SetInt("_ZWrite", 0);
        itemMaterial.DisableKeyword("_ALPHATEST_ON");
        itemMaterial.EnableKeyword("_ALPHABLEND_ON");
        itemMaterial.renderQueue = 3000;

        itemMaterial.SetFloat("_Glossiness", 0.5f);
        itemMaterial.SetFloat("_Metallic", 0.2f);

        Renderer quadRenderer = itemQuad.GetComponent<Renderer>();
        quadRenderer.material = itemMaterial;

        // Enable shadow reception and casting
        quadRenderer.receiveShadows = true;
        quadRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;

        Vector3 scaledSize = new Vector3(
            Mathf.Clamp01(item.size.x) * quadSize.x,
            Mathf.Clamp01(item.size.y) * quadSize.y,
            1
        );
        itemQuad.transform.localScale = scaledSize;

        itemQuad.transform.SetParent(inventoryQuad.transform, false);
        itemQuad.transform.localPosition = localPositionOnQuad;
        itemQuad.transform.localRotation = Quaternion.identity;

        itemQuads.Add(itemQuad);
    }

    private void ShowItemDescription(int index)
    {
        if (index >= 0 && index < currentItems.Count && currentItems[index].descriptionImage != null)
        {
            showingDescription = true;
            currentDescriptionIndex = index;

            foreach (var quad in itemQuads) quad.SetActive(false);
            inventoryRenderer.material.mainTexture = currentItems[index].descriptionImage;

            PlayItemAudio(currentItems[index]);
        }
    }

    private void PlayItemAudio(InventoryItem item)
    {
        if (item.usageAudio != null && audioSource != null)
        {
            audioSource.clip = item.usageAudio;
            audioSource.Play();

            Invoke(nameof(RemoveUsedItem), item.usageAudio.length);
        }
    }

    private void RemoveUsedItem()
    {
        if (currentDescriptionIndex >= 0 && currentDescriptionIndex < currentItems.Count)
        {
            currentItems.RemoveAt(currentDescriptionIndex);
            ShowInventoryOverview();
        }
    }

    private void ClearItemQuads()
    {
        foreach (var quad in itemQuads)
        {
            Destroy(quad);
        }
        itemQuads.Clear();
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
