using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;

    private bool isGameStarted;

    [Header("Player Inputs")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private bool canDoubleJump;



    [Header("Collision Detection - Ground")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded;

    [Header("Collision Detection - Wall")]
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Vector2 wallCheckSize;
    [SerializeField] private float wallCheckDistance;
    private bool isWallDetected;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    


    void Update()
    {
        InputChecks();

        if (!isGameStarted)
            return;

        if (isGrounded)
        {
            canDoubleJump = true;
        }

        Move();

        CollisionChecks();
        AnimationContoller();
    }

    private void AnimationContoller()
    {
        anim.SetFloat("xVelocity", rb.velocity.x);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isDoubleJumping", !canDoubleJump);
    }

    private void InputChecks()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpController();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
            isGameStarted = true;
    }


    #region Move & Jump

    private void Move()
    {
        if(!isWallDetected)
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }

    
    private void Jump(float jumpMultiplier)
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce * jumpMultiplier);
    }

    private void JumpController()
    {
        if (isGrounded)
        {
            Jump(1f);
        }
        else if(canDoubleJump)
        {
            
            canDoubleJump = false;
            Jump(.8f);
        }

    }

    #endregion



    private void CollisionChecks()
    {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        isWallDetected = Physics2D.BoxCast(wallCheck.position, wallCheckSize, 0, Vector2.zero, 0, whatIsGround);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawWireCube(wallCheck.position, wallCheckSize);
    }

}
