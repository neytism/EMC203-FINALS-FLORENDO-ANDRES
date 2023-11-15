using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    
    [SerializeField] private int _size = 5;
    
    [SerializeField] private float _acceleration = 2f;

    [SerializeField] private TextMeshProUGUI _scoreText;

    private int _score = 0;
    
    public int GetScore => _score;
    
    public float Speed => _speed;

    public int Size => _size;
    
    public float Acceleration => _acceleration;

    public void IncreaseScore()
    {
        _score++;
        _scoreText.text = _score.ToString();
    }
    
    
}