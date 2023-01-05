using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewCtrl : MonoBehaviour
{
    private ScrollRect scrollRect;
    public float space = 100.0f;
    public List<GameObject> stageBtn = new List<GameObject>();

    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        CreateStageBtn();
        space = 100.0f;
    }

    void Update()
    {
    }

    public void CreateStageBtn()
    {
        float x = 0.0f;
        float y = 0.0f;

        for (int i = 0; i < stageBtn.Count; i++)
        {
            var Btn = Instantiate(stageBtn[i], scrollRect.content).GetComponent<RectTransform>();
            Btn.anchoredPosition = new Vector2(x + space, y);
            x += (Btn.sizeDelta.x + space);
            Debug.Log(x);
        }
        scrollRect.content.sizeDelta = new Vector2(x + space, 800.0f);
    }
}
