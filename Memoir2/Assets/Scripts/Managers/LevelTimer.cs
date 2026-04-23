using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float startTime = 120f;

    [Header("UI")]
    public TextMeshProUGUI timerText;

    [Header("End UI")]
    public GameObject winPanel;
    public GameObject losePanel;

    [Header("Level Info")]
    public string hubSceneName = "Hub";
    public int levelIndex;

    [Header("Fade Transition")]
    public FadeTransition fadeTransition;

    private float currentTime;
    private bool running;

    void Start()
    {
        // HUB SAFETY
        if (SceneManager.GetActiveScene().name == hubSceneName)
        {
            Destroy(this);
            return;
        }

        // SAFE CHECK
        bool isCompleted =
            PlayerData.Instance != null &&
            PlayerData.Instance.IsLevelComplete(levelIndex);

        // FREE ROAM MODE (COMPLETED LEVEL)
        if (isCompleted)
        {
            Debug.Log("Level completed → Free Roam Mode");

            if (timerText) timerText.gameObject.SetActive(false);
            if (winPanel) winPanel.SetActive(false);
            if (losePanel) losePanel.SetActive(false);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Destroy(this);
            return;
        }

        // TIME TRIAL CHECK
        if (GameManager.Instance == null ||
            GameManager.Instance.currentMode != GameMode.TimeTrial)
        {
            Destroy(this);
            return;
        }

        // START TIMER
        currentTime = startTime;
        running = true;

        if (winPanel) winPanel.SetActive(false);
        if (losePanel) losePanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!running) return;

        currentTime -= Time.deltaTime;

        UpdateUI();

        if (PlayerData.Instance != null &&
            PlayerData.Instance.IsLevelComplete(levelIndex))
        {
            Win();
            return;
        }

        if (currentTime <= 0f)
        {
            Lose();
        }
    }

    void UpdateUI()
    {
        if (!timerText) return;

        int min = Mathf.FloorToInt(currentTime / 60);
        int sec = Mathf.FloorToInt(currentTime % 60);

        timerText.text = $"{min:00}:{sec:00}";
    }

    void Win()
    {
        if (!running) return;

        running = false;

        if (winPanel) winPanel.SetActive(true);

        StartCoroutine(EndLevelFlow());
    }

    void Lose()
    {
        if (!running) return;

        running = false;

        if (losePanel) losePanel.SetActive(true);

        StartCoroutine(EndLevelFlow());
    }

    IEnumerator EndLevelFlow()
    {
        yield return new WaitForSeconds(2f);

        if (fadeTransition != null)
        {
            fadeTransition.FadeToScene(hubSceneName);
        }
        else
        {
            SceneManager.LoadScene(hubSceneName);
        }
    }

    public void StopTimer()
    {
        running = false;
    }

    void OnDisable()
    {
        Time.timeScale = 1f;
    }
}