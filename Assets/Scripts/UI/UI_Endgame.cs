using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Endgame : MonoBehaviour
{
    

    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI scoreText;


   
    private void Start()
    {
        float distance = GameManager.instance.distance;
        int coins = GameManager.instance.coins;
        float score = distance * coins;
        
        DisplayInfo(distance, coins, score);

        GameManager.instance.SaveScores(score);
        GameManager.instance.SaveTotalCoins(coins);

    }

    private void DisplayInfo(float distance, int coins, float score)
    {
        distanceText.text = "Distance: " + distance.ToString("0");
        coinsText.text = "Coins: " + coins.ToString("0");
        scoreText.text = "Score: " + score.ToString("0");
    }
}
