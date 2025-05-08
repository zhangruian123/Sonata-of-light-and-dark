using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeController : MonoBehaviour
{
    public string sceneName = "scene1"; // Ҫ���صĳ�������
    private CanvasGroup canvasGroup; // ���ڿ��Ƶ��뵭���� CanvasGroup
    private float fadeSpeed = 0.5f; // ÿ֡���뵭�����ٶ�

    private void Start()
    {
        // ��ȡ CanvasGroup ���
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup component not found on the GameObject.");
        }
        else
        {
            // ��ʼ����Ч��
            StartCoroutine(FadeIn());
        }
    }

    private IEnumerator FadeIn()
    {
        // ����Ч��
        float elapsedTime = 0f;
        while (elapsedTime < 2f)
        {
            canvasGroup.alpha = elapsedTime / 2f;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f;

        // �ȴ�2��󵭳��������³���
        yield return new WaitForSeconds(2f);
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        // ����Ч��
        float elapsedTime = 0f;
        while (elapsedTime < 2f)
        {
            canvasGroup.alpha = 1f - (elapsedTime / 2f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0f;

        // �����³���
        SceneManager.LoadScene(sceneName);
    }
}