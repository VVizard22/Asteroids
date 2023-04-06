using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour, IDamageable
{
    protected Action<Asteroid> killAction;
    [SerializeField]
    private float asteroidScoreValue = 10f;

    protected int amountToDivide = 0;

    public virtual void OnReceiveDamage()
    {
        killAction(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        if (!collision.TryGetComponent<IDamageable>(out IDamageable playerDamageProcessor))
            return;

        playerDamageProcessor.OnReceiveDamage();
    }

    public float GetAsteroidScoreValue() => asteroidScoreValue;

    public virtual void SetKillAction(Action<Asteroid> killAction) => this.killAction = killAction;

    public int GetAmountToDivide() => amountToDivide;

}
