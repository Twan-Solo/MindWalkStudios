using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public InputActionReference attack;
    public GameObject bulletPrefab;
    public GameObject player;
    public GameObject firePoint;
    private float playerRotation;

    public void Start()
    {
        //assign freshly spawn playerArmature to script
        firePoint = GameObject.Find("FirePoint");
        player = GameObject.Find("PainterArmature(Clone)");
    }

    private void Update()
    {
        //get rotation
        playerRotation = player.transform.eulerAngles.y;
        Vector3 fixPosition = player.transform.position; //need to fix the position because the painterArmature was not parented at 0, so I can't just spawn the bullet at the fire point. 
        Vector3 currentPos = firePoint.transform.position;
        //spawn on attack
        if (attack.action.triggered)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position + new Vector3(currentPos.x - fixPosition.x, currentPos.y - fixPosition.y, currentPos.z - fixPosition.z), Quaternion.Euler(0, playerRotation+90, -90));
        }
    }

}
