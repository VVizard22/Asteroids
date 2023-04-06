using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour, IDamageable
{
    public static event EventHandler OnPlayerDie;

    [SerializeField]
    private int playerLivesAmount = 1;

    public void OnReceiveDamage()
    {
        playerLivesAmount--;

        if (playerLivesAmount > 0)
            return;

        OnPlayerDie?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }
}
