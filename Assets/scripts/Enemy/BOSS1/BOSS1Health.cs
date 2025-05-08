using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSS1Health : MonoBehaviour
{
    public float maxHealth = 100f; // 角色的最大生命值
    public float currentHealth; // 角色的当前生命值
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
