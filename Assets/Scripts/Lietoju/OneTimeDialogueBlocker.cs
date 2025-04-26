using UnityEngine;

public class OneTimeDialogueBlocker : MonoBehaviour
{
    [Header("Assign the character's InkDialogOnClickIND script here:")]
    public InkDialogOnClickIND targetDialogueScript;

    private bool locked = false;

    void OnEnable()
    {
        InkDialogOnClickIND.OnDialogueEnd += HandleDialogueEnd;
    }

    void OnDisable()
    {
        InkDialogOnClickIND.OnDialogueEnd -= HandleDialogueEnd;
    }

    void Update()
    {
        if (targetDialogueScript == null || locked)
            return;

        if (targetDialogueScript.playerInRange && !targetDialogueScript.isDialogueActive && Input.GetKeyDown(KeyCode.E))
        {
            targetDialogueScript.StartStoryOnClick();
        }
    }

    void HandleDialogueEnd(GameObject character)
    {
        if (character == targetDialogueScript.gameObject)
        {
            Debug.Log("✅ Dialogue finished. Disabling the dialogue script.");
            locked = true;
            targetDialogueScript.enabled = false; // 🔥 Disable the whole InkDialogOnClickIND script!
        }
    }
}
