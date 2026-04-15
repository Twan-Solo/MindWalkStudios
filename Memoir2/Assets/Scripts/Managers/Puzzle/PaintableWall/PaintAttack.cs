using JetBrains.Annotations;
using UnityEngine;

public class PaintAttack : MonoBehaviour
{
    public void Update()
    {
        transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * 8f);
    }
    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("PaintedWall"))
        {
            Destroy(gameObject);
        }
    }
}
