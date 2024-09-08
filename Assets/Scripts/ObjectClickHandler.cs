using UnityEngine;

public class ObjectClickHandler : MonoBehaviour
{
    public GameObject dialoguePanel;
    public string dialogueText;

    void OnMouseDown()
    {
        if (dialoguePanel.activeSelf)
        {
            dialoguePanel.SetActive(false);
        }
        else
        {
            dialoguePanel.SetActive(true);
            DialogueManager.Instance.ShowDialogue(dialogueText);
        }
    }
}
