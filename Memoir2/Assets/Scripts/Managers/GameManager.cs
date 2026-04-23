using UnityEngine;

public enum GameMode
{
    FreePlay,
    TimeTrial
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Current Game Mode")]
    public GameMode currentMode = GameMode.FreePlay;

    private void Awake()
    {
        // Singleton safety
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // -------------------------
    // MODE CONTROL
    // -------------------------

    public void SetFreePlay()
    {
        currentMode = GameMode.FreePlay;
        Debug.Log("Game Mode: Free Play");
    }

    public void SetTimeTrial()
    {
        currentMode = GameMode.TimeTrial;
        Debug.Log("Game Mode: Time Trial");
    }

    // -------------------------
    // FULL RESET (MAIN MENU EXIT)
    // -------------------------

    public void ResetGameState()
    {
        currentMode = GameMode.FreePlay;
        Debug.Log("GameManager reset to default state");
    }
}
