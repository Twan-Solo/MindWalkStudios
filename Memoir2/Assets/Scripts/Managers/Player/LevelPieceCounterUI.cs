using UnityEngine;
using TMPro;

public class LevelPieceCounterUI : MonoBehaviour
{
    [SerializeField] private int levelIndex;
    [SerializeField] private TMP_Text counterText;

    private void OnEnable()
    {
        PlayerData.OnProgressChanged += UpdateDisplay;
        UpdateDisplay();
    }

    private void OnDisable()
    {
        PlayerData.OnProgressChanged -= UpdateDisplay;
    }

    public void UpdateDisplay()
    {
        if (PlayerData.Instance == null) return;

        int collected = 0;

        for (int i = 0; i < 4; i++)
        {
            if (PlayerData.Instance.levels[levelIndex].piecesCollected[i])
                collected++;
        }

        counterText.text = collected + " / 4";
    }
}