using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class patrol : MonoBehaviour
{
    public bool findPlayer;
    private Animator anim;
    public bool toRight;
    public Transform leftPoint;
    public Transform rightPoint;
    public float patrolSpeed;
    public float runSpeed;
    public GameObject player;
    public float detectionRange = 5f; // 射线检测的范围长度
    public LayerMask npcLayer;
    public PlayerHealth playerHealth;
    public Collider2D colliderToDisable;

    void Start()
    {
        findPlayer = false;
        toRight = true;
        anim= gameObject.GetComponent<Animator>();
        colliderToDisable = gameObject.GetComponent<Collider2D>();
    }

    void Update()
    {
        LookForPlayer(); // 每帧都进行玩家检测
        if (findPlayer)
        {
            findMovement();
        }
        else
        {
            patrolMovement();
        }
    }

    public void findMovement()
    {
        if (player != null)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.Translate(direction.x * runSpeed * Time.deltaTime, 0, 0);
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            anim.SetBool("FindPlayer", true);
            playerHealth = player.GetComponent<PlayerHealth>();
        }
    }

    public void patrolMovement()
    {
        Transform targetPosition;
        if (toRight)
        {
            targetPosition = rightPoint;
        }
        else
        {
            targetPosition = leftPoint;
        }
        if(targetPosition.position.x-transform.position.x>0)
        {
            transform.Translate(patrolSpeed * Time.deltaTime, 0, 0);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.Translate(-patrolSpeed * Time.deltaTime, 0, 0);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (Mathf.Abs(transform.position.x - leftPoint.position.x) <= 0.1f)
        {
            toRight = true;
        }
        if (Mathf.Abs(transform.position.x - rightPoint.position.x) <= 0.1f)
        {
            toRight = false;
        }
        anim.SetBool("FindPlayer", false);
    }

    public void LookForPlayer()
    {
        // 射线的起点是敌人位置，方向是敌人朝向的方向
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = transform.right;
        if(!toRight)
        {
            rayDirection.x = -rayDirection.x;
        }
        // 射线的长度由 detectionRange 决定
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, detectionRange, npcLayer);
        // 绘制射线（用于调试，在游戏中不可见）
        Debug.DrawRay(rayOrigin, rayDirection * detectionRange, Color.red);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
            if (hit.collider.CompareTag("Player")) // 如果打中的物体的标签是 Player
            {
                findPlayer = true;
                player = hit.collider.gameObject;
            }
        }
        else
        {
            // 如果没有打中任何物体，确保 findPlayer 为 false
            findPlayer = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 检查碰撞的物体是否是玩家
        if(findPlayer)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                // 如果是玩家，调用 TakeDamage 方法
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(20f); // 这里的 20f 是碰撞时造成的伤害量，你可以根据需要调整
                    anim.SetBool("ReachPlayer", true);
                    colliderToDisable.enabled = false;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // 检查碰撞的物体是否是玩家
        if (findPlayer)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                // 如果是玩家，调用 TakeDamage 方法
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(20f); // 这里的 20f 是碰撞时造成的伤害量，你可以根据需要调整
                    anim.SetBool("ReachPlayer", true);
                    colliderToDisable.enabled = false;
                }
            }
        }
    }
    public void DestoryEnemy()
    {
        Destroy(gameObject);
    }
}