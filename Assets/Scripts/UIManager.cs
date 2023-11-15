using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static event Action RestartGameEvent;
    public static event Action LoadMainMenuEvent;
    
    [SerializeField] private TextMeshProUGUI _player1ScoreText;
    [SerializeField] private TextMeshProUGUI _player2ScoreText;

    [SerializeField] private GameObject _winnerTextHolder;
    [SerializeField] private TextMeshProUGUI _winnerText;

    [SerializeField] private GameObject _clickAnywhereHolder;
    
    [SerializeField] private GameObject _pauseHolder;
    
    private void OnEnable()
    {
        Paddle.UpdateScoreEvent += UpdateTextScoreEvent;
        PlayerInput.EscapeKeyPressedEvent += Pause;
        GameManager.ShowWinnerEvent += ShowWinnerPanel;
    }
    
    private void OnDisable()
    {
        Paddle.UpdateScoreEvent -= UpdateTextScoreEvent;
        PlayerInput.EscapeKeyPressedEvent -= Pause;
        GameManager.ShowWinnerEvent -= ShowWinnerPanel;
    }

    private void UpdateTextScoreEvent(int player, int score)
    {
        if (player == 1)
        {
            _player1ScoreText.text = score.ToString();
        }
        else
        {
            _player2ScoreText.text = score.ToString();
        }
    }

    private void ShowWinnerPanel(int player)
    {
        _winnerTextHolder.SetActive(true);
        _winnerText.text = $"PLAYER {player} WINS";
        StartCoroutine(ShowForSeconds(_clickAnywhereHolder, 3));
    }

    IEnumerator ShowForSeconds(GameObject obj, float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        obj.SetActive(true);
    }
    
    public void RestartGame()
    {
        _winnerTextHolder.SetActive(false);
        _clickAnywhereHolder.SetActive(false);
        RestartGameEvent?.Invoke();
    }

    public void Pause()
    {
        if (!_pauseHolder.activeInHierarchy)
        {
            _pauseHolder.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            _pauseHolder.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void MainMenuButton()
    {
        LoadMainMenuEvent?.Invoke();
        Time.timeScale = 1f;
    }
    
    
}
