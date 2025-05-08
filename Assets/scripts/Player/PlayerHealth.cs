using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthSlider; // ��UIѪ����ק�����������
    public float maxHealth = 100f; // ��ɫ���������ֵ
    public float currentHealth; // ��ɫ�ĵ�ǰ����ֵ
    public Text damageText;
    private SpriteRenderer spriteRenderer;
    public Animator anim;
    public GameObject GameOver;
    public GameObject EndGame;
    public GameObject Retry;
    private bool isDead = false; // ����Ƿ��Ѿ���������ֹ�ظ����������߼�
    private Coroutine resetColorCoroutine; // ���ڴ洢��ǰ����ɫ�ָ�Э��

    void Start()
    {
        currentHealth = maxHealth; // ��ʼ����ɫ������ֵ
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
        currentHealth -= damage; // ���½�ɫ������ֵ
        healthSlider.value = currentHealth / maxHealth; // ����UIѪ����ֵ

        // �����ɫ��ԭʼ��ɫ
        Color originalColor = spriteRenderer.color;

        Color flashColor = new Color(241f / 255f, 159f / 255f, 159f / 255f, 0.5f); // ��ɫ��͸����Ϊ0.5
        spriteRenderer.color = flashColor;
        anim.SetBool("isHurt", true);

        // ֹ֮ͣǰ����ɫ�ָ�Э��
        if (resetColorCoroutine != null) StopCoroutine(resetColorCoroutine);

        // �����µ���ɫ�ָ�Э��
        resetColorCoroutine = StartCoroutine(ResetColorCoroutine(spriteRenderer, originalColor));

        
    }

    public void isDie()
    {
        // ��������ӽ�ɫ����ʱ���߼�
        Debug.Log("��ɫ������");
        GameOver.SetActive(true); // ����GameOver����
        StartCoroutine(FadeInGameOver()); // ����GameOver�ĵ���Ч��
        
    }

    private void CheckIfDead()
    {
        if (currentHealth <= 0 && !isDead)
        {
            isDead = true; // ��ǽ�ɫ�Ѿ�����
            isDie(); // ������������
        }
    }

    private IEnumerator ResetColorCoroutine(SpriteRenderer renderer, Color originalColor)
    {
        // �ȴ�һ��ʱ������ʾ��ɫЧ��
        yield return new WaitForSeconds(0.5f); // �����ӳ�ʱ��

        // �ָ���ɫ��ԭʼ��ɫ
        renderer.color = originalColor;
        anim.SetBool("isHurt", false);
    }

    private IEnumerator FadeInGameOver()
    {
        // ����GameOver��һ��Image���������ʾ����
        Image gameOverImage = GameOver.GetComponent<Image>();
        float alpha = 0f; // ��ʼ͸����Ϊ0
        float fadeSpeed = 0.4f; // �����ٶ�

        while (alpha < 1f)
        {
            alpha += fadeSpeed * Time.deltaTime;
            gameOverImage.color = new Color(1f, 1f, 1f, alpha); // ������͸����
            yield return null;
        }
        EndGame.SetActive(true);
        Retry.SetActive(true);
    }
}