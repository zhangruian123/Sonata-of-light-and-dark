using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour
{
    public float Damage;
    void OnCollisionEnter2D(Collision2D collision)
    {
        // �����ײ�������Ƿ������
        
            if (collision.gameObject.CompareTag("Player"))
            {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
                // �������ң����� TakeDamage ����
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(Damage); 
                }
            }
        
    }
}
