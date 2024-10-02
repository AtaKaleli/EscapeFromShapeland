using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PauseScreen : MonoBehaviour
{
    public void ResumeGame(GameObject ingameUI)
    {
        UI_Main.instance.SwitchToUI(ingameUI);
        GameManager.instance.ResumeGameButton();
    }
}
