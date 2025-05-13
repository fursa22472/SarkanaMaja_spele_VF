using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitManager : MonoBehaviour
{
    [SerializeField] private GameObject quitPanel; // Only needed in gameplay scene
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    private bool isPanelActive = false;

    private void Start()
    {
        if (quitPanel != null)
            quitPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsInMainMenu())
            {
                QuitGame(); // Quit the whole app
            }
            else
            {
                ToggleQuitPanel();
            }
        }

        if (!IsInMainMenu() && isPanelActive && Input.GetKeyDown(KeyCode.E))
        {
            LoadMainMenu();
        }
    }

    private void ToggleQuitPanel()
    {
        if (quitPanel == null)
        {
            Debug.LogWarning("Quit Panel is not assigned in this scene.");
            return;
        }

        isPanelActive = !isPanelActive;
        quitPanel.SetActive(isPanelActive);
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    private void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // For testing in Editor
#endif
    }

    private bool IsInMainMenu()
    {
        return SceneManager.GetActiveScene().name == mainMenuSceneName;
    }
}
