using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]
    private Button resetGameButton;
    [SerializeField]
    private TextMeshProUGUI resetGameButtonText;
    [SerializeField]
    private float maxTimeForReset = 5f;

    private ScoreUI finalScoreScoreUI;

    private bool shouldCountdown = false;

    private void Awake()
    {
        resetGameButton.onClick.AddListener(() => {
            ResetGameScene();
        });

        finalScoreScoreUI = GetComponent<ScoreUI>();
    }

    private void Start()
    {
        DamagePlayer.OnPlayerDie += DamagePlayer_OnPlayerDie;
        finalScoreScoreUI.SubscribeToEvents();
        Hide();
    }

    private void OnDestroy()
    {
        DamagePlayer.OnPlayerDie -= DamagePlayer_OnPlayerDie;
    }

    private void DamagePlayer_OnPlayerDie(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Update()
    {
        if (!shouldCountdown)
            return;

        maxTimeForReset -= Time.deltaTime;

        resetGameButtonText.text = "Reset Game (" + ((int) maxTimeForReset).ToString() + ")";


        if (maxTimeForReset > 0)
            return;

        ResetGameScene();
    }

    private void ResetGameScene()
    {
        SceneManager.LoadScene(0);
    }

    private void Show()
    {
        gameObject.SetActive(true);
        shouldCountdown = true;
    }

    private void Hide() =>
        gameObject.SetActive(false);

}
