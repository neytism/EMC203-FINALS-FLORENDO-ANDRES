using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiInput : MonoBehaviour
{
    [SerializeField] private Transform _ball;

    [SerializeField] private float _intelligence = 0.5f;
    
    private float _inputY;

    public float InputY => _inputY;

    private void OnEnable()
    {
        GameManager.SetDifficultyEvent += SetIntelligence;
    }
    
    private void OnDisable()
    {
        GameManager.SetDifficultyEvent += SetIntelligence;
    }

    private void FixedUpdate()
    {
        float distanceY = _ball.position.y - transform.position.y;

        _inputY = Mathf.Clamp(distanceY * _intelligence, -1f, 1f);
    }

    private void SetIntelligence(int difficulty)
    {
        switch (difficulty)
        {
            case 1:
                _intelligence = 0.05f;
                break;
            case 2:
                _intelligence = 0.5f;
                break;
            case 3:
                _intelligence = 0.8f;
                break;
            default:
                _intelligence = 0.05f;
                break;
        }
    }
}
