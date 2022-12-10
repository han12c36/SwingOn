using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneIndex
{
    Intro,
    MainTitle,
    InGame,
    End
}

public class SceneController : MonoBehaviour
{
    //�̱��� ��©�� �̰�

    [SerializeField]
    private SceneIndex curScene;

    public SceneIndex CurSceneIndex { get { return curScene; }}

    public void LoadScene(int sceneindex)
    {
        if ((int)curScene == sceneindex) return;
        if(sceneindex == (int)SceneIndex.Intro)
        {
            //��Ʈ�ξ����� �ٲ�޶�� �ϸ� ������ ���� Ÿ��Ʋ ������ ��������.
            sceneindex = (int)SceneIndex.MainTitle;
        }
        StartCoroutine(LoadSceneProcess(sceneindex));
    }

    private IEnumerator LoadSceneProcess(int sceneindex)
    {
        //�񵿱�� ���� �ҷ��ý� �ڷ�ƾ�� �̿��Ͽ� �����ؾ���(����� �� �ҷ����� ���� �����ִٳ�)->�ð����� �˾ƺ���
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneindex);         
        if (operation == null) Debug.LogError("It's a scene that doesn't exist");
        operation.allowSceneActivation = false;                                        

        float timer = 0f;
        while (!operation.isDone)
        {
            Debug.Log(operation.progress);
            if (operation.progress >= 0.9f)
            {
                timer += Time.unscaledDeltaTime;
                if (timer > 1f)
                {
                    curScene = (SceneIndex)sceneindex;
                    operation.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
