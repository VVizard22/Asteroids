using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private InputDataHolderSO inputDataHolderSO;
    [SerializeField]
    private float moveForceMultiplier = 5f;
    [SerializeField]
    private Transform bulletPrefab;
    [SerializeField]
    private Transform bulletPointTransform;
    [SerializeField]
    private float shootCooldown = .2f;
    private float shootTime;
    private bool isShooting;
    private new Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        shootTime = shootCooldown;
        isShooting = false;
    }

    private void Start()
    {
        inputDataHolderSO.OnStartShoot += PlayerAction_OnPlayerStartShooting;
        inputDataHolderSO.OnCancelShoot += PlayerAction_OnPlayerStopShooting;
    }

    private void PlayerAction_OnPlayerStopShooting(object sender, System.EventArgs e) =>
        isShooting = false;
    
    private void PlayerAction_OnPlayerStartShooting(object sender, System.EventArgs e) =>
        isShooting = true;

    private void Update()
    {
        shootTime += Time.deltaTime;
        
        if (!isShooting)
            return;

        if (shootTime < shootCooldown)
            return;
        
        PerformShootAction();
    }

    private void PerformShootAction()
    {
        shootTime = 0;
        Transform bulletTransform = Instantiate(bulletPrefab);
        bulletTransform.rotation = transform.rotation;
        bulletTransform.position = bulletPointTransform.position;
    }


    private void FixedUpdate()
    {
        float rotationCap = 5f;
        rigidbody2D.SetRotation(rigidbody2D.rotation - inputDataHolderSO.rotation * rotationCap);

        if (inputDataHolderSO.isImpulsing)
        {
            rigidbody2D.AddForce(transform.up * moveForceMultiplier);
        }
    }
}
