using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private float _inputY;
    
    public KeyCode moveUpKey = KeyCode.W;
    public KeyCode moveDownKey = KeyCode.S;

    public float InputY => _inputY;

    void Update()
    {
        if (Input.GetKey(moveUpKey))
            _inputY = 1;
        else if (Input.GetKey(moveDownKey))
            _inputY = -1;
        else
            _inputY = 0;
    }
}
