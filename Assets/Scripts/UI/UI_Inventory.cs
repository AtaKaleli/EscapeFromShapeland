using UnityEngine;
using UnityEngine.UI;







public class UI_Inventory : MonoBehaviour
{
    [Header("Shop - Item")]
    [Space]
    public ShopInformation[] playerColor;
    public ShopInformation[] platformColor;


    [Header("Shop - Text")]



    [Header("Shop - Button")]
    [SerializeField] private GameObject playerColorButton;
    [SerializeField] private GameObject platformColorButton;
    [SerializeField] private Transform playerButtonParent;
    [SerializeField] private Transform platformButtonParent;

    [Header("Shop - Preview")]
    [SerializeField] private Image playerPreview;
    [SerializeField] private Image platformPreview;


    private void Start()
    {
        InstantiatePlayerButton();
        InstantiatePlatformButton();





    }

    private void InstantiatePlatformButton()
    {
        for (int i = 0; i < platformColor.Length; i++)
        {
            int colorPrice = platformColor[i].price;
            Color platformHeadColor = platformColor[i].color;
            var index = i;
            string type = "PlatformHead";

            bool isSold = PlayerPrefs.GetInt(type + index, 0) == 1;

            if (!isSold)
                continue;

            GameObject newButton = Instantiate(platformColorButton, platformButtonParent);

            newButton.GetComponent<UI_ShopButton>().SetupButton(colorPrice, platformHeadColor, index, type);
            newButton.GetComponent<Button>().onClick.AddListener(() => SetPlatformPreview(platformHeadColor));



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

            bool isSold = PlayerPrefs.GetInt(type + index, 0) == 1;

            if (!isSold)
                continue;

            GameObject newButton = Instantiate(playerColorButton, playerButtonParent);
            newButton.GetComponent<UI_ShopButton>().SetupButton(colorPrice, playerSkinColor, index, type);
            newButton.GetComponent<Button>().onClick.AddListener(() => SetPlayerPreview(playerSkinColor));




        }
    }



    private void SetPlatformPreview(Color platformHeadColor)
    {
        platformPreview.color = platformHeadColor;
    }

    private void SetPlayerPreview(Color playerSkinColor)
    {
        playerPreview.color = playerSkinColor;
    }

    public void SetDefaultColor()
    {
        playerPreview.color = new Color(255, 255, 255, 255); //DEFAULT COLOR FOR PLAYER
        platformPreview.color = new Color(0, 255, 78, 255); //DEFAULT COLOR FOR PLATFORM HEAD
    }




}

