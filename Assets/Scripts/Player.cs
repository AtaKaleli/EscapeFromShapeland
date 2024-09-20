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

    [Header("Collision Detection - Ground")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGroundDetected;



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

        Move();

        CollisionChecks();
        AnimationContoller();
    }

    private void AnimationContoller()
    {
        anim.SetFloat("xVelocity", rb.velocity.x);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isGroundDetected", isGroundDetected);
    }

    private void InputChecks()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGroundDetected)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
            isGameStarted = true;
    }


    #region Move & Jump

    private void Move()
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }

    
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
    #endregion



    private void CollisionChecks()
    {
        isGroundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
    }

}
