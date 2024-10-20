using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    [Header("Spawn Items")]
    [SerializeField] private Transform[] levelPartPref;
    [SerializeField] private Transform levelPartParent;
    [SerializeField] private Vector3 respawnPoint;

    [Header("Spawn-Delete Distance")]
    [SerializeField] private float distanceToSpawn;
    [SerializeField] private int childToDelete;

    [Header("Player Info")]
    public Player player;
    private Transform playerTransform;

    

    void Start()
    {
        playerTransform = player.transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        DistanceToSpawnPlatform();
        DistanceToDeletePlatform();   
    }

    private void LevelPartGenerator()
    {
        int randomIndex = Random.Range(0, levelPartPref.Length);
        Transform levelPart = Instantiate(levelPartPref[randomIndex], new Vector2(respawnPoint.x,respawnPoint.y + 2.21f), Quaternion.identity, levelPartParent);
        Vector3 nextPosition = levelPart.Find("EndPoint").position - levelPart.Find("StartPoint").position;
        respawnPoint = new Vector3(levelPart.position.x + nextPosition.x, nextPosition.y, nextPosition.z);
    }

    private void DistanceToSpawnPlatform()
    {
        if(respawnPoint.x - playerTransform.position.x < distanceToSpawn)
            LevelPartGenerator();
    }

    private void DistanceToDeletePlatform()
    {
        if (levelPartParent.childCount > childToDelete)
            Destroy(levelPartParent.GetChild(0).gameObject);
    }

}
