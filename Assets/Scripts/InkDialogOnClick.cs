using System;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class InkDialogOnClick: MonoBehaviour
{
    public static event Action<Story> OnCreateStory;

    [SerializeField] private TextAsset inkJSONAsset = null;
    [SerializeField] private Canvas canvas = null;
    [SerializeField] private Text textPrefab = null;
    [SerializeField] private Button buttonPrefab = null;

    private Story story;
    private CubeMovement2 cubeMovement; // Reference to the player's movement script

    void Awake()
    {
        RemoveChildren();
    }

    void Start()
    {
        // Find the player object and get the CubeMovement2 script
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            cubeMovement = player.GetComponent<CubeMovement2>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartStoryOnClick();
        }
    }

    void OnMouseDown()
    {
        StartStoryOnClick();
    }

    public void StartStoryOnClick()
    {
        if (cubeMovement != null)
        {
            cubeMovement.StopMovement(); // Stop the player's movement
        }

        story = new Story(inkJSONAsset.text);

        if (OnCreateStory != null)
            OnCreateStory(story);

        RefreshView();
    }

    void RefreshView()
    {
        RemoveChildren();

        while (story.canContinue)
        {
            string text = story.Continue().Trim();
            CreateContentView(text);
        }

        if (story.currentChoices.Count > 0)
        {
            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                Choice choice = story.currentChoices[i];
                Button button = CreateChoiceView(choice.text.Trim());

                button.onClick.AddListener(delegate {
                    OnClickChoiceButton(choice);
                });
            }
        }
        else
        {
            Button choice = CreateChoiceView("Close");
            choice.onClick.AddListener(delegate {
                RemoveChildren();
                if (cubeMovement != null)
                {
                    cubeMovement.ResumeMovement(); // Resume the player's movement when closing the story
                }
            });
        }
    }

    void OnClickChoiceButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        RefreshView();
    }

    void CreateContentView(string text)
    {
        Text storyText = Instantiate(textPrefab) as Text;
        storyText.text = text;
        storyText.transform.SetParent(canvas.transform, false);
    }

    Button CreateChoiceView(string text)
    {
        Button choice = Instantiate(buttonPrefab) as Button;
        choice.transform.SetParent(canvas.transform, false);

        Text choiceText = choice.GetComponentInChildren<Text>();
        choiceText.text = text;

        HorizontalLayoutGroup layoutGroup = choice.GetComponent<HorizontalLayoutGroup>();
        layoutGroup.childForceExpandHeight = false;

        return choice;
    }

    void RemoveChildren()
    {
        int childCount = canvas.transform.childCount;
        for (int i = childCount - 1; i >= 0; --i)
        {
            Destroy(canvas.transform.GetChild(i).gameObject);
        }
    }
}
