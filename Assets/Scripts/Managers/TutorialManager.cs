using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameObject playerAIPref;

    [SerializeField] private GameObject tutorialCollider;
    [SerializeField] private TextMeshProUGUI tutorialText;

    private string[] texts = { "Press Space", "Press Space Twice", "Press Shift", "Press Space" };
    private int phaseCounter;

    public void SpawnPlayerAI()
    {
        StartCoroutine(SpawnCouroutine());
    }
    private IEnumerator SpawnCouroutine()
    {
        
        tutorialText.gameObject.SetActive(false);
        tutorialCollider.transform.position = new Vector2(player.transform.position.x + 30, tutorialCollider.transform.position.y);
        player.canMove = false;
        yield return new WaitForSeconds(1.5f);

        if (phaseCounter == 4)
        {
            tutorialText.gameObject.SetActive(true);
            tutorialText.text = "Have Fun !";
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene("SampleScene");
        }

        playerAIPref.SetActive(true);
        yield return new WaitForSeconds(4f);
        playerAIPref.SetActive(false);
        player.canMove = true;
        
        if(phaseCounter == 3)
        {
            yield return new WaitForSeconds(1.5f);
        }
        tutorialText.gameObject.SetActive(true);

        tutorialText.text = texts[phaseCounter];
        phaseCounter++;
    }

    
}
