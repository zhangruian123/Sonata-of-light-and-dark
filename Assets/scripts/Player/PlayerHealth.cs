using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthSlider; // 将UI血条拖拽到这个变量上
    public float maxHealth = 100f; // 角色的最大生命值
    public float currentHealth; // 角色的当前生命值
    public Text damageText;
    private SpriteRenderer spriteRenderer;
    public Animator anim;
    public GameObject GameOver;
    public GameObject EndGame;
    public GameObject Retry;
    private bool isDead = false; // 标记是否已经死亡，防止重复触发死亡逻辑
    private Coroutine resetColorCoroutine; // 用于存储当前的颜色恢复协程

    void Start()
    {
        currentHealth = maxHealth; // 初始化角色的生命值
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
        GameOver.SetActive(false);
        EndGame.SetActive(false);
        Retry.SetActive(false);
    }

    void Update()
    {
        healthSlider.value = currentHealth / maxHealth;
        damageText.text = $"{currentHealth}/{maxHealth}";
        CheckIfDead();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage; // 更新角色的生命值
        healthSlider.value = currentHealth / maxHealth; // 更新UI血条的值

        // 保存角色的原始颜色
        Color originalColor = spriteRenderer.color;

        Color flashColor = new Color(241f / 255f, 159f / 255f, 159f / 255f, 0.5f); // 红色，透明度为0.5
        spriteRenderer.color = flashColor;
        anim.SetBool("isHurt", true);

        // 停止之前的颜色恢复协程
        if (resetColorCoroutine != null) StopCoroutine(resetColorCoroutine);

        // 启动新的颜色恢复协程
        resetColorCoroutine = StartCoroutine(ResetColorCoroutine(spriteRenderer, originalColor));

        
    }

    public void isDie()
    {
        // 在这里添加角色死亡时的逻辑
        Debug.Log("角色死亡！");
        GameOver.SetActive(true); // 激活GameOver界面
        StartCoroutine(FadeInGameOver()); // 启动GameOver的淡入效果
        
    }

    private void CheckIfDead()
    {
        if (currentHealth <= 0 && !isDead)
        {
            isDead = true; // 标记角色已经死亡
            isDie(); // 调用死亡函数
        }
    }

    private IEnumerator ResetColorCoroutine(SpriteRenderer renderer, Color originalColor)
    {
        // 等待一段时间来显示红色效果
        yield return new WaitForSeconds(0.5f); // 增加延迟时间

        // 恢复角色的原始颜色
        renderer.color = originalColor;
        anim.SetBool("isHurt", false);
    }

    private IEnumerator FadeInGameOver()
    {
        // 假设GameOver有一个Image组件用于显示背景
        Image gameOverImage = GameOver.GetComponent<Image>();
        float alpha = 0f; // 初始透明度为0
        float fadeSpeed = 0.4f; // 淡入速度

        while (alpha < 1f)
        {
            alpha += fadeSpeed * Time.deltaTime;
            gameOverImage.color = new Color(1f, 1f, 1f, alpha); // 逐渐增加透明度
            yield return null;
        }
        EndGame.SetActive(true);
        Retry.SetActive(true);
    }
}