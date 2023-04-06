using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    private string originalText;
    bool alreadySuscribed = false;

    private void Awake()
    {
        originalText = scoreText.text;
    }

    private void Start()
    {
        if (!alreadySuscribed)
            SubscribeToEvents();
    }

    public void SubscribeToEvents()
    {
        alreadySuscribed = true;
        ScoreHandler.OnScoreUpdated += ScoreHandler_OnScoreUpdated;
    }

    private void OnDestroy()
    {
        ScoreHandler.OnScoreUpdated -= ScoreHandler_OnScoreUpdated;
    }


    private void ScoreHandler_OnScoreUpdated(object sender, ScoreHandler.OnScoreUpdatedEventArgs e)
    {
        scoreText.text = originalText + e.score.ToString();
    }
}

