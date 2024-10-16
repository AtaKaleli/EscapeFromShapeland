using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Menu : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private TextMeshProUGUI lastScoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TextMeshProUGUI totalCoinsText;

    [Header("Volume Settings")]
    [SerializeField] private UI_Settings settings;

    [Header("Tutorial")]
    [SerializeField] private bool isTutorial;

    

    private void Start()
    {
        LoadSettings();
       
    }


    private void OnEnable()
    {
        DisplayInfo();
    }

    private void DisplayInfo()
    {
        if(!isTutorial)
        {
            lastScoreText.text = "Last Score: " + SaveManager.LoadLastScore().ToString("0");
            bestScoreText.text = "Best Score: " + SaveManager.LoadBestScore().ToString("0");
            totalCoinsText.text = SaveManager.LoadTotalCoins().ToString("0");
        }
    }

    public void TapToStart(GameObject ingameUI)
    {
        UI_Main.instance.SwitchToUI(ingameUI);
        GameManager.instance.GameStarted = true;
    }
    private void LoadSettings()
    {
        if(settings != null)
        {
            settings.SetupBGMVolumeSlider();
            settings.SetupSFXVolumeSlider();
            settings.SetupBackground();
        }
    }

    
   
}
