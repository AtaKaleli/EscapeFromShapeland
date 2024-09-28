using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{

    [SerializeField] private GameObject coinPref;
    [SerializeField] private Transform coinParent;
    [SerializeField] private int minCoinLimit;
    [SerializeField] private int maxCoinLimit;
    [SerializeField] private int spawnChance;


    private void Start()
    {
        if (spawnChance < Random.Range(0, 100))
            return;

        int randomCoinNumber = Random.Range(minCoinLimit, maxCoinLimit);
        
        for (int i = - randomCoinNumber / 2; i <= randomCoinNumber / 2; i++)
        {
            Instantiate(coinPref, new Vector2(transform.position.x + i, transform.position.y), Quaternion.identity, coinParent);
        }
    }






}
