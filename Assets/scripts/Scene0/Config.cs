using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Config : MonoBehaviour
{
    public Color targetColor = Color.black; // ����Ŀ����ɫ����ɫ��
    public Color textColor = Color.white; // ����Ŀ����ɫ����ɫ��
    private Image imageComponent; // ���� Image ���
    private Text textComponent; // ���ڻ�ȡ Text ���
    private Color originalColor; // ����ԭʼ��ɫ
    private Color originalTextColor; // ����ԭʼ��ɫ
    public GameObject selectImage;

    void Start()
    {
        // ��ȡ Image ���
        imageComponent = GetComponent<Image>();

        // ��ȡ Text ���
        textComponent = GetComponentInChildren<Text>();

        if (imageComponent != null)
        {
            originalColor = imageComponent.color;
        }
        else
        {
            Debug.LogError("Image component not found on the GameObject.");
        }

        if (textComponent != null)
        {
            originalTextColor = textComponent.color;
        }
        else
        {
            Debug.LogError("Text component not found on the GameObject or its children.");
        }

        selectImage.SetActive(false);
    }

    void Update()
    {
        // ��ȡ�������Ļ�ϵ�λ��
        Vector3 mousePosition = Input.mousePosition;

        // ����Ļ����ת��Ϊ��������
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));

        // �������Ƿ�������ķ�Χ��
        if (IsMouseOverObject(worldPosition))
        {
            // �����������巶Χ�ڣ�����ΪĿ����ɫ
            if (imageComponent != null)
            {
                imageComponent.color = targetColor;
            }

            // ����������ɫΪĿ����ɫ
            if (textComponent != null)
            {
                textComponent.color = textColor;
            }

            selectImage.SetActive(true);
        }
        else
        {
            // �����겻�����巶Χ�ڣ��ָ�ԭʼ��ɫ
            if (imageComponent != null)
            {
                imageComponent.color = originalColor;
            }

            // �ָ�����ԭʼ��ɫ
            if (textComponent != null)
            {
                textComponent.color = originalTextColor;
            }

            selectImage.SetActive(false);
        }
    }

    bool IsMouseOverObject(Vector3 worldPosition)
    {
        // ��ȡ�����RectTransform
        RectTransform rectTransform = GetComponent<RectTransform>();

        if (rectTransform != null)
        {
            // ����������ת��Ϊ��Ļ����
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, Camera.main, out Vector2 localPoint);

            // �������Ƿ���RectTransform�ķ�Χ��
            return rectTransform.rect.Contains(localPoint);
        }

        return false;
    }
}
