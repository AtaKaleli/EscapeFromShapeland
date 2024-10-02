using UnityEngine;


public static class SaveManager
{

    public static void SaveLastScore(float score)
    {
        PlayerPrefs.SetFloat("LastScore", score);
    }

    public static float LoadLastScore()
    {
        return PlayerPrefs.GetFloat("LastScore", 0);
    }

    public static void SaveBestScore(float score)
    {
        PlayerPrefs.SetFloat("BestScore", score);
    }

    public static float LoadBestScore()
    {
        return PlayerPrefs.GetFloat("BestScore", 0);
    }

    public static void SaveTotalCoins(int coins)
    {
        PlayerPrefs.SetInt("TotalCoins", coins);
    }

    public static int LoadTotalCoins()
    {
        return PlayerPrefs.GetInt("TotalCoins", 0);
    }
}
