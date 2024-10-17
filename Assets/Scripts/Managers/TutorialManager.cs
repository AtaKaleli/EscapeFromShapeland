using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [Header("Players")]
    [SerializeField] private Player player;
    [SerializeField] private GameObject playerAIPref;

    [Header("Collider Info")]
    [SerializeField] private GameObject tutorialCollider;
    [SerializeField] private float colliderOffsetX = 30.0f;

    [Header("Wait Times")]
    [SerializeField] private float enterTutorialWaitTime = 1.5f;
    [SerializeField] private float executeTutorialWaitTime = 4.0f;

    [Space]
    [SerializeField] private TextMeshProUGUI tutorialText;


    private string[] texts = { "Press Space", "Press Space Twice", "Press Shift", "Press Space" };
    private int phaseCounter;



 
    public void SpawnPlayerAI()
    {
        StartCoroutine(SpawnCouroutine());
    }
    private IEnumerator SpawnCouroutine()
    {
        if (IsFinalPhase())
        {
            GameManager.instance.SwitchToGame();
            yield break;
        }

        EnterTutorialPhase();
        yield return new WaitForSeconds(enterTutorialWaitTime);



        SetAIMovement(true);
        yield return new WaitForSeconds(executeTutorialWaitTime);
        ExecuteTutorialPhase();
    }

    private void EnterTutorialPhase()
    {
        tutorialText.gameObject.SetActive(false);
        SetCollider();
        SetPlayerMovement(false);
    }

    private void ExecuteTutorialPhase()
    {
        
        SetAIMovement(false);
        SetPlayerMovement(true);

        tutorialText.gameObject.SetActive(true);
        tutorialText.text = texts[phaseCounter];
        phaseCounter++;
    }

    private void SetPlayerMovement(bool canMove)
    {
        player.canMove = canMove;
    }

    private void SetAIMovement(bool isActive)
    {
        playerAIPref.SetActive(isActive);
    }
    
    private void SetCollider()
    {
        tutorialCollider.transform.position = new Vector2(player.transform.position.x + colliderOffsetX, tutorialCollider.transform.position.y);
    }

    private bool IsFinalPhase()
    {
        return phaseCounter == texts.Length;
    }

   
}
