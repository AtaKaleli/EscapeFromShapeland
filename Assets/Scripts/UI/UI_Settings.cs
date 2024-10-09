using Cinemachine;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI_Settings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private float mixerMultiplier = 25f;

    [Header("Background Music Settings")]
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private string bgmParameter;

    [Header("Sound Effects Settings")]
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private string sfxParameter;

    [Header("Background Selection")]
    [SerializeField] private CinemachineConfiner2D virtualCamera;
    [SerializeField] private GameObject[] backgrounds;

    private int currentBackgroundId = 0;



    #region Volume Settings
    public void SetupSFXVolumeSlider()
    {
        sfxSlider.onValueChanged.AddListener(SFXSliderValue);
        sfxSlider.minValue = 0.001f;
        sfxSlider.value = SaveManager.LoadSFXValue();
    }

    public void SetupBGMVolumeSlider()
    {
        bgmSlider.onValueChanged.AddListener(BGMSliderValue);
        bgmSlider.minValue = 0.001f;
        bgmSlider.value = SaveManager.LoadBGMValue();

    }

    private void SFXSliderValue(float value)
    {
        float newValue = Mathf.Log10(value) * mixerMultiplier;
        audioMixer.SetFloat(sfxParameter, newValue);
    }

    private void BGMSliderValue(float value)
    {
        float newValue = Mathf.Log10(value) * mixerMultiplier;
        audioMixer.SetFloat(bgmParameter, newValue);

    }

    private void OnDisable()
    {
        SaveManager.SaveBGMValue(bgmSlider.value);
        SaveManager.SaveSFXValue(sfxSlider.value);
    }

    #endregion

    #region Background Selection

    public void SetupBackground()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            if (SaveManager.LoadBackground(i) == 1)
            {
                currentBackgroundId = i;
                break;
            }
        }

        SelectBackground(currentBackgroundId);
    }

    public void SelectBackground(int id)
    {

        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].SetActive(false);
            SaveManager.SaveBackground(i, 0);
        }

        backgrounds[id].SetActive(true);
        SaveManager.SaveBackground(id, 1);

        PolygonCollider2D colliderHolder = backgrounds[id].transform.GetChild(0).GetComponent<PolygonCollider2D>();
        virtualCamera.m_BoundingShape2D = colliderHolder;
    }
    #endregion
}
