using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ShopButton : MonoBehaviour
{

    

    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Image playerImage;
    public GameObject soldImage;
    [SerializeField] private bool isInventory;

    public void SetupButton(float price, Color color)
    {
        if(!isInventory)
            priceText.text = price.ToString("0");
        playerImage.color = color;
    }


}
