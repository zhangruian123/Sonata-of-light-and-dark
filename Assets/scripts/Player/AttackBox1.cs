using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBox1 : MonoBehaviour
{
    // Start is called before the first frame update
    public float damageAmount = 10f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �����ײ�����tag�Ƿ�Ϊ"Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // ��ȡEnemy�����EnemyScript�ű����
            EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                // ����Enemy��TakeDamage����
                enemy.TakeDamage(damageAmount);
            }
        }
    }
}
