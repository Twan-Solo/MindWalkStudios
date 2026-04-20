using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class PaintAttack : MonoBehaviour
{
    public float bulletSpeed = 1;


    public void Update()
    {
        transform.Translate(new Vector3(0, -(bulletSpeed), 0) * Time.deltaTime * 8f);

    }


    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("PaintedWall"))
        {
            Destroy(gameObject);
        }
        else if(other.CompareTag("Player")){
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
