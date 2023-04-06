using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBig : Asteroid
{
    [SerializeField]
    private Transform asteroidSmallPrefab;

    [SerializeField]
    private int minAsteroidSpawn;
    [SerializeField]
    private int maxAsteroidSpawn;

    private int amountToInstantiate;
    public override void OnReceiveDamage()
    {
        amountToInstantiate = Random.Range(minAsteroidSpawn, maxAsteroidSpawn);

        for (int i = 0; i < amountToInstantiate; i++)
        {
            Transform asteroidTransform = Instantiate(asteroidSmallPrefab);
            asteroidTransform.position = transform.position;
            //asteroidTransform.GetComponent<ConstantVelocity>().GenerateRandomizedDirection();
        }

        Destroy(this.gameObject);
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
