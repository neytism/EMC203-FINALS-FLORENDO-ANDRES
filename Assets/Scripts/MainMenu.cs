using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public static event Action<bool> ToggleEffectEvent; 
    [SerializeField] private GameObject _mutedIcon;
    [SerializeField] private GameObject _unmutedIcon;
    [SerializeField] private GameObject _effectOnIcon;
    [SerializeField] private GameObject _effectOffIcon;
    public ParticleSystem ballTrail;
    
    private void Start()
    {
        SoundManager.Instance.PlayLoop(SoundManager.Sounds.GameBgm);
        
        if (SoundManager.Instance.SoundSfx.volume == 0f)
        {
            _mutedIcon.SetActive(false);
            _unmutedIcon.SetActive(true);
        }
        else
        {
            _mutedIcon.SetActive(true);
            _unmutedIcon.SetActive(false);
        }
        
        if (GameManager.Instance.isEffectOn)
        {
            _effectOffIcon.SetActive(false);
            _effectOnIcon.SetActive(true);
        }
        else
        {
            _effectOffIcon.SetActive(true);
            _effectOnIcon.SetActive(false);
        }
    }

    public void SetNumbersOfPlayer(int numOfPlayers)
    {
        FindObjectOfType<GameManager>().NumberOfPlayer = numOfPlayers;
    }

    public void SetDifficulty(int difficulty)
    {
        FindObjectOfType<GameManager>().Difficulty = difficulty;
    }

    public void SetBallSpeed(int speed)
    {
        FindObjectOfType<GameManager>().BallSpeed = speed;
    }
    public void LoadGame()
    {
        SoundManager.Instance.StopPlayingBGM(SoundManager.Sounds.GameBgm);
        FindObjectOfType<GameManager>().LoadGame();
    }
    
    public void ExitGame() {
        Application.Quit();
    }

    public void PlayUIHighlightSound()
    {
        SoundManager.Instance.PlayOnce(SoundManager.Sounds.UIHighlight);
    }
    
    public void PlayUIClickSound()
    {
        SoundManager.Instance.PlayOnce(SoundManager.Sounds.UIClick);
    }
    
    public void Mute()
    {
        SoundManager.Instance.SoundSfx.volume = 0f;
        SoundManager.Instance.SoundBGMusic.volume = 0f;
    }
    
    public void Unmute()
    {
        SoundManager.Instance.SoundSfx.volume = 1f;
        SoundManager.Instance.SoundBGMusic.volume = 1f;
    }

    public void ToggleEffect(bool isOn)
    {
        ToggleEffectEvent?.Invoke(isOn);
        
        if (isOn)
        {
            ballTrail.Play();
        }
        else
        {
            ballTrail.Stop();
        }
        
    }
}
