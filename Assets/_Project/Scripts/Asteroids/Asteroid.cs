using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour, IDamageable
{
    public virtual void OnReceiveDamage()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        if (!collision.TryGetComponent<IDamageable>(out IDamageable playerDamageProcessor))
            return;

        playerDamageProcessor.OnReceiveDamage();
    }
}
