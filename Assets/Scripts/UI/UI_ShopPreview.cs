using TMPro;
using UnityEngine;
using UnityEngine.UI;






public class UI_ShopPreview : MonoBehaviour
{
    [Header("Shop - Item")]
    [Space]
    public ShopInformation[] playerColor;
    public ShopInformation[] platformColor;

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

            GameObject newButton = Instantiate(platformColorButton, platformButtonParent);

            newButton.GetComponent<UI_ShopButton>().SetupButton(colorPrice, platformHeadColor);
            newButton.GetComponent<Button>().onClick.AddListener(() => SetPlatformPreview(platformHeadColor));
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


}
