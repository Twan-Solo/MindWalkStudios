using UnityEngine;
using UnityEngine.UI;

public class LevelPieceHUD : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Text pieceText;

    [Header("Level Index")]
    [SerializeField] private int levelIndex;

    private void OnEnable()
    {
        PlayerData.OnProgressChanged += UpdateDisplay;
    }

    private void OnDisable()
    {
        PlayerData.OnProgressChanged -= UpdateDisplay;
    }

    private void Start()
    {
        UpdateDisplay(); // initial sync
    }

    private void UpdateDisplay()
    {
        if (PlayerData.Instance == null) return;

        int collected = 0;

        for (int i = 0; i < 4; i++)
        {
            if (PlayerData.Instance.levels[levelIndex].piecesCollected[i])
                collected++;
        }

        pieceText.text = collected + " / 4 Pieces";
    }
}