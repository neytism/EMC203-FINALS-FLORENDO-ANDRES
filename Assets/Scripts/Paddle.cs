using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    
    [SerializeField] private int _size = 5;
    
    [SerializeField] private float _acceleration = 2f;
    
    public float Speed => _speed;

    public int Size => _size;
    
    public float Acceleration => _acceleration;
    
}
