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
    private bool isWallDetected;





    [Header("Player - Slide Ability")]
    [SerializeField] private float slideTime;
    private float slideTimeCounter;
    [SerializeField] private float upperGroundCheckDistance;
    private bool isUpperGrounded;
    private bool isSliding;
    private bool canSlide = true;




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }




    void Update()
    {

        slideTimeCounter -= Time.deltaTime;

        if (slideTimeCounter < 0 && !isUpperGrounded)
        {
            isSliding = false;
            canSlide = true;
        }





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
        anim.SetBool("isSliding", isSliding);
    }

    private void InputChecks()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpController();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
            isGameStarted = true;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SlideController();
        }
    }


    #region Move & Jump

    private void Move()
    {
        if (!isWallDetected || isSliding)
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }


    private void Jump(float jumpMultiplier)
    {
        if(!isSliding)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * jumpMultiplier);
    }

    private void JumpController()
    {
        if (isGrounded)
        {
            Jump(1f);
        }
        else if (canDoubleJump)
        {

            canDoubleJump = false;
            Jump(.8f);
        }

    }

    #endregion

    private void SlideController()
    {

        if (canSlide && isGrounded)
        {
            slideTimeCounter = slideTime;
            isSliding = true;
            canSlide = false;
        }

    }



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

}
