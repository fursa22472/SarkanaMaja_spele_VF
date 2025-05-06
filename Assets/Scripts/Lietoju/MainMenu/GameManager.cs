using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Button tutorialButton;
    public Button newGameButton;
    public GameObject tutorialPanel; // MANUALLY ASSIGN THIS IN UNITY

    private Button[] buttons;
    private int selectedButtonIndex = 0;

 

    private bool isTutorialOpen = false; // Tracks tutorial visibility

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
           
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        
         Cursor.lockState = CursorLockMode.Locked; // Locks the cursor to the center of the screen
    Cursor.visible = false;                   // Hides the cursor
    }

    void Update()
    {
        // Prevents any actions in non-MainMenu scenes
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            return;
        }

        if (buttons == null || buttons.Length == 0) return;

        HandleKeyboardNavigation();

        // Press "E" to show/hide tutorial ONLY in MainMenu
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isTutorialOpen)
            {
                CloseTutorial();
            }
            else
            {
                ActivateSelectedButton();
            }
        }
    }

    void HandleKeyboardNavigation()
    {
        if (buttons == null || buttons.Length == 0) return;

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedButtonIndex = (selectedButtonIndex + 1) % buttons.Length;
            SelectButton(selectedButtonIndex);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedButtonIndex = (selectedButtonIndex - 1 + buttons.Length) % buttons.Length;
            SelectButton(selectedButtonIndex);
        }
    }

    void SelectButton(int index)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i] != null)
            {
                // Reset to Unity's preset colors instead of forcing yellow
                buttons[i].colors = buttons[i].colors;
            }
        }

        if (buttons[index] != null)
        {
            EventSystem.current.SetSelectedGameObject(buttons[index].gameObject);
        }
    }

    void ActivateSelectedButton()
    {
        if (buttons[selectedButtonIndex] == newGameButton)
        {
            StartNewGame();
        }
        else if (buttons[selectedButtonIndex] == tutorialButton)
        {
            OpenTutorial();
        }
    }

public void StartNewGame()
{
    Debug.Log("Starting new game... Loading LoadingScene first...");
    SceneManager.LoadScene("LoadingScene"); // <-- change this to your actual loading scene name
}


    public void OpenTutorial()
    {
        if (tutorialPanel == null)
        {
            Debug.LogError("TutorialPanel is missing! Make sure it is assigned in Unity.");
            return;
        }

        tutorialPanel.SetActive(true);
        isTutorialOpen = true;
        Debug.Log("Tutorial Panel is now visible.");
    }

    public void CloseTutorial()
    {
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(false);
            isTutorialOpen = false;
            Debug.Log("Tutorial Panel is now hidden.");
        }
    }

    public void QuitToMainMenu()
    {
        Debug.Log("Returning to Main Menu...");
        SceneManager.LoadScene("MainMenu");
        Invoke("FindUIElements", 0.2f);
    }

    private void FindUIElements()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            tutorialButton = GameObject.Find("TutorialButton")?.GetComponent<Button>();
            newGameButton = GameObject.Find("NewGameButton")?.GetComponent<Button>();

            if (tutorialButton == null || newGameButton == null)
            {
                Debug.LogError("UI Elements not found! Check names in Unity.");
                return;
            }

            buttons = new Button[] { tutorialButton, newGameButton };

            if (tutorialPanel == null)
            {
                Debug.LogError("Tutorial Panel is missing! Make sure it is manually assigned in Unity.");
            }
            else
            {
                tutorialPanel.SetActive(false); // Ensure it starts hidden
                Debug.Log("Tutorial Panel is set up correctly.");
            }

            SelectButton(0);
        }
    }







    void OnEnable()
{
    SceneManager.sceneLoaded += OnSceneLoaded;
}

void OnDisable()
{
    SceneManager.sceneLoaded -= OnSceneLoaded;
}

private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
{
    if (scene.name == "MainMenu")
    {
        Invoke("FindUIElements", 0.1f); // Delay slightly to ensure objects are loaded
    }
}

}
