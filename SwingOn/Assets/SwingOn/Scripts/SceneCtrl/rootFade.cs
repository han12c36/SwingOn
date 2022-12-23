using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rootFade : MonoBehaviour
{
    public Text text;
    private bool isStop;
    bool isDown;
    bool isUp;
    float a;

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

        while (true)
        {
            yield return null;
            timer += Time.unscaledDeltaTime * 1.5f;
            if (timer % 2 == 0) isUp = true;

            if (text.color.a == 255.0f)
            {
                //이제 내려가야함 
                isDown = true;
                isUp = false;
                timer = 0.0f;
            }
            else if (text.color.a == 0.0f)
            {
                //이제 올라가야함
                isUp = true;
                isDown = false;
                timer = 0.0f;
            }

            if (isDown) a = Mathf.Lerp(1f, 0f, timer);
            else if(isUp) a = Mathf.Lerp(0f, 1f, timer);
            text.color = new Color(255, 255, 255, a);

            if (isStop) break;
        }
    }
}
