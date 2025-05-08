using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeController : MonoBehaviour
{
    public string sceneName = "scene1"; // 要加载的场景名称
    private CanvasGroup canvasGroup; // 用于控制淡入淡出的 CanvasGroup
    private float fadeSpeed = 0.5f; // 每帧淡入淡出的速度

    private void Start()
    {
        // 获取 CanvasGroup 组件
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup component not found on the GameObject.");
        }
        else
        {
            // 开始淡入效果
            StartCoroutine(FadeIn());
        }
    }

    private IEnumerator FadeIn()
    {
        // 淡入效果
        float elapsedTime = 0f;
        while (elapsedTime < 2f)
        {
            canvasGroup.alpha = elapsedTime / 2f;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f;

        // 等待2秒后淡出并加载新场景
        yield return new WaitForSeconds(2f);
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        // 淡出效果
        float elapsedTime = 0f;
        while (elapsedTime < 2f)
        {
            canvasGroup.alpha = 1f - (elapsedTime / 2f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0f;

        // 加载新场景
        SceneManager.LoadScene(sceneName);
    }
}