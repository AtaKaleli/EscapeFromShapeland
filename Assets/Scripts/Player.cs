using System.Collections;
using UnityEngine;


public class Player : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    private TutorialManager tutorialManager;

    

    [Header("Player Inputs")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private bool canDoubleJump;
    [HideInInspector] public bool canMove = true;

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
    [HideInInspector] public bool isUpperGrounded;
    [HideInInspector] public bool isSliding;
    [HideInInspector] public bool canSlide = true;


    [Header("Ledge Climb ")]
    [SerializeField] private Vector2 offset1;
    [SerializeField] private Vector2 offset2;
    [HideInInspector] public bool isLedgeDetected;
    private Vector2 climbBegunPosition;
    private Vector2 climbOverPosition;
    private bool canClimb;
    private bool canGrabLedge = true;
    

    [Header("Speed Up Milestones")]
    [SerializeField] private float milestoneDistance;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float distanceMultiplier;
    [SerializeField] private float maxMoveSpeed;
    private float defaultMoveSpeed;
    private float defaultMilestoneDistance;
    private float tempDistance = 100f;

    //Player Roll Information
    private bool isRolling;

    [Header("Knockback")]
    [SerializeField] private float knockbackTime;
    [SerializeField] private Vector2 knockbackDirection;
    private bool isKnocked;
    [HideInInspector] public bool canBeKnocked = true;


    //Player Die Information
    [HideInInspector] public bool isDead;
    [HideInInspector] public bool hasExtraLife;

    //UI Information
    [SerializeField] private GameObject endGameUI;

    //particle system
    [SerializeField] private ParticleSystem bloodSplatter;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        tutorialManager = FindAnyObjectByType<TutorialManager>();

        isDead = false;
        defaultMoveSpeed = 8f;
        defaultMilestoneDistance = 100f;
    }

    private void Start()
    {
        sr.color = SaveManager.LoadPlayerSkinColor(); 
    }

    private void Update()
    {

        slideTimeCounter -= Time.deltaTime;
        cayoteJumpCounter -= Time.deltaTime;

   
        

        AnimationContoller();
        CollisionChecks();
        
        if (isDead || isKnocked)
            return;


        if (!GameManager.instance.GameStarted)
            return;

       
        
        InputChecks();
        AllowJumpAbilities();
        CancelSlideAbility();
        RollController();
        LedgeCheck();
        SpeedUpController();
        Move();
    }

    

    private void AnimationContoller()
    {
        anim.SetFloat("xVelocity", rb.velocity.x);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isDoubleJumping", !canDoubleJump);
        anim.SetBool("isSliding", isSliding);
        anim.SetBool("canClimb", canClimb);
        anim.SetBool("isRolling", isRolling);
        anim.SetBool("isKnocked", isKnocked);
        anim.SetBool("isDead", isDead);
    }

    private void InputChecks()
    {
        if (!canMove)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpController();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SlideController();
        }
    }


    #region Move & Jump

    private void Move()
    {
        if(!canMove)
            rb.velocity = new Vector2(0, rb.velocity.y);
        else if (!isWallDetected || isSliding)
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }


    private void Jump(float jumpMultiplier)
    {
        if(!isSliding)
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
        if(isLedgeDetected && canGrabLedge)
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

    #region Speed Up & Down

    private void SpeedUpController()
    {
        
        if (moveSpeed == maxMoveSpeed)
        {
            GameManager.instance.TookDamage = false;
            hasExtraLife = true;
            return;
        }
        
        if(transform.position.x > milestoneDistance)
        {
            milestoneDistance = transform.position.x + tempDistance * distanceMultiplier;
            tempDistance *= distanceMultiplier;
            moveSpeed *= speedMultiplier;
            
            if (moveSpeed >= maxMoveSpeed)
            {
                AudioManager.instance.PlaySfx(4);
                moveSpeed = maxMoveSpeed;
            }
        }
    }

    private void SlowDownController()
    {
       
        moveSpeed = defaultMoveSpeed;
        tempDistance = defaultMilestoneDistance;
        milestoneDistance = transform.position.x + tempDistance;
    }

    #endregion

    #region Roll 
    private void RollController()
    {
        if (rb.velocity.y < -25f && isGrounded && canGrabLedge)
        {
            Time.timeScale = 0.6f;
            AudioManager.instance.PlaySfx(13);
            isRolling = true;
        }
    }

    public void CancelRolling()
    {
        Time.timeScale = 1.0f;
        isRolling = false;
    }

    #endregion

    #region Knockback & Die
    public void Damage()
    {
        if (hasExtraLife)
        {
            Knockback();
        }
        else
        {
            Die();
        }
    }

    private void Die()
    {
        if (canBeKnocked)
        {
            bloodSplatter.Play();
            AudioManager.instance.PlaySfx(7);
            StartCoroutine(DieCouroutine());
        }
    }

    private IEnumerator DieCouroutine()
    {
        Time.timeScale = 0.6f;
        AudioManager.instance.StopBgm();
        isDead = true;
        rb.velocity = knockbackDirection;
        yield return new WaitForSeconds(1f);
        rb.velocity = new Vector2(0, 0);
        GameManager.instance.Distance = transform.position.x;
        yield return new WaitForSeconds(1f);
        UI_Main.instance.SwitchToUI(endGameUI);
        Time.timeScale = 1.0f;
        GameManager.instance.TookDamage = false;
    }

    private void Knockback()
    {
        if (canBeKnocked)
            StartCoroutine(KnockbackCouroutine());
    }

    private IEnumerator KnockbackCouroutine()
    {
        bloodSplatter.Play();
        AudioManager.instance.PlaySfx(3);
        SlowDownController();

        Color normalColor = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
        Color midAlphaColor = new Color(sr.color.r, sr.color.g, sr.color.b, 0.75f);
        Color lowAlphaColor = new Color(sr.color.r, sr.color.g, sr.color.b, 0.25f);

        hasExtraLife = false;
        isKnocked = true;
        canBeKnocked = false;
        rb.velocity = knockbackDirection;

        sr.color = midAlphaColor;
        yield return new WaitForSeconds(0.1f);
        sr.color = lowAlphaColor;
        yield return new WaitForSeconds(0.1f);

        sr.color = midAlphaColor;
        yield return new WaitForSeconds(0.15f);
        sr.color = lowAlphaColor;
        yield return new WaitForSeconds(0.15f);

        sr.color = midAlphaColor;
        yield return new WaitForSeconds(0.15f);
        sr.color = lowAlphaColor;
        yield return new WaitForSeconds(0.15f);

        isKnocked = false;

        sr.color = midAlphaColor;
        yield return new WaitForSeconds(0.2f);
        sr.color = lowAlphaColor;
        yield return new WaitForSeconds(0.2f);

        sr.color = midAlphaColor;
        yield return new WaitForSeconds(0.3f);
        sr.color = lowAlphaColor;
        yield return new WaitForSeconds(0.3f);

        sr.color = midAlphaColor;
        yield return new WaitForSeconds(0.4f);
        sr.color = lowAlphaColor;
        yield return new WaitForSeconds(0.4f);



        sr.color = normalColor;
        canBeKnocked = true;
        GameManager.instance.TookDamage = false;

    }

    #endregion

    #region Tutorial
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "StartTutorial")
        {
            tutorialManager.SpawnPlayerAI();
        }
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

    
}
