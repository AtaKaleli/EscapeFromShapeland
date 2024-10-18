using Cinemachine;
using System.Collections;
using UnityEngine;


public class RandomPicker : MonoBehaviour
{
    private Player player;
    private PostProcessController postProcessController;

    [SerializeField] private float spawnChance;
    [SerializeField] private float pickerTime = 5.0f;
    

    //Random Property - RotateScreen
    private CinemachineVirtualCamera virtualCamera;
    private float defaultOrthoSize = 8.0f;
    private float defaultScreenX = 0.1f;

    //Random Property - ReduceVision
    


    private void Awake()
    {
        if (spawnChance < Random.Range(0, 100) || !GameManager.instance.CanSpawn)
            Destroy(gameObject);
        
        if(virtualCamera == null)
            virtualCamera = FindAnyObjectByType<CinemachineVirtualCamera>();
        
        if (player == null)
            player = FindAnyObjectByType<Player>();

        if (postProcessController == null)
            postProcessController = FindAnyObjectByType<PostProcessController>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            int randomPickerValue = Random.Range(0, 101);
            PickerDecider(randomPickerValue);
        }
    }



    private void PickerDecider(int randomPickerValue)
    {
        if (randomPickerValue > 0 && randomPickerValue <= 20)
            StartCoroutine(ImmunePlayerCoroutine());
        else if (randomPickerValue > 20 && randomPickerValue <= 40)
            StartCoroutine(RotateScreenCoroutine());
        else if (randomPickerValue > 40 && randomPickerValue <= 60)
            StartCoroutine(DisableSpriteCoroutine());
        else if (randomPickerValue > 60 && randomPickerValue <= 80)
            StartCoroutine(ReduceVisionCoroutine());
        else if (randomPickerValue > 80 && randomPickerValue <= 100)
            StartCoroutine(IncreaseLensDistortionCoroutine());
    }

    private IEnumerator ImmunePlayerCoroutine()
    {
        DisableRandomPicker();

        player.canBeKnocked = false;
        SpriteRenderer sr = player.GetComponent<SpriteRenderer>();

        Color normalColor = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
        Color midAlphaColor = new Color(sr.color.r, sr.color.g, sr.color.b, 0.75f);
        Color lowAlphaColor = new Color(sr.color.r, sr.color.g, sr.color.b, 0.25f);

        for (int i = 0; i < 5; i++)
        {
            sr.color = lowAlphaColor;
            yield return new WaitForSeconds(0.5f);
            sr.color = midAlphaColor;
            yield return new WaitForSeconds(0.5f);
        }

        sr.color = normalColor;
        player.canBeKnocked = true;


        EnableRandomPicker();
    }

    private IEnumerator RotateScreenCoroutine()
    {
        DisableRandomPicker();

        virtualCamera.m_Lens.OrthographicSize = defaultOrthoSize * -1.0f;
        virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = 0.9f;

        yield return new WaitForSeconds(pickerTime);
        
        virtualCamera.m_Lens.OrthographicSize = defaultOrthoSize;
        virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = defaultScreenX;

        EnableRandomPicker();
    }

    private IEnumerator DisableSpriteCoroutine()
    {
        DisableRandomPicker();

        player.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(pickerTime);
        player.GetComponent<SpriteRenderer>().enabled = true;

        EnableRandomPicker();
    }

    private IEnumerator ReduceVisionCoroutine()
    {
        DisableRandomPicker();

        postProcessController.vignetteStatus = true;
        yield return new WaitForSeconds(pickerTime);
        postProcessController.vignetteStatus = false;

        EnableRandomPicker();
    }

    private IEnumerator IncreaseLensDistortionCoroutine()
    {
        DisableRandomPicker();

        postProcessController.lensDistortionStatus = true;
        yield return new WaitForSeconds(pickerTime);
        postProcessController.lensDistortionStatus = false;

        EnableRandomPicker();
    }

    private void DisableRandomPicker()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        GameManager.instance.CanSpawn = false;
    }

    private void EnableRandomPicker()
    {
        GameManager.instance.CanSpawn = true;
        Destroy(gameObject);
    }

}
