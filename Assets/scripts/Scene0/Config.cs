using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Config : MonoBehaviour
{
    public Color targetColor = Color.black; // 物体目标颜色（黑色）
    public Color textColor = Color.white; // 文字目标颜色（白色）
    private Image imageComponent; // 用于 Image 组件
    private Text textComponent; // 用于获取 Text 组件
    private Color originalColor; // 物体原始颜色
    private Color originalTextColor; // 文字原始颜色
    public GameObject selectImage;

    void Start()
    {
        // 获取 Image 组件
        imageComponent = GetComponent<Image>();

        // 获取 Text 组件
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
        // 获取鼠标在屏幕上的位置
        Vector3 mousePosition = Input.mousePosition;

        // 将屏幕坐标转换为世界坐标
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));

        // 检测鼠标是否在物体的范围内
        if (IsMouseOverObject(worldPosition))
        {
            // 如果鼠标在物体范围内，设置为目标颜色
            if (imageComponent != null)
            {
                imageComponent.color = targetColor;
            }

            // 设置文字颜色为目标颜色
            if (textComponent != null)
            {
                textComponent.color = textColor;
            }

            selectImage.SetActive(true);
        }
        else
        {
            // 如果鼠标不在物体范围内，恢复原始颜色
            if (imageComponent != null)
            {
                imageComponent.color = originalColor;
            }

            // 恢复文字原始颜色
            if (textComponent != null)
            {
                textComponent.color = originalTextColor;
            }

            selectImage.SetActive(false);
        }
    }

    bool IsMouseOverObject(Vector3 worldPosition)
    {
        // 获取物体的RectTransform
        RectTransform rectTransform = GetComponent<RectTransform>();

        if (rectTransform != null)
        {
            // 将世界坐标转换为屏幕坐标
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, Camera.main, out Vector2 localPoint);

            // 检查鼠标是否在RectTransform的范围内
            return rectTransform.rect.Contains(localPoint);
        }

        return false;
    }
}
