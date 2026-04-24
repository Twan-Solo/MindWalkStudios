using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsMenu : MonoBehaviour
{
    [Header("Scene")]
    public string mainMenuSceneName = "MainMenu";

    [Header("Fade Transition (Optional)")]
    public FadeTransition fadeTransition;

    private void Start()
    {
        // FORCE CURSOR ON FOR CREDITS
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 1f;
    }

    public void ReturnToMainMenu()
    {
        // extra safety in case anything locked it
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 1f;

        if (fadeTransition != null)
        {
            fadeTransition.FadeToScene(mainMenuSceneName);
        }
        else
        {
            SceneManager.LoadScene(mainMenuSceneName);
        }
    }
}