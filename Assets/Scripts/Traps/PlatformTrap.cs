using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformTrap : Trap
{
    private Rigidbody2D rb;


    [Header("Collision Detection - Player")]
    [SerializeField] private Transform playerCheck;
    [SerializeField] private float playerCheckDistance;
    [SerializeField] private LayerMask whatIsPlayer;
    private bool isPlayerDetected;



    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        PlayerChecks();

    }

    private void PlayerChecks()
    {
        isPlayerDetected = Physics2D.Raycast(playerCheck.position, Vector2.down, playerCheckDistance, whatIsPlayer);

        if (isPlayerDetected)
            rb.gravityScale = 15;

    }

    

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(playerCheck.position, new Vector2(playerCheck.position.x, playerCheck.position.y - playerCheckDistance));
        
    }

    
}
