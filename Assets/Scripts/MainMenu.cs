using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        SoundManager.Instance.PlayLoop(SoundManager.Sounds.GameBgm);
    }

    public void SetNumbersOfPlayer(int numOfPlayers)
    {
        FindObjectOfType<GameManager>().NumberOfPlayer = numOfPlayers;
    }

    public void SetDifficulty(int difficulty)
    {
        FindObjectOfType<GameManager>().Difficulty = difficulty;
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
}
