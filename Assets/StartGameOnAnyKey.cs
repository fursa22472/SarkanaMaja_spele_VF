
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameOnAnyKey : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Check if any key is pressed
        if (Input.anyKeyDown)
        {
            // Load the main game scene
            SceneManager.LoadScene("MezsArKokiem"); // Replace "MainGameScene" with the name of your main game scene
        }
    }
}
