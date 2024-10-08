using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;




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

    #region Instantiate Buttons
    private void InstantiatePlatformButton()
    {
        for (int i = 0; i < platformColor.Length; i++)
        {
            int colorPrice = platformColor[i].price;
            Color platformHeadColor = platformColor[i].color;
            var index = i;
            string type = "PlatformHead";

            bool isSold = SaveManager.LoadSoldItem(index,type) == 1;


            GameObject newButton = Instantiate(platformColorButton, platformButtonParent);
            newButton.GetComponent<UI_ShopButton>().SetupButton(colorPrice, platformHeadColor);
            newButton.GetComponent<Button>().onClick.AddListener(() => BuyPlatformHeadColor(colorPrice, newButton, index, type));

            if (isSold)//if item is sold, still instantiate the buttons, but make them not interactable
                ItemSold(newButton, index, type);
        }
    }

    private void InstantiatePlayerButton()
    {
        for (int i = 0; i < playerColor.Length; i++)
        {
            int colorPrice = playerColor[i].price;
            Color playerSkinColor = playerColor[i].color;
            var index = i;
            string type = "PlayerSkin";

            bool isSold = SaveManager.LoadSoldItem(index, type) == 1;

            GameObject newButton = Instantiate(playerColorButton, playerButtonParent);
            newButton.GetComponent<UI_ShopButton>().SetupButton(colorPrice, playerSkinColor);
            newButton.GetComponent<Button>().onClick.AddListener(() => BuyPlayerSkinColor(colorPrice, newButton, index, type));

            //if item is sold, still instantiate the buttons, but make them not interactable
            if (isSold)
                ItemSold(newButton, index, type);
        }
    }

    #endregion

    private void BuyPlatformHeadColor(int colorPrice, GameObject newButton, int index, string type)
    {
        int totalCoins = SaveManager.LoadTotalCoins();

        if (totalCoins >= colorPrice)
        {
            AudioManager.instance.PlaySfx(0);
            ItemSold(newButton, index, type);
            UpdateTotalCoins(totalCoins, colorPrice);
            StartCoroutine(InformationTextCouroutine("Successfully Purchased!"));
        }
        else
        {
            AudioManager.instance.PlaySfx(10);
            StartCoroutine(InformationTextCouroutine("Not Enough Coins!"));

        }
    }

    private void BuyPlayerSkinColor(int colorPrice, GameObject newButton, int index, string type)
    {
        int totalCoins = SaveManager.LoadTotalCoins();

        if (totalCoins >= colorPrice)
        {
            AudioManager.instance.PlaySfx(0);
            ItemSold(newButton, index, type);
            UpdateTotalCoins(totalCoins, colorPrice);
            StartCoroutine(InformationTextCouroutine("Successfully Purchased!"));
        }
        else
        {
            AudioManager.instance.PlaySfx(10);
            StartCoroutine(InformationTextCouroutine("Not Enough Coins!"));
        }
    }

    private IEnumerator InformationTextCouroutine(string text)
    {
        informationText.text = text;
        yield return new WaitForSeconds(1f);
        informationText.text = "Click to Buy";
    }

    private void ItemSold(GameObject newButton, int index, string type)
    {
        newButton.GetComponent<Button>().interactable = false;
        newButton.GetComponent<UI_ShopButton>().soldImage.SetActive(true);
        SaveManager.SaveSoldItem(index, type);
        
    }

    private void UpdateTotalCoins(int totalCoins, int colorPrice)
    {
        totalCoins -= colorPrice;
        SaveManager.SaveTotalCoins(totalCoins);
        totalCoinsText.text = SaveManager.LoadTotalCoins().ToString("0");
    }




}
