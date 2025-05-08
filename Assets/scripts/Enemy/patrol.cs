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
    public float detectionRange = 5f; // ���߼��ķ�Χ����
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
        LookForPlayer(); // ÿ֡��������Ҽ��
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
        // ���ߵ�����ǵ���λ�ã������ǵ��˳���ķ���
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = transform.right;
        if(!toRight)
        {
            rayDirection.x = -rayDirection.x;
        }
        // ���ߵĳ����� detectionRange ����
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, detectionRange, npcLayer);
        // �������ߣ����ڵ��ԣ�����Ϸ�в��ɼ���
        Debug.DrawRay(rayOrigin, rayDirection * detectionRange, Color.red);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
            if (hit.collider.CompareTag("Player")) // ������е�����ı�ǩ�� Player
            {
                findPlayer = true;
                player = hit.collider.gameObject;
            }
        }
        else
        {
            // ���û�д����κ����壬ȷ�� findPlayer Ϊ false
            findPlayer = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // �����ײ�������Ƿ������
        if(findPlayer)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                // �������ң����� TakeDamage ����
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(20f); // ����� 20f ����ײʱ��ɵ��˺���������Ը�����Ҫ����
                    anim.SetBool("ReachPlayer", true);
                    colliderToDisable.enabled = false;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // �����ײ�������Ƿ������
        if (findPlayer)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                // �������ң����� TakeDamage ����
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(20f); // ����� 20f ����ײʱ��ɵ��˺���������Ը�����Ҫ����
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