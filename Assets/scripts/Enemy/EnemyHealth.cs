using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f; // 角色的最大生命值
    public float currentHealth; // 角色的当前生命值
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; // 初始化角色的生命值
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        DieOrNot();
    }

    public void DieOrNot()
    {
        if(currentHealth<=0)
        {
            BOSS1patrol boss1Patrol = gameObject.GetComponent<BOSS1patrol>();
            if(boss1Patrol==null)
            {
                Destroy(gameObject);
            }
            else
            {
                boss1Patrol.isDie = true;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage; // 更新角色的生命值

        // 保存角色的原始颜色
        Color originalColor = spriteRenderer.color;

        Color flashColor = new Color(241f / 255f, 159f / 255f, 159f / 255f, 0.5f); // 红色，透明度为0.5
        spriteRenderer.color = flashColor;

        // 启动协程，等待一段时间后恢复角色颜色
        StartCoroutine(ResetColorCoroutine(spriteRenderer, originalColor));
    }

    private IEnumerator ResetColorCoroutine(SpriteRenderer renderer, Color originalColor)
    {
        yield return new WaitForSeconds(0.2f);

        // 恢复角色的原始颜色
        renderer.color = originalColor;
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
