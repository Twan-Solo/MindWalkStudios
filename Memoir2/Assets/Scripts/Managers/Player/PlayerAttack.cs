using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public InputActionReference attack;
    public GameObject bulletPrefab;
    public GameObject player;
    private float playerRotation;

    public float bulletSpawnHeight = 1;

    public void Start()
    {
        //assign freshly spawn playerArmature to script
        player = GameObject.Find("PlayerArmature(Clone)");
    }

    private void Update()
    {
        //get rotation
        playerRotation = player.transform.eulerAngles.y;
        //spawn on attack
        if (attack.action.triggered)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position + new Vector3(0, bulletSpawnHeight, 0), Quaternion.Euler(0, playerRotation+90, -90));
        }
    }

}
