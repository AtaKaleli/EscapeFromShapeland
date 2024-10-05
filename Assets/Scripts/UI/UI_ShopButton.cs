using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ShopButton : MonoBehaviour
{

    //[SerializeField] private Image playerImage;
    [HideInInspector] public int buttonIndex;
    [HideInInspector] public string buttonType;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Image playerImage;
    public GameObject soldImage;
    [HideInInspector] public bool isAvailable = true;

    public void SetupButton(float price, Color color, int index, string type)
    {
        priceText.text = price.ToString("0");
        playerImage.color = color;
        buttonIndex = index;
        buttonType = type;
    }


}
