using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitManager : MonoBehaviour
{
    [SerializeField] private GameObject quitPanel; // Only needed in gameplay scene
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    private bool isPanelActive = false;
    private bool hasStarted = false;

    private void Start()
    {
        if (quitPanel != null)
            quitPanel.SetActive(false);

        // Optional: delay to avoid triggering on scene load input
        Invoke(nameof(EnableInputHandling), 0.1f);
    }

    private void EnableInputHandling()
    {
        hasStarted = true;
    }

    void Update()
    {
        if (!hasStarted) return; // Wait until we're ready

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!IsInMainMenu())
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

    private bool IsInMainMenu()
    {
        return SceneManager.GetActiveScene().name == mainMenuSceneName;
    }
}
