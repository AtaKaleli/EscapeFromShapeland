using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Endgame : MonoBehaviour
{
    private Player player;

    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI scoreText;


    private void Awake()
    {
        if(player == null)
        {
            player = FindObjectOfType<Player>();
        }
    }
    private void Start()
    {
        float distance = player.transform.position.x;
        float coins = GameManager.instance.coins;
        float score = distance * coins;

        distanceText.text = "Distance: " + distance.ToString("0");
        coinsText.text = "Coins: " + coins.ToString("0");
        scoreText.text = "Score: " + score.ToString("0");
    }
}
