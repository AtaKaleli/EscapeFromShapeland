using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Main : MonoBehaviour
{

    
    [SerializeField] private GameObject[] uiElements;


    private void Awake()
    {
        Time.timeScale = 1;
    }

    private void Start()
    {
        

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

        uiToOpen.SetActive(true);
    }

    public void TapToStart(GameObject ingameUI)
    {
        uiElements[0].SetActive(false);
        ingameUI.SetActive(true);
        GameManager.instance.isGameStarted = true;
    }

    public void PauseGame(GameObject pauseUI)
    {
        SwitchToUI(pauseUI);
        GameManager.instance.PauseGameButton();
    }
    
    public void RestartGame(GameObject ingameUI)
    {
        SwitchToUI(ingameUI);
        GameManager.instance.ResumeGameButton();
    }
    


}
