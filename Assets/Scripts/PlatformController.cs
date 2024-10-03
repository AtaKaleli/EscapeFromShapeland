using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private SpriteRenderer headerSr;
    [SerializeField] private Transform platformHeader;


    private void Start()
    {
        platformHeader.parent = transform.parent;
        headerSr.transform.localScale = new Vector2(sr.bounds.size.x, 0.2f);
        headerSr.transform.position = new Vector2(sr.transform.position.x, sr.bounds.max.y);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            headerSr.color = GameManager.instance.platformHeadColor;
        }
    }



}