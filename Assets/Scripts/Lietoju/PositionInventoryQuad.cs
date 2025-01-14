using UnityEngine;

public class PositionInventoryQuad : MonoBehaviour
{
    [Header("References")]
    public Camera mainCamera;           // The camera to which the inventory should be positioned relative
    public GameObject inventoryQuad;    // The quad representing the inventory

    [Header("Settings")]
    public float distanceFromCamera = 2f; // How far the quad is from the camera
    public float scaleFactor = 0.8f;      // Controls how large the quad should appear in the camera view

    void Start()
    {
        if (inventoryQuad == null)
        {
            Debug.LogError("Inventory Quad is not assigned.");
            return;
        }
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Automatically use the main camera if it's not assigned
        }
        
        // Initially position and scale the inventory quad
        PositionAndScaleInventoryQuad();
    }

    void Update()
    {
        if (inventoryQuad != null && mainCamera != null)
        {
            // Update the position and scale of the inventory quad every frame
            PositionAndScaleInventoryQuad();
        }
    }

    // This method positions and scales the inventory quad in front of the camera
    private void PositionAndScaleInventoryQuad()
    {
        // Position the quad at a fixed distance from the camera
        Vector3 forward = mainCamera.transform.forward;
        inventoryQuad.transform.position = mainCamera.transform.position + forward * distanceFromCamera;

        // Make sure the quad faces the camera
        inventoryQuad.transform.rotation = Quaternion.LookRotation(forward);

        // Adjust the size of the quad to fit the camera's field of view
        float height = scaleFactor * 2f * Mathf.Tan(mainCamera.fieldOfView * 0.5f * Mathf.Deg2Rad); // Height of the quad
        float width = height * 1.414f; // Width = height * horizontal A4 aspect ratio (1.414:1)

        // Set the scale of the inventory quad
        inventoryQuad.transform.localScale = new Vector3(width, height, 1f); // Keep depth at 1 (flat)
    }
}
