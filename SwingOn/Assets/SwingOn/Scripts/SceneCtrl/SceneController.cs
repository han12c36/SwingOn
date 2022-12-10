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
    //싱글톤 우짤까 이거

    [SerializeField]
    private SceneIndex curScene;

    public SceneIndex CurSceneIndex { get { return curScene; }}

    public void LoadScene(int sceneindex)
    {
        if ((int)curScene == sceneindex) return;
        if(sceneindex == (int)SceneIndex.Intro)
        {
            //인트로씬으로 바꿔달라고 하면 강제로 메인 타이틀 씬으로 변경하자.
            sceneindex = (int)SceneIndex.MainTitle;
        }
        StartCoroutine(LoadSceneProcess(sceneindex));
    }

    private IEnumerator LoadSceneProcess(int sceneindex)
    {
        //비동기로 씬을 불러올시 코루틴을 이용하여 진행해야함(동기로 씬 불러오면 정지 현상있다네)->시간날떄 알아보자
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
