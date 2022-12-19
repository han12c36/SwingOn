using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    Camera camera;

    private void Awake()
    {

        camera = GetComponent<Camera>();
        Rect rect = camera.rect;
        float scaleH = ((float)Screen.width / Screen.height) / ((float)16 / 9);//(가로 / 세로)
        float scaleW = 1.0f / scaleH;
        if(scaleH < 1)
        {
            rect.height = scaleH;
            rect.y = (1f - scaleH) / 2f;
        }
        else if(scaleH == 1) return;
        else
        {
            rect.width = scaleW;
            rect.x = (1.0f / scaleW) / 2f;
        }
        camera.rect = rect;
    }

 
}
