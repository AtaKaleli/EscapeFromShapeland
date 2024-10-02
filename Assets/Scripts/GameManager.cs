using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Player Info")]
    public bool isGameStarted;
    public bool isGamePaused;

    [Header("Saveables")]
    public float distance;
    public int coins;
    public int totalCoins;
    public Color platformColor;
    public float lastScore;
    public float bestScore;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        LoadScores();
        LoadTotalCoins();
    }

    private void Start()
    {
        

        
    }

    public void ResumeGameButton()
    {
        Time.timeScale = 1;
        isGamePaused = false;
    }

    public void PauseGameButton()
    {
        if (!isGamePaused)
        {
            Time.timeScale = 0;
            isGamePaused = true;
        }

    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SaveScores(float score)
    {

        PlayerPrefs.SetFloat("LastScore", score);

        bestScore = PlayerPrefs.GetFloat("BestScore", -1);

        if(score >= bestScore)
        {
            PlayerPrefs.SetFloat("BestScore", score);
        }
    }

    public void SaveTotalCoins(int coins)
    {
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        totalCoins += coins;
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
    }

    public void LoadScores()
    {
        lastScore = PlayerPrefs.GetFloat("LastScore", 0);
        bestScore = PlayerPrefs.GetFloat("BestScore", 0);
    }
    
    public void LoadTotalCoins()
    {
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
    }

}
