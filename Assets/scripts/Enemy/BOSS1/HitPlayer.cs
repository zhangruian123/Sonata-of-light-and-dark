using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour
{
    public float Damage;
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 检查碰撞的物体是否是玩家
        
            if (collision.gameObject.CompareTag("Player"))
            {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
                // 如果是玩家，调用 TakeDamage 方法
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(Damage); 
                }
            }
        
    }
}
