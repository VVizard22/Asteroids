using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed = 5f;

    [SerializeField]
    [Tooltip("The total time in seconds that the bullet will be alive")]
    private float lifeTime = 5f;
    private float currentTime = 0;

    private new Rigidbody2D rigidbody2D;
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= lifeTime)
            Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        rigidbody2D.velocity = transform.up * bulletSpeed;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out IDamageable current))
        {
            current.OnReceiveDamage();
            Destroy(this.gameObject);
        }
    }
}
