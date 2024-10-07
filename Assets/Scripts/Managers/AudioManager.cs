using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;


    [SerializeField] private AudioSource[] bgm;
    [SerializeField] private AudioSource[] sfx;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        InvokeRepeating("PlayMusicIfNeeded", 0, 1f);
    }


    private void PlayMusicIfNeeded()
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            if (bgm[i].isPlaying)
                return;
        }

        PlayRandomBgm();
    }

    private void PlayRandomBgm()
    {
        int randomIndex = Random.Range(0, bgm.Length);
        PlayBgm(randomIndex);
    }

    private void PlayBgm(int index)
    {
        if (bgm.Length == 0 || index >= bgm.Length)
            print("No BGM to play!");

        bgm[index].Play();
    }

    private void PlaySfx(int index)
    {
        if (sfx.Length == 0 || index >= sfx.Length)
            print("No SFX to play!");

        sfx[index].Play();
    }

    #region Stop
    /*
    private void StopBgm(int index)
    {
        if (bgm[index].isPlaying)
            bgm[index].Stop();
    }

    private void StopSfx(int index)
    {
        sfx[index].Stop();
    }
    */
    #endregion  
}
