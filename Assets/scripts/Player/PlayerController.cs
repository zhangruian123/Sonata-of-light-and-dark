using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float walkSpeed;
    public float jumpSpeed;
    public Rigidbody2D rb;
    public bool onGround;
    public bool isAttacking;
    public bool isSecondAttacking;
    private float xVelocity;
    public Animator anim;
    public SpriteRenderer spriteRenderer;
    public int jumpCount;
    private bool isSliding;     // 是否正在滑行
    private float slideRemainingDistance; // 剩余滑行距离
    public float slideDistance; // 滑行距离
    public float slideSpeed;    // 滑行速度
    private bool isSprinting;
    public float sprintDistance;
    public float sprintSpeed;
    private float sprintRemainingDistance;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        isAttacking = false;
        onGround = true;
        isSecondAttacking = false;
        jumpCount = 0;
        isSliding = false;
        slideRemainingDistance = 0f;
        isSprinting = false;
        sprintRemainingDistance = 0f;
    }

    private void Update()
    {
        ifOnGround();
        if(!isAttacking)
        {
            if (onGround)
            {
                GroundMovement();
                checkJump();
                if(!isSprinting&&!isSliding)
                {
                    checkFirstAttack();
                }
                if (Input.GetKeyDown(KeyCode.LeftShift) && !isSliding && onGround&&!isSprinting)
                {
                    StartSlide();
                }
                if (Input.GetKeyDown(KeyCode.L) && !isSliding && onGround && !isSprinting)
                {
                    StartSprint();
                }
            }
            else
            {
                checkSecondJump();
            }
        }
        else
        {
            checkSecondAttack();
        }
        UpdateSlide();
        UpdateSprint();
    }

    public void ifOnGround()
    {
        float groundRayDistance = 0.1f;
        LayerMask backgroundLayer = LayerMask.GetMask("background");
        Collider2D collider = GetComponent<Collider2D>();
        if (collider == null)
        {
            Debug.LogError("Collider2D component not found on the gameObject.");
            return;
        }
        Vector2 rayOrigin = collider.bounds.center + new Vector3(0, -collider.bounds.extents.y, 0);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, groundRayDistance, backgroundLayer);
        Debug.DrawRay(rayOrigin, Vector2.down * groundRayDistance, Color.red);

        // 如果射线击中了物体
        if (hit.collider != null)
        {
            // 检测击中物体的tag
            if (hit.collider.CompareTag("Ground"))
            {
                
                onGround = true;
                if (jumpCount == 1) Debug.Log("jumpCount Changed");
                jumpCount = 0;
            }
            else
            {
                onGround = false;
                
            }
        }
        else
        {
            
            onGround = false;
        }

        anim.SetBool("isGround", onGround);
    }

    public void GroundMovement()
    {
        xVelocity = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(walkSpeed * xVelocity, rb.velocity.y);
        if (xVelocity != 0)
        {
            transform.localScale = new Vector3(xVelocity, 1, 1);
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }

    public void checkJump()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (onGround)
            {
                // 第一段跳跃
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                onGround = false;
                jumpCount++;
                Debug.Log($"jumpcound{jumpCount}");
            }
        }
    }

    public void checkSecondJump()
    {
        if (jumpCount==1 && Input.GetKeyDown(KeyCode.K))
        {
            // 第二段跳跃
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            jumpCount++;
            anim.SetBool("BeginSecondJump", true);
            
        }
    }

    public void checkFirstAttack()
    {
        if (!isAttacking && Input.GetKeyDown(KeyCode.J))
        {
            isAttacking = true;
            anim.SetBool("isAttacking", true);
        }
    }

    public void checkSecondAttack()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            isSecondAttacking = true;
            anim.SetBool("isSecondAttacking", true);
        }
    }

    public void closeFirstAttack()
    {
        if (!isSecondAttacking)
        {
            isAttacking = false;
            anim.SetBool("isAttacking", false);
        }

    }

    public void closeSecondAttack()
    {
        anim.SetBool("isSecondAttacking", false);
        isSecondAttacking = false;
        anim.SetBool("isAttacking", false);
        isAttacking = false;
    }

    public void closeSecondJumpAnimation()
    {
        anim.SetBool("BeginSecondJump", false);
    }

    public void StartSlide()
    {
        isSliding = true;
        slideRemainingDistance = slideDistance;
        float backforward = -transform.localScale.x;
        rb.velocity = new Vector2(backforward * slideSpeed, rb.velocity.y);
        anim.SetBool("isSliding", true);
    }

    public void StartSprint()
    {
        isSprinting = true;
        sprintRemainingDistance = sprintDistance; // 使用冲刺距离
        float forward = transform.localScale.x;
        rb.velocity = new Vector2(forward * sprintSpeed, rb.velocity.y); // 使用冲刺速度
        anim.SetBool("isSprinting", true);
    }

    public void UpdateSlide()
    {
        if (isSliding)
        {
            // 计算当前滑行速度方向
            float backforward = -transform.localScale.x;
            rb.velocity = new Vector2(backforward * slideSpeed, rb.velocity.y);

            // 减少剩余滑行距离
            slideRemainingDistance -= Mathf.Abs(backforward * slideSpeed * Time.deltaTime);

            // 如果滑行距离用完，停止滑行
            if (slideRemainingDistance <= 0)
            {
                StopSlide();
            }
        }
    }

    public void UpdateSprint()
    {
        if (isSprinting)
        {
            // 计算当前冲刺速度方向
            float forward = transform.localScale.x;
            rb.velocity = new Vector2(forward * sprintSpeed, rb.velocity.y); // 使用冲刺速度

            // 减少剩余冲刺距离
            sprintRemainingDistance -= Mathf.Abs(forward * sprintSpeed * Time.deltaTime);

            // 如果冲刺距离用完，停止冲刺
            if (sprintRemainingDistance <= 0)
            {
                StopSprint();
            }
        }
    }

    public void StopSprint()
    {
        isSprinting = false;
        anim.SetBool("isSprinting", false);
    }

    public void StopSlide()
    {
        isSliding = false;
        anim.SetBool("isSliding", false);
    }

}