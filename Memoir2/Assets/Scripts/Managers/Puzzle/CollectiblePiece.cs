using UnityEngine;

public class CollectiblePiece : MonoBehaviour
{
    [Header("Level & Piece Index")]
    public int levelIndex;
    public int pieceIndex;

    private bool collected = false;

    private void Start()
    {
        if (PlayerData.Instance == null) return;

        // If already collected hide completely
        if (PlayerData.Instance.levels[levelIndex].piecesCollected[pieceIndex])
        {
            collected = true;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collected) return;
        if (!other.CompareTag("Player")) return;

        collected = true;

        // Save progress
        PlayerData.Instance.CollectPiece(levelIndex, pieceIndex);

        // Hide completely
        gameObject.SetActive(false);
    }
}