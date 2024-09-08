using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public TextAsset inkJSONAsset;
    private DialogueManager_2 dialogueManager;

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager_2>();
    }

    void OnMouseDown()
    {
        dialogueManager.inkJSONAsset = inkJSONAsset;
        dialogueManager.StartStory();
    }
}
