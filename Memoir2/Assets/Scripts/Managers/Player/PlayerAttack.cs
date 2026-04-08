using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public InputActionReference attack;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 5f;
    public float rotationSpeed = 10f;


    private void Update()
    {
        if (attack.action.triggered)
        {
            Instantiate(bulletPrefab);
        }
    }

}
