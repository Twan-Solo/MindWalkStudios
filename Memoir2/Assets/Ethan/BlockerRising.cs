using System.Collections;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class BlockerPositionChange : MonoBehaviour
{
    public GameObject blockerMovement;
    public GameObject blockerGroup;
    public GameObject blockerHp;
    public float duration;
    private Vector3 newPos;
    private Vector3 prevPos;
    public float newX;
    public float newY;
    public float newZ;
    private int lockPos = 0;
    public BlockerHP blockerScript;
    
    void Awake()
    {
        newPos = new Vector3(blockerMovement.transform.position.x + newX, blockerMovement.transform.position.y + newY, blockerMovement.transform.position.z + newZ);
        prevPos = new Vector3(blockerMovement.transform.position.x, blockerMovement.transform.position.y, blockerMovement.transform.position.z);
    }

    public void Update()
    {
        if (blockerScript.blockerHP == 0)
        {
            StartCoroutine(returnSlowly());
        }
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
            blockerMovement.transform.position = Vector3.Lerp(prevPos, newPos, (elapsed / duration));
            elapsed += Time.deltaTime;
            
            yield return null;
        }
        blockerMovement.transform.position = newPos;
        lockPos = 1;

    }
    IEnumerator returnSlowly()
    {
        Vector3 startPos = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            blockerMovement.transform.position = Vector3.Lerp(newPos, prevPos, (elapsed / duration));
            elapsed += Time.deltaTime;

            yield return null;
        }
        blockerMovement.transform.position = prevPos;
        lockPos = 1;

    }
}
