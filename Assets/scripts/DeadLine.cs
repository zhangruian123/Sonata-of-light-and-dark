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
        // �����ײ����ı�ǩ�Ƿ�Ϊ "Player"
        if (other.CompareTag("Player"))
        {
            Debug.Log("HitPlayer");
            // ��ȡ��ײ����� PlayerHealth �ű�
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // �� PlayerHealth �ű��е� currentHealth ����Ϊ 0
                playerHealth.currentHealth = 0;
            }
        }
    }
}