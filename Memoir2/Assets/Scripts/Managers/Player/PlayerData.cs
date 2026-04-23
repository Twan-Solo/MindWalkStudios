using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class LevelProgress
{
    public bool[] piecesCollected = new bool[4];
    public bool IsComplete() => Array.TrueForAll(piecesCollected, p => p);
}

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;

    public static Action OnProgressChanged;

    public static void EnsureExists()
    {
        if (Instance == null)
        {
            GameObject obj = new GameObject("PlayerData");
            obj.AddComponent<PlayerData>();
        }
    }

    [Header("Player Stats")]
    public int score;
    public GameObject heldObjectPrefab;

    [Header("Health")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("Damage")]
    public float damageCooldown = 1.5f;
    private float lastDamageTime;

    [Header("Death Settings")]
    public string mainMenuSceneName = "MainMenu";

    [Header("Death Screen")]
    public AudioClip deathMusic;

    [Header("Progression")]
    public LevelProgress[] levels = new LevelProgress[3];

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        currentHealth = maxHealth;

        InitializeLevels();
    }

    void InitializeLevels()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            if (levels[i] == null)
                levels[i] = new LevelProgress();
        }
    }

    // ------------------------------
    // HEALTH
    // ------------------------------

    public int GetCurrentHealth() => currentHealth;

    public void TakeDamage(int damageAmount)
    {
        if (Time.time < lastDamageTime + damageCooldown) return;

        lastDamageTime = Time.time;

        currentHealth -= damageAmount;
        if (currentHealth < 0) currentHealth = 0;

        if (HealthCounter.Instance != null)
            HealthCounter.Instance.UpdateHealthDisplay();

        if (currentHealth <= 0)
            HandleDeath();
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        if (HealthCounter.Instance != null)
            HealthCounter.Instance.UpdateHealthDisplay();
    }

    void HandleDeath()
    {
        Debug.Log("Player has died!");

        DeathScreen deathScreen = UnityEngine.Object.FindAnyObjectByType<DeathScreen>();

        if (deathScreen != null)
        {
            if (deathMusic != null)
                deathScreen.deathSound = deathMusic;

            deathScreen.ShowDeathScreen();
        }
        else
        {
            SceneManager.LoadScene(mainMenuSceneName);
        }
    }

    // ------------------------------
    // PROGRESSION
    // ------------------------------

    public void CollectPiece(int levelIndex, int pieceIndex)
    {
        if (levelIndex < 0 || levelIndex >= levels.Length) return;
        if (pieceIndex < 0 || pieceIndex >= 4) return;

        levels[levelIndex].piecesCollected[pieceIndex] = true;

        OnProgressChanged?.Invoke();
    }

    public bool IsLevelComplete(int levelIndex)
    {
        if (levelIndex < 0 || levelIndex >= levels.Length) return false;
        return levels[levelIndex].IsComplete();
    }

    public bool IsGameComplete()
    {
        foreach (var level in levels)
            if (!level.IsComplete()) return false;

        return true;
    }

    // ------------------------------
    // RESET
    // ------------------------------

    public void ResetAllProgress()
    {
        score = 0;
        currentHealth = maxHealth;

        for (int i = 0; i < levels.Length; i++)
        {
            for (int j = 0; j < levels[i].piecesCollected.Length; j++)
            {
                levels[i].piecesCollected[j] = false;
            }
        }

        Debug.Log("FULL GAME RESET");

        OnProgressChanged?.Invoke();
    }
}