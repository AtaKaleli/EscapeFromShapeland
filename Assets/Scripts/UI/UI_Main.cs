using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Main : MonoBehaviour
{
    public static UI_Main instance;
    
    [SerializeField] private GameObject[] uiElements;
    private UI_FadeEffect fadeEffect;


    
    private void Awake()
    {
        Time.timeScale = 1;

        if (instance == null)
            instance = this;
        else
            Destroy(this);

        

    }

    private void Start()
    {
        GameManager.instance.FadeOutScreen();

        for (int i = 1; i < uiElements.Length; i++)
        {
            uiElements[i].SetActive(false);
        }
        uiElements[0].SetActive(true);
    }

    public void SwitchToUI(GameObject uiToOpen)
    {

        for (int i = 0; i < uiElements.Length; i++)
        {
            uiElements[i].SetActive(false);
        }
        AudioManager.instance.PlaySfx(8);
        uiToOpen.SetActive(true);
    }

 
    


}
