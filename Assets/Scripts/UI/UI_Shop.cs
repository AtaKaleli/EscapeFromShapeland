using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public enum ShopType { Shop,Preview}

[Serializable]
public struct ShopInformation
{
    public int price;
    public Color color;
}


public class UI_Shop : MonoBehaviour
{
    [Header("Shop - Item")]
    [Space]
    public ShopInformation[] playerColor;
    public ShopInformation[] platformColor;
    

    [Header("Shop - Text")]
    [SerializeField] private TextMeshProUGUI totalCoinsText;
    [SerializeField] private TextMeshProUGUI informationText;

    [Header("Shop - Button")]
    [SerializeField] private GameObject playerColorButton;
    [SerializeField] private GameObject platformColorButton;
    [SerializeField] private Transform playerButtonParent;
    [SerializeField] private Transform platformButtonParent;
  


 
    private void Start()
    {
        InstantiatePlayerButton();
        InstantiatePlatformButton();

        totalCoinsText.text = SaveManager.LoadTotalCoins().ToString("0");
        informationText.text = "Click to Buy";
        

        
    }

    private void InstantiatePlatformButton()
    {
        for (int i = 0; i < platformColor.Length; i++)
        {
            int colorPrice = platformColor[i].price;
            Color platformHeadColor = platformColor[i].color;
         

            GameObject newButton = Instantiate(platformColorButton, platformButtonParent);

            newButton.GetComponent<UI_ShopButton>().SetupButton(colorPrice, platformHeadColor);
            newButton.GetComponent<Button>().onClick.AddListener(() => BuyPlatformHeadColor(colorPrice, newButton));
        }
    }

    private void InstantiatePlayerButton()
    {
        for (int i = 0; i < playerColor.Length; i++)
        {
            int colorPrice = playerColor[i].price;
            Color playerSkinColor = playerColor[i].color;
       

            GameObject newButton = Instantiate(playerColorButton, playerButtonParent);
            newButton.GetComponent<UI_ShopButton>().SetupButton(colorPrice, playerSkinColor);
            newButton.GetComponent<Button>().onClick.AddListener(() => BuyPlayerSkinColor(colorPrice,newButton));
            

            
        }
    }

    

    private void BuyPlatformHeadColor(int colorPrice, GameObject newButton)
    {
        
        
        int totalCoins = SaveManager.LoadTotalCoins();
        
        if (totalCoins > colorPrice)
        {
            informationText.text = "Successfully Bought!";
            newButton.GetComponent<Button>().interactable = false;
            newButton.GetComponent<UI_ShopButton>().soldImage.SetActive(true);
            newButton.GetComponent<UI_ShopButton>().isAvailable = false;
            
        }
        else
            informationText.text = "Not Enough Coins!";
    }

    private void BuyPlayerSkinColor(int colorPrice, GameObject newButton)
    {
        
        
        int totalCoins = SaveManager.LoadTotalCoins();
        
        if (totalCoins > colorPrice)
        {
            informationText.text = "Successfully Bought!";
            newButton.GetComponent<Button>().interactable = false;
            newButton.GetComponent<UI_ShopButton>().soldImage.SetActive(true);
            newButton.GetComponent<UI_ShopButton>().isAvailable = false;
        }
        else
            informationText.text = "Not Enough Coins!";
    }

    
   
}
