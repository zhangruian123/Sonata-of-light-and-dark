using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadLine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        // 检测碰撞物体的标签是否为 "Player"
        if (other.CompareTag("Player"))
        {
            Debug.Log("HitPlayer");
            // 获取碰撞物体的 PlayerHealth 脚本
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // 将 PlayerHealth 脚本中的 currentHealth 设置为 0
                playerHealth.currentHealth = 0;
            }
        }
    }
}