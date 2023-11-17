using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiInput : MonoBehaviour
{
    [SerializeField] private Transform _ball;

    [SerializeField] private float _intelligence = 0.5f;
    
    private float _inputY;
    private bool _isBallReady = false;
    
    private float _minDistanceX = -30f;

    public float InputY => _inputY;

    private void OnEnable()
    {
        GameManager.SetDifficultyEvent += SetIntelligence;
        BouncingBall.IsBallCDUpdateEvent += SetBallStatus;
    }
    
    private void OnDisable()
    {
        GameManager.SetDifficultyEvent -= SetIntelligence;
        BouncingBall.IsBallCDUpdateEvent -= SetBallStatus;
    }

    private void FixedUpdate()
    {
        float distanceY = _ball.position.y - transform.position.y;
        float distanceX = _ball.position.x - transform.position.x;
        
        //the farther the ball, the less intelligent
        float intelligenceMultiplier = distanceX < _minDistanceX ? Mathf.Pow(_minDistanceX / distanceX, 5) : 1f;

        if (_isBallReady)
        {
            _inputY = Mathf.Clamp(distanceY * _intelligence * intelligenceMultiplier, -1f, 1f);
            Debug.Log(distanceX);
            Debug.Log(_intelligence * intelligenceMultiplier);
        }
        else
        {
            _inputY = Mathf.Clamp(distanceY * 0.01f, -1f, 1f);
        }
    }

    private void SetBallStatus(bool x)
    {
        _isBallReady = !x;
    }

    private void SetIntelligence(int difficulty)
    {
        int ballSpeed = GameManager.Instance.BallSpeed;
    
        switch (difficulty)
        {
            case 1:
                switch (ballSpeed)
                {
                    case 1:
                        _intelligence = 0.03f;
                        break;
                    case 2:
                        _intelligence = 0.04f; // Medium ball speed
                        break;
                    case 3:
                        _intelligence = 0.05f;
                        break;
                    default:
                        _intelligence = 0.03f;
                        break;
                }
                break;
            case 2:
                switch (ballSpeed)
                {
                    case 1:
                        _intelligence = 0.06f;
                        break;
                    case 2:
                        _intelligence = 0.08f;
                        break;
                    case 3:
                        _intelligence = 0.1f; 
                        break;
                    default:
                        _intelligence = 0.06f; 
                        break;
                }
                break;
            case 3:
                switch (ballSpeed)
                {
                    case 1:
                        _intelligence = 0.1f;
                        break;
                    case 2:
                        _intelligence = 0.25f; 
                        break;
                    case 3:
                        _intelligence = 0.4f;
                        break;
                    default:
                        _intelligence = 0.4f;
                        break;
                }
                break;
            default:
                _intelligence = 0.05f;
                break;
        }
    }

}
