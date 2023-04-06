using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class AsteroidsSpawner : MonoBehaviour
{
    public static event EventHandler<OnDestroyAsteroidEventArgs> OnDestroyAsteroid;

    public class OnDestroyAsteroidEventArgs : EventArgs
    {
        public Asteroid asteroid;
    }

    [SerializeField]
    private Vector2Variable playerPosition;
    [SerializeField]
    private AsteroidBig bigAsteroidPrefab;
    [SerializeField]
    private AsteroidSmall smallAsteroidPrefab;
    
    [Space]
    [Header("Script settings: ")]
    [SerializeField]
    private int maxSimultaneousAsteroidAmount = 7;
    [SerializeField]
    private float minDistanceToPlayer = 1.5f;
    [SerializeField]
    private float maxSpawnTimer = 5f;
    private float spawnTimer;

    private int currentSimultaneousAsteroidAmount;
    private float cameraHeight;
    private float cameraWidth;

    private ObjectPool<AsteroidBig> asteroidBigPool;
    private ObjectPool<AsteroidSmall> asteroidSmallPool;


    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        cameraHeight = Camera.main.orthographicSize;
        cameraWidth = cameraHeight * Camera.main.aspect;

        #region Asteroids Pool configuration
        asteroidBigPool = new ObjectPool<AsteroidBig>(() => {
            return Instantiate(bigAsteroidPrefab);
        }, asteroidBig => {
            asteroidBig.gameObject.SetActive(true);
        }, asteroidBig => {
            asteroidBig.gameObject.SetActive(false);
        }, asteroidBig => {
            Destroy(asteroidBig.gameObject);
        }, false, maxSimultaneousAsteroidAmount, maxSimultaneousAsteroidAmount);

        asteroidSmallPool = new ObjectPool<AsteroidSmall>(() => {
            return Instantiate(smallAsteroidPrefab);
        }, asteroidSmall => {
            asteroidSmall.gameObject.SetActive(true);
        }, asteroidSmall => {
            asteroidSmall.gameObject.SetActive(false);
        }, asteroidSmall => {
            Destroy(asteroidSmall.gameObject);
        }, false, 10, 20);

        #endregion
        for (int i = 0; i < maxSimultaneousAsteroidAmount; i++)
            SpawnNewBigAsteroid();
    }


    private void Update()
    {
        if (currentSimultaneousAsteroidAmount >= maxSimultaneousAsteroidAmount)
            return;

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= maxSpawnTimer)
        {
            spawnTimer = 0;
            SpawnNewBigAsteroid();
        }
    }

    #region Big Asteroid Spawn And Kill

    private void SpawnNewBigAsteroid()
    {
        if (currentSimultaneousAsteroidAmount >= maxSimultaneousAsteroidAmount)
            return;

        currentSimultaneousAsteroidAmount++;

        AsteroidBig asteroidBig = asteroidBigPool.Get();
        asteroidBig.SetKillAction(KillBigAsteroid);
        asteroidBig.transform.position = GenerateNewScreenPos();
        
        while (Vector2.Distance(asteroidBig.transform.position, playerPosition.Value) < minDistanceToPlayer)
        {
            asteroidBig.transform.position = GenerateNewScreenPos();
        }
    }

    private void KillBigAsteroid(Asteroid asteroid)
    {
        currentSimultaneousAsteroidAmount--;
        for (int i = 0; i < asteroid.GetAmountToDivide(); i++)
        {
            SpawnSmallAsteroid(asteroid);
        }
        asteroidBigPool.Release((AsteroidBig) asteroid);
        InvokeKillAsteroidEvent(asteroid);
    }
    #endregion

    #region Small Asteroid Spawn And kill
    // Gets a small asteroid from the pool, activates it and gives it a random direction
    private void SpawnSmallAsteroid(Asteroid asteroid)
    {
        AsteroidSmall asteroidSmall = asteroidSmallPool.Get();
        asteroidSmall.transform.position = asteroid.transform.position;
        asteroidSmall.GetComponent<ConstantVelocity>().GenerateRandomizedDirection();

        asteroidSmall.SetKillAction(KillSmallAsteroid);
    }

    private void KillSmallAsteroid(Asteroid asteroid)
    {
        asteroidSmallPool.Release((AsteroidSmall) asteroid);

        InvokeKillAsteroidEvent(asteroid);

        SpawnNewBigAsteroid();
    }

    #endregion

    private void InvokeKillAsteroidEvent(Asteroid asteroid)
    {
        OnDestroyAsteroid?.Invoke(this, new OnDestroyAsteroidEventArgs {
            asteroid = asteroid
        });
    }

    private Vector2 GenerateNewScreenPos()
    {
        Vector2 newPosition = Vector3.zero;

        newPosition.x = UnityEngine.Random.Range(-cameraWidth, cameraWidth);
        newPosition.y = UnityEngine.Random.Range(-cameraHeight, cameraHeight);

        return newPosition;
    }

}
