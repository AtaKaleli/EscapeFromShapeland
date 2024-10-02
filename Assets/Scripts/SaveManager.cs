using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SaveManager : MonoBehaviour
{

    public static SaveManager instance;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }


    public void SaveLastScore(float score)
    {
        PlayerPrefs.SetFloat("LastScore", score);
    }

    public float LoadLastScore()
    {
        return PlayerPrefs.GetFloat("LastScore", 0);
    }

    public void SaveBestScore(float score)
    {
        PlayerPrefs.SetFloat("BestScore", score);
    }

    public float LoadBestScore()
    {
        return PlayerPrefs.GetFloat("BestScore", 0);
    }

    public void SaveTotalCoins(int coins)
    {
        PlayerPrefs.SetInt("TotalCoins", coins);
    }

    public int LoadTotalCoins()
    {
        return PlayerPrefs.GetInt("TotalCoins", 0);
    }
}
