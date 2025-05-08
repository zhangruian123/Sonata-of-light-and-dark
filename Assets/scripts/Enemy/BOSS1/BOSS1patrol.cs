using System.Collections;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class BOSS1patrol : MonoBehaviour
{
    [Header("组件")]
    public Animator anim;
    public Collider2D colliderToDisable;

    [Header("状态")]
    public bool findPlayer;
    public bool isAttack1;
    public bool isAttack2;
    public bool isBigAttack;
    public bool isDie;
    public bool toRight;
    public bool Beginsprint;
    public bool isCooldown;

    [Header("外部游戏物体")]
    public Transform leftPoint;
    public Transform rightPoint;
    public GameObject player;

    [Header("移动部分")]
    public float patrolSpeed;
    public float sprintSpeed;
    public float sprintDistance;
    public float sprintDistanceTraveled;

    [Header("射线检测")]
    public float detectionRange;
    public LayerMask npcLayer;

    [Header("攻击冷却部分")]
    private float cooldownTimer; // 用于记录冷却时间

    

    private void Start()
    {
        colliderToDisable = gameObject.GetComponent<Collider2D>();
        anim = gameObject.GetComponent<Animator>();
        findPlayer = false;
        isAttack1 = false;
        isAttack2 = false;
        isBigAttack = false;
        isDie = false;
        toRight = true;
        Beginsprint = false;
        sprintDistanceTraveled = 0; // 初始化冲刺距离
        isCooldown = false; // 初始化冷却状态为 false
        cooldownTimer = 0; // 初始化冷却计时器
    }

    private void Update()
    {
        SetAnimatorBool();
        if (!isDie)
        {
            find();

            if (!findPlayer)
            {
                PatrolMovement();
            }
            else
            {
                DecideAttack();
            }

            if (isCooldown)
            {
                Cooldown();
            }

            if (Beginsprint)
            {
                sprint();
            }
        }
        else
        {
            colliderToDisable.enabled = false;
        }
        
        
    }

    private void DecideAttack()
    {
        if (!Beginsprint && !isCooldown&&!isAttack1&&!isAttack2&&!isBigAttack)
        {
            int randomValue = Random.Range(0, 4); // 生成0到3的随机数
            if (randomValue == 0)
            {
                Beginsprint = true; // 开始冲刺
                
            }
            if(randomValue==1)
            {
                Attack1();
            }
            if(randomValue==2)
            {
                Attack2();
            }
            if(randomValue==3)
            {
                BigAttack();
            }
        }
    }

    private void SetAnimatorBool()
    {
        anim.SetBool("isAttack1", isAttack1);
        anim.SetBool("isAttack2", isAttack2);
        anim.SetBool("isBigAttack", isBigAttack);
        anim.SetBool("isDie", isDie);
    }

    public void PatrolMovement()
    {
        Transform targetPosition;
        if(toRight)
        {
            targetPosition = rightPoint;
        }
        else
        {
            targetPosition = leftPoint;
        }
        if (targetPosition.position.x - transform.position.x > 0)
        {
            transform.Translate(patrolSpeed * Time.deltaTime, 0, 0);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.Translate(-patrolSpeed * Time.deltaTime, 0, 0);
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (Mathf.Abs(transform.position.x - leftPoint.position.x) <= 0.1f)
        {
            toRight = true;
        }
        if (Mathf.Abs(transform.position.x - rightPoint.position.x) <= 0.1f)
        {
            toRight = false;
        }
    }

    public void find()
    {
        Vector3 rayOrigin = new Vector3(transform.position.x, transform.position.y - 2, transform.position.z);
        Vector3 rayRightDirection = transform.right;
        RaycastHit2D RightHit = Physics2D.Raycast(rayOrigin, rayRightDirection, detectionRange, npcLayer);
        Vector3 rayLeftDirection = -transform.right;
        RaycastHit2D LeftHit = Physics2D.Raycast(rayOrigin, rayLeftDirection, detectionRange, npcLayer);
        Debug.DrawRay(rayOrigin, rayRightDirection * detectionRange, Color.red);
        Debug.DrawRay(rayOrigin, rayLeftDirection * detectionRange, Color.red);
        if (RightHit.collider != null)
        {
            
            if (RightHit.collider.CompareTag("Player")) // 如果打中的物体的标签是 Player
            {
                findPlayer = true;
                player = RightHit.collider.gameObject;
            }
        }
        else
        {
            if (LeftHit.collider != null)
            {
                
                if (LeftHit.collider.CompareTag("Player")) // 如果打中的物体的标签是 Player
                {
                    findPlayer = true;
                    player = LeftHit.collider.gameObject;
                }
            }
            else
            {
                findPlayer = false;
            }
        }

        if(findPlayer)
        {
            if (player != null)
            {
                Transform targetPosition = player.transform;
                if (targetPosition.position.x - transform.position.x > 0)
                {
                    
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    
                    transform.localScale = new Vector3(1, 1, 1);
                }
            }
        }
    }

    public void sprint()
    {
        if(player!=null)
        {
            Transform targetPosition = player.transform;
            if (targetPosition.position.x - transform.position.x > 0)
            {
                transform.Translate(sprintSpeed * Time.deltaTime, 0, 0);
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.Translate(-sprintSpeed * Time.deltaTime, 0, 0);
                transform.localScale = new Vector3(1, 1, 1);
            }

            // 更新冲刺距离
            sprintDistanceTraveled += sprintSpeed * Time.deltaTime;

            // 检查是否达到冲刺距离
            if (sprintDistanceTraveled >= sprintDistance)
            {
                Beginsprint = false;
                sprintDistanceTraveled = 0; // 重置冲刺距离
                StartCooldown();
            }
        }
    }

    private void StartCooldown()
    {
        isCooldown = true; // 设置冷却状态为 true
        cooldownTimer = 0; // 重置冷却计时器
    }

    private void Cooldown()
    {
        cooldownTimer += Time.deltaTime; // 更新冷却计时器
        if (cooldownTimer >= 2f) // 检查是否冷却时间已到
        {
            isCooldown = false; // 结束冷却状态
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 检查碰撞的物体是否是玩家
        if(Beginsprint)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if(player!=null)
                {
                    PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                    playerHealth.TakeDamage(30);

                    
                }
                Beginsprint = false; // 结束冲刺
                sprintDistanceTraveled = 0; // 重置冲刺距离
                StartCooldown();
            }
        }
    }

    public void Attack1()
    {
        isAttack1 = true;

    }

    public void Attack2()
    {
        isAttack2 = true;
    }

    public void BigAttack()
    {
        isBigAttack = true;
    }

    public void closeAttack1()
    {
        isAttack1 = false;
        StartCooldown();
    }

    public void closeAttack2()
    {
        isAttack2 = false;
        StartCooldown();
    }

    public void closeBigAttack()
    {
        isBigAttack = false;
        StartCooldown();
    }
}