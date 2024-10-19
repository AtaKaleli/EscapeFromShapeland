using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [Header("Object Types")]
    [SerializeField] private Transform trapParent;
    [SerializeField] private Transform platformParent;
    [SerializeField] private Transform coinGeneratorParent;

    private string trapTag = "Trap";
    private string platformTag = "Platform";
    private string coinGeneratorTag = "CoinGenerator";


    private void Start()
    {
        GameObject[] traps = GameObject.FindGameObjectsWithTag(trapTag);
        foreach (var trap in traps)
        {
            trap.transform.SetParent(trapParent);
        }

        GameObject[] platforms = GameObject.FindGameObjectsWithTag(platformTag);
        foreach (var platform in platforms)
        {
            platform.transform.SetParent(platformParent);
        }

        GameObject[] coinGenerators = GameObject.FindGameObjectsWithTag(coinGeneratorTag);
        foreach (var coinGenerator in coinGenerators)
        {
            coinGenerator.transform.SetParent(coinGeneratorParent);
        }
    }




}
