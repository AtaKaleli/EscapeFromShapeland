using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    

    [Header("Player Info")]
    public bool isGameStarted;
    public bool isGamePaused;

    [Header("EndGame UI Data")]
    public float distance;
    public int coins;
    public int score;

    [Header("Shop UI Data")]
    public Color platformColor;
    
    

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }


    public void UpdateData(float newScore, int coins)
    {
        UpdateScores(newScore);
        UpdateTotalCoins(coins);
    }

    private static void UpdateTotalCoins(int coins)
    {
        int totalCoins = SaveManager.LoadTotalCoins();
        totalCoins += coins;
        SaveManager.SaveTotalCoins(totalCoins);
    }

    private static void UpdateScores(float newScore)
    {
        SaveManager.SaveLastScore(newScore);

        float bestScore = SaveManager.LoadBestScore();

        if (newScore >= bestScore)
            SaveManager.SaveBestScore(newScore);
    }

    public int CalculateScore()
    {
        if (distance < 0)
            distance = 0;

        return (int)distance * coins;
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
