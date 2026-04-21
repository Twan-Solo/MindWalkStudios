using UnityEngine;

public class PickupStatic : MonoBehaviour
{
    [Header("Score Settings")]
    public int scoreValue = 1;

    [Header("Trigger Settings")]
    public bool destroyAfterTrigger = true;

    private void OnTriggerEnter(Collider other)
    {
        // Only allow the player to trigger this
        if (!other.transform.root.CompareTag("Player"))
            return;

        Debug.Log("Player collected puzzle. Score added: " + scoreValue);

        ScoreCounter.Instance?.AddScore(scoreValue);

        if (destroyAfterTrigger)
            Destroy(gameObject);
    }
}
