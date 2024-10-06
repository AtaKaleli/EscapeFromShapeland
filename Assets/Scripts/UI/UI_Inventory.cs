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

    [Header("Color Data")]
    [SerializeField] private SpriteRenderer playerSr;

    private void Start()
    {
        platformPreview.color = SaveManager.LoadPlatformHeadColor();
        playerPreview.color = SaveManager.LoadPlayerSkinColor();
    }

    private void OnEnable()
    {
        InstantiatePlayerButton();
        InstantiatePlatformButton();
    }

    private void OnDisable()
    {
        playerSr.color = SaveManager.LoadPlayerSkinColor();
        DeletePlatformButtons();
        DeletePlayerButtons();
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

            newButton.GetComponent<UI_ShopButton>().SetupButton(colorPrice, platformHeadColor);
            newButton.GetComponent<Button>().onClick.AddListener(() => SetPlatformPreview(platformHeadColor));
        }
    }

    private void DeletePlatformButtons()
    {
        for (int i = 0; i < platformButtonParent.transform.childCount; i++)
        {
            Destroy(platformButtonParent.GetChild(i).gameObject);
        }
    }

    private void DeletePlayerButtons()
    {
        for (int i = 0; i < playerButtonParent.transform.childCount; i++)
        {
            Destroy(playerButtonParent.GetChild(i).gameObject);
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
            newButton.GetComponent<UI_ShopButton>().SetupButton(colorPrice, playerSkinColor);
            newButton.GetComponent<Button>().onClick.AddListener(() => SetPlayerPreview(playerSkinColor));
        }
    }

    private void SetPlatformPreview(Color platformHeadColor)
    {
        platformPreview.color = platformHeadColor;
        SaveManager.SavePlatformHeadColor(platformHeadColor.r, platformHeadColor.g, platformHeadColor.b);
    }

    private void SetPlayerPreview(Color playerSkinColor)
    {
        playerPreview.color = playerSkinColor;
        SaveManager.SavePlayerSkinColor(playerSkinColor.r, playerSkinColor.g, playerSkinColor.b);
    }

    public void SetDefaultColor()
    {
        playerPreview.color = new Color(255, 255, 255); //DEFAULT COLOR FOR PLAYER
        platformPreview.color = new Color(0, 255, 78); //DEFAULT COLOR FOR PLATFORM HEAD
        SaveManager.SavePlayerSkinColor(255, 255, 255);
        SaveManager.SavePlatformHeadColor(0, 255, 78);
    }




}

