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

    public static void SavePlayerSkinColor(float r, float g, float b)
    {
        PlayerPrefs.SetFloat("playerR", r);
        PlayerPrefs.SetFloat("playerG", g);
        PlayerPrefs.SetFloat("playerB", b);
    }

    public static Color LoadPlayerSkinColor()
    {
        float r = PlayerPrefs.GetFloat("playerR", 1);
        float g = PlayerPrefs.GetFloat("playerG", 1);
        float b = PlayerPrefs.GetFloat("playerB", 1);
        
        return new Color(r, g, b);

    }

    public static void SavePlatformHeadColor(float r, float g, float b)
    {
        PlayerPrefs.SetFloat("platformR", r);
        PlayerPrefs.SetFloat("platformG", g);
        PlayerPrefs.SetFloat("platformB", b);
    }

    public static Color LoadPlatformHeadColor()
    {
        float r = PlayerPrefs.GetFloat("platformR", 0);
        float g = PlayerPrefs.GetFloat("platformG", 1);
        float b = PlayerPrefs.GetFloat("platformB", 0.3f);

        return new Color(r, g, b);

    }

}
