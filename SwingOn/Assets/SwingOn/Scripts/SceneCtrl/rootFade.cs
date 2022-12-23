using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rootFade : MonoBehaviour
{
    public Text text;
    public float a;
    public bool isStop;
    public bool isDown;
    public bool isUp;

    void Start()
    {
        StartCoroutine(RootFade());
        a = text.color.a;
        Debug.Log(text.color.r+ text.color.g+ text.color.b+ text.color.a);
    }
    private void OnDisable()
    {
        isStop = false;
    }
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            isStop = true;
            StopCoroutine(RootFade());
        }
    }

    IEnumerator RootFade()
    {
        Debug.Log(text.color.a);
        float timer = 0;
        while (true)
        {
            yield return null;
            if (text.color.a == 1.0f)
            {
                timer = 0.0f;
                isDown = true;
                isUp = false;
            }
            else if (text.color.a == 0.0f)
            {
                timer = 0.0f;
                isDown = false;
                isUp = true;
            }
            timer += Time.unscaledDeltaTime * 0.65f;

            if (isDown) a = Mathf.Lerp(1f, 0f, timer);
            else if (isUp) a = Mathf.Lerp(0f, 1f, timer);

            text.color = new Color(0, 0, 0, a);
            if (isStop) break;
        }
    }
}
