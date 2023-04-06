using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    public static event EventHandler<OnScoreUpdatedEventArgs> OnScoreUpdated;
    public class OnScoreUpdatedEventArgs : EventArgs
    {
        public float score;
    }

    private float currentScore;
    private bool firstUpdate = true;


    private void Start()
    {
        AsteroidsSpawner.OnDestroyAsteroid += AsteroidsSpawner_OnDestroyAsteroid;
        currentScore = 0;
    }

    private void OnDestroy()
    {
        AsteroidsSpawner.OnDestroyAsteroid -= AsteroidsSpawner_OnDestroyAsteroid;
    }

    private void Update()
    {
        if (!firstUpdate)
            return;

        firstUpdate = false;
        SendNewScore();
    }

    private void AsteroidsSpawner_OnDestroyAsteroid(object sender, AsteroidsSpawner.OnDestroyAsteroidEventArgs e)
    {
        Asteroid currentAsteroid = e.asteroid;

        AddAsteroidScore(currentAsteroid);
    }

    private void AddAsteroidScore(Asteroid asteroid)
    {
        currentScore += asteroid.GetAsteroidScoreValue();

        SendNewScore();
    }

    private void SendNewScore()
    {
        OnScoreUpdated?.Invoke(this, new OnScoreUpdatedEventArgs {
            score = this.currentScore
        });
    }
}
