using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
   [System.Serializable]
public class InventoryItem
{
    public string itemName;
    public Texture itemImage;
    public Texture descriptionImage;
    public Vector2 position;
    public Vector2 size = new Vector2(0.1f, 0.1f);
    
    // ✅ New: Assign a material per item
    public Material customMaterial;
}

    public InventoryItem[] allItems;
    public GameObject inventoryQuad;
    public Texture emptyInventoryImage;
    public Camera mainCamera;

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
            inventoryQuad.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleInventory();
        }

        if (isInventoryOpen)
        {
            HandleNumberKeyInput();
        }
    }

    public void AddItemToInventory(string itemName)
    {
        InventoryItem item = System.Array.Find(allItems, i => i.itemName == itemName);

        if (item != null)
        {
            InventoryItem newItem = new InventoryItem
            {
                itemName = item.itemName,
                itemImage = item.itemImage,
                descriptionImage = item.descriptionImage,
                position = item.position,
                size = item.size
            };

            currentItems.Add(newItem);
            Debug.Log($"🛍️ Added {itemName} to inventory.");
            ForceInventoryUpdate();
        }
        else
        {
            Debug.LogWarning($"❌ Item {itemName} not found in allItems array.");
        }
    }

    public bool HasItem(string itemName)
    {
        return currentItems.Exists(item => item.itemName == itemName);
    }

    public void RemoveItem(string itemName)
    {
        InventoryItem item = currentItems.Find(i => i.itemName == itemName);
        if (item != null)
        {
            currentItems.Remove(item);
            Debug.Log($"❌ Removed {itemName} from inventory.");
            ForceInventoryUpdate();
        }
    }

    private void ForceInventoryUpdate()
    {
        ClearItemQuads();
        ShowInventoryOverview();
    }

    private void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;

        if (isInventoryOpen)
        {
            ShowInventoryOverview();
            inventoryQuad.SetActive(true);
        }
        else
        {
            inventoryQuad.SetActive(false);
            ClearItemQuads();
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
            inventoryRenderer.material.mainTexture = emptyInventoryImage;
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
    GameObject itemQuad = GameObject.CreatePrimitive(PrimitiveType.Quad);
    Renderer quadRenderer = itemQuad.GetComponent<Renderer>();

    // 🔄 Load your transparent lit material from Resources folder
    Material baseMat = Resources.Load<Material>("Materials/InterObj_Etalons_Mat");

    if (baseMat == null)
    {
        Debug.LogError("❌ Could not load 'InterObj_Etalons_Mat' from Resources/Materials.");
        return;
    }

    // 🧬 Clone to safely assign unique texture
    Material mat = new Material(baseMat);
    mat.mainTexture = item.itemImage;

    // 🪄 Apply material to quad
    quadRenderer.material = mat;

    // 🧯 Disable shadows
    quadRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
    quadRenderer.receiveShadows = false;

    // 🧭 Position inside inventory
    itemQuad.transform.SetParent(inventoryQuad.transform, false);
    itemQuad.transform.localPosition = new Vector3(
        (item.position.x - 0.5f) * inventoryQuad.transform.localScale.x,
        (item.position.y - 0.5f) * inventoryQuad.transform.localScale.y,
        -0.01f
    );
    itemQuad.transform.localScale = new Vector3(item.size.x, item.size.y, 1);

    itemQuads.Add(itemQuad);

    Debug.Log("✅ Loaded and applied material with texture: " + item.itemName);
}


    private void HandleNumberKeyInput()
    {
        for (int i = 0; i < currentItems.Count; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                if (showingDescription && currentDescriptionIndex == i)
                {
                    ShowInventoryOverview();
                }
                else
                {
                    ShowItemDescription(i);
                }
            }
        }
    }

    private void ShowItemDescription(int index)
    {
        if (index >= 0 && index < currentItems.Count && currentItems[index].descriptionImage != null)
        {
            showingDescription = true;
            currentDescriptionIndex = index;

            foreach (var quad in itemQuads)
            {
                quad.SetActive(false);
            }

            inventoryRenderer.material.mainTexture = currentItems[index].descriptionImage;
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
}
