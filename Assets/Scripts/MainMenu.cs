using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
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
        FindObjectOfType<GameManager>().LoadGame();
    }
}
