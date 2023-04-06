using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBig : Asteroid
{

    [SerializeField]
    private int minAsteroidSpawn;
    [SerializeField]
    private int maxAsteroidSpawn;

    public override void OnReceiveDamage()
    {
        amountToDivide = UnityEngine.Random.Range(minAsteroidSpawn, maxAsteroidSpawn);

        killAction(this);
    }

    private void OnEnable() 
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, .5f).SetEase(Ease.OutQuad);
    }

    private void OnValidate()
    {
        if (maxAsteroidSpawn < 0)
            maxAsteroidSpawn = 0;
        else if (minAsteroidSpawn < 0)
            minAsteroidSpawn = 0;
        else if (minAsteroidSpawn > maxAsteroidSpawn)
            minAsteroidSpawn = maxAsteroidSpawn;
    }

}
