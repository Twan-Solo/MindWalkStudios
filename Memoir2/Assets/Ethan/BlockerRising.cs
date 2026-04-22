using System.Collections;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class BlockerPositionChange : MonoBehaviour
{
    public GameObject blockerWall;
    public GameObject BlockerGroup;
    public float duration;
    private Vector3 newPos;
    private Vector3 prevPos;
    public float newX;
    public float newY;
    public float newZ;
    private int lockPos = 0;
    void Awake()
    {
        newPos = new Vector3(blockerWall.transform.position.x + newX, blockerWall.transform.position.y + newY, blockerWall.transform.position.z + newZ);
        prevPos = new Vector3(blockerWall.transform.position.x, blockerWall.transform.position.y, blockerWall.transform.position.z);
    }
    void OnTriggerEnter(Collider other)
    {
        // Check if the object we hit has the "player" tag
        if (other.CompareTag("Player"))
        {
            //if it hasn't run before, move wall into place.
            if (lockPos == 0)
            {
                
                StartCoroutine(moveSlowly());
            }
        }
        else
        {
            Debug.Log("Something Happened.");
        }
    }
    // uses lerp to slowly move the wall from the old position to the new position
    IEnumerator moveSlowly()
    {
        Vector3 startPos = transform.position;
        float elapsed = 0f;

        while (elapsed < duration) 
        {
            blockerWall.transform.position = Vector3.Lerp(prevPos, newPos, (elapsed / duration));
            elapsed += Time.deltaTime;
            
            yield return null;
        }
        blockerWall.transform.position = newPos;
        lockPos = 1;

    }

}
