using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static event Action EscapeKeyPressedEvent;
    private float _inputY;
    
    public KeyCode moveUpKey = KeyCode.W;
    public KeyCode moveDownKey = KeyCode.S;

    public float InputY => _inputY;

    private void OnEnable()
    {
        GameManager.SetNumberOfPlayerEvent += PlayersChecker;
    }
    
    private void OnDisable()
    {
        GameManager.SetNumberOfPlayerEvent -= PlayersChecker;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GetComponent<Paddle>().PlayerNumber == 1 ) EscapeKeyPressedEvent?.Invoke();
        }
        
        if (Input.GetKey(moveUpKey))
            _inputY = 1;
        else if (Input.GetKey(moveDownKey))
            _inputY = -1;
        else
            _inputY = 0;
    }

    public void PlayersChecker(int numberOfPlayer)
    {
        if (GetComponent<Paddle>().PlayerNumber == 2)
        {
            if(numberOfPlayer == 1)
            { 
                GetComponent<AiInput>().enabled = true;
                enabled = false;
            }
            else
            {
                GetComponent<AiInput>().enabled = false;
                enabled = true;
            }
           
        }
        
    }
}
