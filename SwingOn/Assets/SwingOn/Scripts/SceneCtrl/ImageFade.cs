using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFade : MonoBehaviour
{
    public Image fadeImage;

    void Start()
    {
        StartCoroutine(FadeEffect(false));
    }

    void Update()
    {
        
    }

    //�ش� ���� �̹����� ������ �������� ȿ��

    IEnumerator FadeEffect(bool isFadeIn)
    {
        float timer = 0;
        while (timer <= 1f)
        {
            yield return null;
            timer += Time.unscaledDeltaTime * 0.5f;
            float a = isFadeIn ? Mathf.Lerp(0f, 1f, timer) : Mathf.Lerp(1f, 0f, timer);
            fadeImage.color = new Color(0, 0, 0, a);
        }
        //if (!isFadeIn) loadingCanvas.gameObject.SetActive(false);
    }
}
