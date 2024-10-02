using TMPro;
using UnityEngine;

public class UI_Endgame : MonoBehaviour
{


    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI scoreText;



    private void Start()
    {

        float score = GameManager.instance.CalculateScore();
        float distance = GameManager.instance.distance;
        int coins = GameManager.instance.coins;

        DisplayInfo(distance, coins, score);

        GameManager.instance.UpdateData(score,coins);

    }

    private void DisplayInfo(float distance, int coins, float score)
    {
        distanceText.text = "Distance: " + distance.ToString("0");
        coinsText.text = "Coins: " + coins.ToString("0");
        scoreText.text = "Score: " + score.ToString("0"); 
    }
}
