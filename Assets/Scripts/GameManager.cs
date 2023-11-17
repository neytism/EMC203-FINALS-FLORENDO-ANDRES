using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Instance

    private static GameManager _instance;
    public static  GameManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    #endregion
    //game prep
    public static event Action<int> SetDifficultyEvent;
    public static event Action<int> SetBallSpeedEvent;
    public static event Action<int> SetNumberOfPlayerEvent;
    public static event Action GameReadyEvent;
    
    private int numberOfPlayer = 1;
    private int difficulty = 1;
    private int ballSpeed = 1;

    public int NumberOfPlayer
    {
        get => numberOfPlayer;
        set => numberOfPlayer = value;
    }

    public int Difficulty
    {
        get => difficulty;
        set => difficulty = value;
    }
    
    public int BallSpeed
    {
        get => ballSpeed;
        set => ballSpeed = value;
    }

    
    //game
    public static event Action<int> ShowWinnerEvent;
    [SerializeField] private int _maxScore = 10;
    private int player1Score;
    private int player2Score;
    public bool isEffectOn = true;

    private void OnEnable()
    {
        BouncingBall.GameLoaded += SetGameSettings;
        Paddle.UpdateScoreEvent += UpdateScoreEvent;
        UIManager.RestartGameEvent += RestartGame;
        UIManager.LoadMainMenuEvent += LoadMainMenu;
        MainMenu.ToggleEffectEvent += ToggleEffect;
    }
    
    private void OnDisable()
    {
        BouncingBall.GameLoaded -= SetGameSettings;
        Paddle.UpdateScoreEvent -= UpdateScoreEvent;
        UIManager.RestartGameEvent -= RestartGame;
        UIManager.LoadMainMenuEvent -= LoadMainMenu;
        MainMenu.ToggleEffectEvent -= ToggleEffect;
    }

    private void UpdateScoreEvent(int player, int score)
    {
        if (player == 1)
        {
            player1Score = score;
        }
        else
        {
            player2Score = score;
        }
        
        CheckWinner();
    }

    private void CheckWinner()
    {
        if (player1Score >= _maxScore)
        {
            Time.timeScale = 0f;
            ShowWinnerEvent?.Invoke(1);
        }
        
        if (player2Score >= _maxScore)
        {
            Time.timeScale = 0f;
            ShowWinnerEvent?.Invoke(2);
        }
        
    }

    private void RestartGame()
    {
        player1Score = 0;
        player2Score = 0;
        Time.timeScale = 1f;
    }

    public void SetPlayerNumber(int players)
    {
        numberOfPlayer = players;
    }

    public void SetDifficulty(int dif)
    {
        difficulty = dif;
    }

    public void ToggleEffect(bool isOn)
    {
        isEffectOn = isOn;
    }

    private void SetGameSettings()
    {
        SetNumberOfPlayerEvent?.Invoke(numberOfPlayer);
        SetBallSpeedEvent?.Invoke(ballSpeed);
        SetDifficultyEvent?.Invoke(difficulty);
        GameReadyEvent?.Invoke();
    }
    
    

    public void LoadGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
