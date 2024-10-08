using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;


    [SerializeField] private AudioSource[] bgm;
    [SerializeField] private AudioSource[] sfx;

    [SerializeField] private Player player;

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

        if(!player.isDead)
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

    public void PlaySfx(int index, bool randomPitch = false)
    {
        

        if (sfx.Length == 0 || index >= sfx.Length)
            print("No SFX to play!");
        
        if (randomPitch)
            sfx[index].pitch = Random.Range(0.7f, 1.1f);
        
        sfx[index].Play();
        
    }

    #region Stop
    
    public void StopBgm()
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
    }

    public void StopSfx(int index)
    {
        sfx[index].Stop();
    }
    
    #endregion  
}
