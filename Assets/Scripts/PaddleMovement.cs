using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private PlayerInput _input;
    [SerializeField] private float _minY, _maxY;
    private Paddle _paddle;

    private float _currentSpeed = 0f;
    private Vector3 _lastPosition;

    public Vector3 velocity;

    private void Start()
    {
        _paddle = GetComponent<Paddle>();
    }

   

    void FixedUpdate()
    {
        _lastPosition = _player.transform.position; // Store the last position first
        Move(_paddle.Size);
        velocity = Velocity();
        
    }

    private void Move(int size)
    {
        
        Vector3 newScale = new Vector3(_player.transform.localScale.x, _paddle.Size, _player.transform.localScale.z);
        _player.transform.localScale = newScale;
    
        float targetSpeed = _input.InputY * (_paddle.Speed * 20);
        
        _currentSpeed = Mathf.MoveTowards(_currentSpeed, targetSpeed, (_paddle.Acceleration * 100) * Time.deltaTime);
        
        Vector3 newPosition = _player.transform.position + new Vector3(0, _currentSpeed, 0) * Time.deltaTime;

        float adjustedMinY = _minY + size / 2.0f;
        float adjustedMaxY = _maxY - size / 2.0f;
    
        newPosition.y = Mathf.Clamp(newPosition.y, adjustedMinY, adjustedMaxY);
    
        if (newPosition.y <= adjustedMinY || newPosition.y >= adjustedMaxY)
        {
            _currentSpeed = 0f;
        }
        
        _player.transform.position = newPosition;
    }

    public Vector3 Velocity()
    {
        // Calculate the velocity
        Vector3 velocity = (_player.transform.position - _lastPosition) / Time.deltaTime;
        return velocity;
    }
}
