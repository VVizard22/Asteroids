using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantVelocity : MonoBehaviour, IShootable
{
    private Vector2 velocityDirection = Vector2.zero;
    [SerializeField]
    private float velocityMultiplier = 1f;

    private new Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        GenerateRandomizedDirection();
    }

    private void Update()
    {
        rigidbody2D.velocity = velocityDirection * velocityMultiplier;
    }

    [ContextMenu("Generate New Direction")]
    private void GenerateRandomizedDirection()
    {
        velocityDirection.x = Random.Range(-1f, 1f);
        velocityDirection.y = Random.Range(-1f, 1f);

        velocityDirection.Normalize();
    }

    public void OnReceiveShoot()
    {
        Destroy(gameObject);
    }
}
