using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsMenu : MonoBehaviour
{
    [Header("Scene")]
    public string mainMenuSceneName = "MainMenu";

    [Header("Fade Transition (Optional)")]
    public FadeTransition fadeTransition;

    // Called by button
    public void ReturnToMainMenu()
    {
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