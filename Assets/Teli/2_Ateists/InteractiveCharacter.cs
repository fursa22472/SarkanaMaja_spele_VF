using UnityEngine;

public class InteractiveCharacter : MonoBehaviour
{
    [Header("Indicator Settings")]
    public GameObject indicatorPrefab; // Prefab for the indicator
    public Vector3 indicatorLocalPosition = new Vector3(-0.16f, 1.66f, -0.11f);
    public Vector3 indicatorLocalRotation = new Vector3(0f, 321.4f, 0f);
    public Vector3 indicatorLocalScale = Vector3.one;

    private GameObject indicatorInstance; // Instance of the indicator
    private Collider characterCollider;   // Collider of the character

    private void Start()
    {
        characterCollider = GetComponent<Collider>();

        if (indicatorPrefab != null)
        {
            indicatorInstance = Instantiate(indicatorPrefab, transform);
            indicatorInstance.transform.localPosition = indicatorLocalPosition;
            indicatorInstance.transform.localEulerAngles = indicatorLocalRotation;
            indicatorInstance.transform.localScale = indicatorLocalScale;

            indicatorInstance.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && indicatorInstance != null)
        {
            indicatorInstance.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && indicatorInstance != null)
        {
            indicatorInstance.SetActive(false);
        }
    }

    public void SetDialogueActive(bool active)
    {
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
