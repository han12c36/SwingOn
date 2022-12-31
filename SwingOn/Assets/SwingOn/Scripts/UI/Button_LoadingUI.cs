using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_LoadingUI : MonoBehaviour
{
    public Image Button_Image;
    public Image Stage_Image;
    public void Button_Animation()
    {
        if (SceneController.Instance.CurSceneIndex == SceneIndex.Menu)
        {
            Button_Image.color = new Color(255.0f, 255.0f, 255.0f, 0.0f);
            Stage_Image.gameObject.SetActive(true);
        }
    }
}
