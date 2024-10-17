using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAI : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;


    [Header("Player Inputs")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private bool canDoubleJump;

    [Header("Cayote Jump")]
    [SerializeField] private float cayoteJumpTime;
    private float cayoteJumpCounter;
    private bool canCayoteJump;

    [Header("Collision Detection - Ground")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded;

    [Header("Collision Detection - Wall")]
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Vector2 wallCheckSize;
    private bool isWallDetected;

    [Header("Player - Slide Ability")]
    [SerializeField] private float slideTime;
    private float slideTimeCounter;
    [SerializeField] private float upperGroundCheckDistance;
    private bool isUpperGrounded;
    [HideInInspector] public bool isSliding;
    [HideInInspector] public bool canSlide = true;

    [Header("Ledge Climb ")]
    [SerializeField] private Vector2 offset1;
    [SerializeField] private Vector2 offset2;
     public bool isLedgeDetected;
    private Vector2 climbBegunPosition;
    private Vector2 climbOverPosition;
    private bool canClimb;
    private bool canGrabLedge = true;


    public bool canMove;
    private bool jumpToLedge = true;

    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }




    private void Update()
    {

        slideTimeCounter -= Time.deltaTime;
        cayoteJumpCounter -= Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.Space))
        {
            //JumpController();
        }


        AnimationController();
        LedgeCheck();
        CancelSlideAbility();
        AllowJumpAbilities();
        Move();
        CollisionChecks();
    }




    private void AnimationController()
    {
        anim.SetFloat("xVelocity", rb.velocity.x);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isDoubleJumping", !canDoubleJump);
        anim.SetBool("isSliding", isSliding);
        anim.SetBool("canClimb", canClimb);
    }

    #region Move & Jump

    private void Move()
    {
        if (!isWallDetected || isSliding)
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        else
            rb.velocity = new Vector2(0, rb.velocity.y);
    }

    private void Jump(float jumpMultiplier)
    {
        if (!isSliding)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * jumpMultiplier);
    }

    private void JumpController()
    {
        if (isGrounded || cayoteJumpCounter > 0)
        {
            Jump(1f);
            AudioManager.instance.PlaySfx(6);
        }
        else if (canDoubleJump)
        {
            AudioManager.instance.PlaySfx(2);
            canDoubleJump = false;
            Jump(.8f);
        }

    }

    private void AllowJumpAbilities()
    {
        if (isGrounded)
        {
            canDoubleJump = true;
            canCayoteJump = true;
        }
        else
        {
            if (canCayoteJump)
            {
                canCayoteJump = false;
                cayoteJumpCounter = cayoteJumpTime;
            }
        }
    }

    #endregion

    #region Slide Ability
    private void SlideController()
    {
        if (canSlide && isGrounded)
        {
            AudioManager.instance.PlaySfx(11);
            slideTimeCounter = slideTime;
            isSliding = true;
            canSlide = false;
        }

    }

    private void CancelSlideAbility()
    {
        if (slideTimeCounter < 0 && !isUpperGrounded)
        {
            isSliding = false;
            canSlide = true;
        }
    }


    #endregion

    #region LedgeClimb
    private void LedgeCheck()
    {
        if (isLedgeDetected && canGrabLedge)
        {

            AudioManager.instance.PlaySfx(12);
            canGrabLedge = false;

            Vector2 ledgePosition = GetComponentInChildren<LedgeDetection>().transform.position;

            climbBegunPosition = ledgePosition + offset1;
            climbOverPosition = ledgePosition + offset2;

            canClimb = true;
        }
        if (canClimb)
        {

            transform.position = climbBegunPosition;
        }
    }

    private void LedgeCheckOver()
    {
        canClimb = false;
        transform.position = climbOverPosition;
        Invoke("AllowLedgeCheck", 0.1f);
    }

    private void AllowLedgeCheck()
    {
        canGrabLedge = true;
    }

    #endregion



    private void CollisionChecks()
    {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        
        if (!isSliding)
            isWallDetected = Physics2D.BoxCast(wallCheck.position, wallCheckSize, 0, Vector2.zero, 0, whatIsGround);
        
        isUpperGrounded = Physics2D.Raycast(transform.position, Vector2.up, upperGroundCheckDistance, whatIsGround);
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawWireCube(wallCheck.position, wallCheckSize);
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + upperGroundCheckDistance));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "JumpPhase")
        {
          
            Jump(1f);
        }
        else if(collision.tag == "DoubleJumpPhase")
        {
            
            JumpController();
            Invoke("JumpController", .25f);
        }
        else if (collision.tag == "SlidePhase")
        {
            
            SlideController();
        }
        else if (collision.tag == "LedgeClimbPhase" && jumpToLedge)
        {
            jumpToLedge = false;
            Jump(1f);
            Invoke("allow", 1f);
        }




    }

    private void allow()
    {
        jumpToLedge = true;
    }

}
