using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LedgeDetection : MonoBehaviour
{

    [SerializeField] private Player player;
    [SerializeField] private float ledgeCheckRadius;
    [SerializeField] private LayerMask whatIsGround;

    private bool canDetectLedge;
    private BoxCollider2D boxCd => GetComponent<BoxCollider2D>();

    private void Update()
    {
        if (canDetectLedge && !player.isSliding)
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
        Collider2D[] colliders = Physics2D.OverlapBoxAll(boxCd.bounds.center, boxCd.size, 0);

        foreach (var hit in colliders)
        {
            if (hit.gameObject.GetComponent<PlatformController>() != null)
                return;
        }

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