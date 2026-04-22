using UnityEngine;

public class BlockerHP : MonoBehaviour
{
    public int blockerHP = 5;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PaintAttack")){
            blockerHP--;
        }
    }
}
