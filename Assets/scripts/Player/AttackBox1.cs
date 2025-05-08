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
        // 检测碰撞对象的tag是否为"Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // 获取Enemy对象的EnemyScript脚本组件
            EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                // 调用Enemy的TakeDamage方法
                enemy.TakeDamage(damageAmount);
            }
        }
    }
}
