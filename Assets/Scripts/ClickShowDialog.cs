using UnityEngine;
using UnityEngine.UI;

public class ShowPrefabOnClick : MonoBehaviour
{
    [SerializeField] private GameObject prefabToShow; // The prefab to instantiate and show

    private Transform spawnLocation; // The location where the prefab will be instantiated
    private GameObject instantiatedObject;

    // This method will be called when the button or object is clicked
    public void OnClick()
    {
        if (prefabToShow != null && spawnLocation != null)
        {
            if (instantiatedObject == null)
            {
                instantiatedObject = Instantiate(prefabToShow, spawnLocation.position, spawnLocation.rotation);
            }
            else
            {
                instantiatedObject.SetActive(true);
            }
        }
        else
        {
            Debug.LogError("Prefab or spawn location is not assigned.");
        }
    }

    // For UI buttons, you can directly hook this method to the button's OnClick event in the Inspector
    private void Start()
    {
        // Find the MainCamera and use its transform as the spawn location
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            spawnLocation = mainCamera.transform;
        }
        else
        {
            Debug.LogError("Main Camera not found");
        }

        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(OnClick);
        }
    }

    // For in-game objects, use a collider to detect the click
    private void OnMouseDown()
    {
        OnClick();
    }
}
