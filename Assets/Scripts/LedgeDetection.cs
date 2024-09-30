using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LedgeDetection : MonoBehaviour
{

    [SerializeField] private Player player;
    [SerializeField] private float ledgeCheckRadius;
    [SerializeField] private LayerMask whatIsGround;

    private bool canDetectLedge;


    private void Update()
    {
        if (canDetectLedge)
            player.isLedgeDetected = Physics2D.OverlapCircle(transform.position, ledgeCheckRadius, whatIsGround);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            canDetectLedge = false;
        }   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            canDetectLedge = true;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, ledgeCheckRadius);
    }



}