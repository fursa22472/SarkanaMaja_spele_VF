using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;

public class DialogueManager_2 : MonoBehaviour
{
    public TextAsset inkJSONAsset;
    private Story story;

    public Text dialogueText;
    public GameObject[] choiceButtons;

    void Start()
    {
        if (inkJSONAsset != null)
        {
            StartStory();
        }
    }

    public void StartStory()
    {
        story = new Story(inkJSONAsset.text);
        RefreshView();
    }

    void RefreshView()
    {
        // Clear existing choice buttons
        foreach (var button in choiceButtons)
        {
            button.SetActive(false);
            button.GetComponent<Button>().onClick.RemoveAllListeners();
        }

        // Display the current dialogue line
        if (story.canContinue)
        {
            dialogueText.text = story.Continue();
        }

        // Display choices, if any
        if (story.currentChoices.Count > 0)
        {
            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                choiceButtons[i].SetActive(true);
                choiceButtons[i].GetComponentInChildren<Text>().text = story.currentChoices[i].text;
                int choiceIndex = i;
                choiceButtons[i].GetComponent<Button>().onClick.AddListener(() => OnClickChoiceButton(choiceIndex));
            }
        }
    }

    void OnClickChoiceButton(int choiceIndex)
    {
        story.ChooseChoiceIndex(choiceIndex);
        RefreshView();
    }
}
