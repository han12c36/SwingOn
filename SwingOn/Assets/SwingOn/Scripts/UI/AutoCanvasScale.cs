using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoCanvasScale : MonoBehaviour
{
    CanvasScaler canvasScaler;
    
    private void Awake()
    {
        canvasScaler = GetComponent<CanvasScaler>();
        //Default �ػ� ����
        float fixedAspectRatio = 16f / 9f;

        //���� �ػ��� ����
        float currentAspectRatio = (float)Screen.width / (float)Screen.height;

        //���� �ػ� ���� ������ �� �� ���
        if (currentAspectRatio > fixedAspectRatio) canvasScaler.matchWidthOrHeight = 0;
        //���� �ػ��� ���� ������ �� �� ���
        else if (currentAspectRatio < fixedAspectRatio) canvasScaler.matchWidthOrHeight = 1;
    }
}
