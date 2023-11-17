using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public static event Action<int,int> UpdateScoreEvent;
    
    [SerializeField] private int _playerNumber = 1;

    public int PlayerNumber => _playerNumber;

    [SerializeField] private float _speed = 5f;
    
    [SerializeField] private int _size = 5;
    
    [SerializeField] private float _acceleration = 2f;

    private int _score = 0;
    
    public int GetScore => _score;
    
    public float Speed => _speed;

    public int Size => _size;
    
    public float Acceleration => _acceleration;

    public void IncreaseScore()
    {
        _score++;
        UpdateScoreEvent?.Invoke(_playerNumber,_score);
    }

    private void OnEnable()
    {
        _size = 5;
        UIManager.RestartGameEvent += ResetScore;
        GameManager.SetDifficultyEvent += SetIntelligence;
    }
    
    private void OnDisable()
    {
        UIManager.RestartGameEvent -= ResetScore;
        GameManager.SetDifficultyEvent -= SetIntelligence;
    }

    private void ResetScore()
    {
        _score = 0;
        UpdateScoreEvent?.Invoke(_playerNumber,_score);
    }
    
    private void SetIntelligence(int difficulty)
    {
        switch (difficulty)
        {
            case 1:
                _size = 7;
                break;
            case 2:
                _size = 6;
                break;
            case 3:
                _size = 5;
                break;
            default:
                _size = 5;
                break;
        }
    }
    
    
}