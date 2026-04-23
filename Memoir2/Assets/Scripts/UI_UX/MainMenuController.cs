using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("UI Buttons")]
    public Button freePlayButton;
    public Button timeTrialButton;
    public Button quitButton;

    [Header("Fade Transition")]
    public FadeTransition fadeTransition;

    [Header("Scene")]
    public string levelToLoad = "Level1";

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (freePlayButton != null)
            freePlayButton.onClick.AddListener(StartFreePlay);

        if (timeTrialButton != null)
            timeTrialButton.onClick.AddListener(StartTimeTrial);

        if (quitButton != null)
            quitButton.onClick.AddListener(() => Application.Quit());

        // GUARANTEE SYSTEM EXISTS BEFORE RESET
        PlayerData.EnsureExists();
        PlayerData.Instance.ResetAllProgress();

        if (GameManager.Instance != null)
            GameManager.Instance.ResetGameState();
    }

    void StartFreePlay()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.SetFreePlay();

        LoadLevel();
    }

    void StartTimeTrial()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.SetTimeTrial();

        LoadLevel();
    }

    void LoadLevel()
    {
        if (fadeTransition != null)
            fadeTransition.FadeToScene(levelToLoad);
        else
            SceneManager.LoadScene(levelToLoad);
    }
}