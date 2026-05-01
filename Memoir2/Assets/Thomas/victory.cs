using UnityEngine;

public class VictoryTrigger : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private string victoryTriggerName = "Victory";

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        Animator animator = other.GetComponentInChildren<Animator>();
        if (animator != null)
        {
            animator.SetTrigger(victoryTriggerName);
        }
    }
}