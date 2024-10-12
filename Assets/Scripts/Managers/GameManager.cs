using System;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    

    private bool isGameStarted;
    private bool isGamePaused;

    private float distance;
    private int coins;
    

    public int Coins { get { return coins; } set { coins = value; } }
    public float Distance { get { return distance; } set { distance = value; } }
    public bool GameStarted { get { return isGameStarted; } set { isGameStarted = value; } }




    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
       //SaveManager.SaveTotalCoins(190);
        
    }

    public void UpdateData(float newScore, int coins)
    {
        UpdateScores(newScore);
        UpdateTotalCoins(coins);
    }

    private void UpdateTotalCoins(int coins)
    {
        int totalCoins = SaveManager.LoadTotalCoins();
        totalCoins += coins;
        SaveManager.SaveTotalCoins(totalCoins);
    }

    private void UpdateScores(float newScore)
    {
        SaveManager.SaveLastScore(newScore);

        float bestScore = SaveManager.LoadBestScore();

        if (newScore >= bestScore)
            SaveManager.SaveBestScore(newScore);
    }

    public int CalculateScore()
    {
        if (Distance < 0)
            Distance = 0;

        return (int)Distance * Coins;
    }



    #region UI Buttons

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

    public void RestartTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Quit()
    {
        Application.Quit();
    }

    #endregion




}
