using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSS1Health : MonoBehaviour
{
    public float maxHealth = 100f; // ��ɫ���������ֵ
    public float currentHealth; // ��ɫ�ĵ�ǰ����ֵ
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
