using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Menu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lastScoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TextMeshProUGUI totalCoinsText;

    private void Start()
    {
        DisplayInfo();
    }

    private void DisplayInfo()
    {


        lastScoreText.text = "Last Score: " + SaveManager.instance.LoadLastScore().ToString("0");
        bestScoreText.text = "Best Score: " + SaveManager.instance.LoadBestScore().ToString("0");
        totalCoinsText.text = SaveManager.instance.LoadTotalCoins().ToString("0");
    }

    public void TapToStart(GameObject ingameUI)
    {
        UI_Main.instance.SwitchToUI(ingameUI);
        GameManager.instance.isGameStarted = true;
    }
}
