using UnityEngine;

public class InteractiveCharacterPIANO : MonoBehaviour
{
    public GameObject indicatorPrefab; // Prefab for the indicator
    private GameObject indicatorInstance; // Instance of the indicator
    private Collider characterCollider; // Collider of the character

    private void Start()
    {
        // Initially, the indicator is not visible
        if (indicatorPrefab != null)
        {
            indicatorInstance = Instantiate(indicatorPrefab, transform.position + Vector3.up * 2, Quaternion.identity, transform);
            indicatorInstance.SetActive(false);
        }

        // Get the collider component
        characterCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player entered the trigger and dialogue is not active
        if (other.CompareTag("Player"))
        {
            // Show the indicator
            if (indicatorInstance != null)
            {
                indicatorInstance.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player exited the trigger
        if (other.CompareTag("Player"))
        {
            // Hide the indicator
            if (indicatorInstance != null)
            {
                indicatorInstance.SetActive(false);
            }
        }
    }

    public void SetDialogueActive(bool active)
    {
        // Hide the indicator and disable the collider when dialogue starts
        if (indicatorInstance != null)
        {
            indicatorInstance.SetActive(!active);
        }

        if (characterCollider != null)
        {
            characterCollider.enabled = !active;
        }
    }
}
