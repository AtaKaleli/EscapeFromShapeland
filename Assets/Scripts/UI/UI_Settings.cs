using System.Collections;
using System.Collections.Generic;
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
}
