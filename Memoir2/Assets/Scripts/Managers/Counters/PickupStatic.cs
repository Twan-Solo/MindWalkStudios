using UnityEngine;

public class PickupStatic : MonoBehaviour
{
    [Header("Score Settings")]
    public int scoreValue = 1;

    [Header("Trigger Settings")]
    public bool destroyAfterTrigger = true;

    [Header("Audio")]
    public AudioClip pickupSound; // assign in Inspector
    public float volume = 1f;

    private void OnTriggerEnter(Collider other)
    {
        // Only allow the player to trigger this
        if (!other.transform.root.CompareTag("Player"))
            return;

        Debug.Log("Player collected puzzle. Score added: " + scoreValue);

        // Play sound at this object's position
        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position, volume);
        }

        ScoreCounter.Instance?.AddScore(scoreValue);

        if (destroyAfterTrigger)
            Destroy(gameObject);
    }
}