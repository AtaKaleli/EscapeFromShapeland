using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Ingame : MonoBehaviour
{
    private Player player;

    [SerializeField] private GameObject slideReadyImage;
    [SerializeField] private GameObject heartFullImage;
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI coinsText;

    [SerializeField] private bool isTutorial;

    private void Awake()
    {
        if (player == null)
            player = FindAnyObjectByType<Player>();
    }

    

    private void Update()
    {
        UpdateInfo();
    }

    private void UpdateInfo()
    {
        if (!isTutorial)
        {
            distanceText.text = "Distance: " + player.transform.position.x.ToString("0");
            coinsText.text = "Coins: " + GameManager.instance.Coins.ToString("0");
            heartFullImage.SetActive(player.hasExtraLife);
            slideReadyImage.SetActive(player.canSlide);
        }
    }

    public void PauseGame(GameObject pauseUI)
    {
        UI_Main.instance.SwitchToUI(pauseUI);
        GameManager.instance.PauseGameButton();
    }

    public void SkipTutorial()
    {
        GameManager.instance.RestartGame();
    }
}
