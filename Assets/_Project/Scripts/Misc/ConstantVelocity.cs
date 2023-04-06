using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantVelocity : MonoBehaviour
{
    private Vector2 velocityDirection = Vector2.zero;
    [SerializeField]
    private float velocityMultiplier = 1f;

    private new Rigidbody2D rigidbody2D;
    private bool shouldMove = false;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        GenerateRandomizedDirection();
    }

    private void Update()
    {
        if (!shouldMove)
            return;

        rigidbody2D.velocity = velocityDirection * velocityMultiplier;
    }

    [ContextMenu("Generate New Direction")]
    public void GenerateRandomizedDirection()
    {
        shouldMove = true;
        velocityDirection.x = Random.Range(-1f, 1f);
        velocityDirection.y = Random.Range(-1f, 1f);

        velocityDirection.Normalize();
    }

    public void StopMovement()
    {
        shouldMove = false;
        velocityDirection = Vector2.zero;
    }

}
