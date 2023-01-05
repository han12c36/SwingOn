using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_LoadingUI : MonoBehaviour
{
    public static int stageIndex;
    public int index;
    public Image Button_Image;
    public Image Stage_Image;

    public void Awake()
    {
        Debug.Log(stageIndex);
        index = stageIndex++;
    }
    public void Button_Animation()
    {
        if (SceneController.Instance.CurSceneIndex == SceneIndex.Menu)
        {
            Button_Image.color = new Color(255.0f, 255.0f, 255.0f, 0.0f);
            GameObject stageImage = Instantiate(MenuManager.Instance.stageLoadingUI[index]);
            stageImage.transform.SetParent(MenuManager.Instance.stageImageTr.transform);
        }
    }
}
