using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Shop : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI totalCoinsText;
    [SerializeField] private GameObject playerColorButton;
    [SerializeField] private GameObject platformColorButton;
    [SerializeField] private Transform playerButtonParent;
    [SerializeField] private Transform platformButtonParent;

    private void Start()
    {
        InstantiatePlayerButton();
        InstantiatePlatformButton();

        totalCoinsText.text = SaveManager.LoadTotalCoins().ToString("0");
    }

    private void InstantiatePlatformButton()
    {
        for (int i = 0; i < GameManager.instance.platformColor.Length; i++)
        {
            float colorPrice = GameManager.instance.platformColor[i].price;
            Color platformColor = GameManager.instance.platformColor[i].color;

            GameObject newButton = Instantiate(platformColorButton, platformButtonParent);
            newButton.GetComponent<UI_ShopButton>().SetupButton(colorPrice, platformColor);
        }
    }

    private void InstantiatePlayerButton()
    {
        for (int i = 0; i < GameManager.instance.playerColor.Length; i++)
        {
            float colorPrice = GameManager.instance.playerColor[i].price;
            Color playerColor = GameManager.instance.playerColor[i].color;

            GameObject newButton = Instantiate(playerColorButton, playerButtonParent);
            newButton.GetComponent<UI_ShopButton>().SetupButton(colorPrice, playerColor);
        }
    }
}
