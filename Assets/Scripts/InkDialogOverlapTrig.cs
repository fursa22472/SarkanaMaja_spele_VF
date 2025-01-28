using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

public class InkDialogOverlapTrig : MonoBehaviour
{
    [SerializeField] private TextAsset inkJSONAsset; // The Ink JSON file
    [SerializeField] private Canvas canvas; // Canvas where the text will be displayed
    [SerializeField] private Text textPrefab; // Text UI element to display the dialogue

    private Story story;
    private Text currentText; // Holds the reference to the instantiated text object

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && inkJSONAsset != null)
        {
            ShowText();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HideText();
        }
    }

    private void OnDisable()
    {
        HideText(); // Make sure the text disappears when the object is disabled
    }

    private void ShowText()
    {
        if (currentText == null)
        {
            story = new Story(inkJSONAsset.text);
            if (story.canContinue)
            {
                string text = story.Continue().Trim();
                currentText = Instantiate(textPrefab, canvas.transform);
                currentText.text = text;
            }
        }
    }

    private void HideText()
    {
        if (currentText != null)
        {
            Destroy(currentText.gameObject);
            currentText = null;
        }
    }
}
