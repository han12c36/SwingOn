using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rootFade : MonoBehaviour
{
    public Text text;
    private bool isStop;

    void Start()
    {
        StartCoroutine(RootFade());
    }
    private void OnDisable()
    {
        isStop = false;
    }
    private void Update()
    {
        if (Input.anyKeyDown) isStop = true;
    }

    IEnumerator RootFade()
    {
        float timer = 0;
        float a;
        while (true)
        {
            yield return null;
            timer += Time.unscaledDeltaTime * 0.5f;
            if (timer > 2.0f) timer = 0.0f;
            else
            {
                if(timer <= 1.0f)
                {
                    float a1 = Mathf.Lerp(1f, 0f, timer);
                    text.color = new Color(255, 255, 255, a1);
                }
                else
                {
                    float a2 = Mathf.Lerp(0f, 1f, timer); 
                    text.color = new Color(255, 255, 255, a2);
                }
            }
            if (isStop) break;
        }
    }
}
