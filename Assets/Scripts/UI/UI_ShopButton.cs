using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ShopButton : MonoBehaviour
{

    //[SerializeField] private Image playerImage;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Image playerImage;


    public void SetupButton(float price, Color color)
    {
        priceText.text = price.ToString("0");
        playerImage.color = color;

    }
}
