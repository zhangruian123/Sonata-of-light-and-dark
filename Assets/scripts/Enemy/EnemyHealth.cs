using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f; // ��ɫ���������ֵ
    public float currentHealth; // ��ɫ�ĵ�ǰ����ֵ
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; // ��ʼ����ɫ������ֵ
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
        currentHealth -= damage; // ���½�ɫ������ֵ

        // �����ɫ��ԭʼ��ɫ
        Color originalColor = spriteRenderer.color;

        Color flashColor = new Color(241f / 255f, 159f / 255f, 159f / 255f, 0.5f); // ��ɫ��͸����Ϊ0.5
        spriteRenderer.color = flashColor;

        // ����Э�̣��ȴ�һ��ʱ���ָ���ɫ��ɫ
        StartCoroutine(ResetColorCoroutine(spriteRenderer, originalColor));
    }

    private IEnumerator ResetColorCoroutine(SpriteRenderer renderer, Color originalColor)
    {
        yield return new WaitForSeconds(0.2f);

        // �ָ���ɫ��ԭʼ��ɫ
        renderer.color = originalColor;
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
