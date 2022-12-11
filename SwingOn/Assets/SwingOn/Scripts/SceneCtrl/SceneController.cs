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
        //if(sceneindex == (int)SceneIndex.Intro)
        //{
        //    //인트로씬으로 바꿔달라고 하면 강제로 메인 타이틀 씬으로 변경하자.
        //    sceneindex = (int)SceneIndex.MainTitle;
        //}
        Debug.Log("다음 씬 로드 전에 씬 : " + SceneManager.GetActiveScene().name);
        SceneManager.sceneLoaded += OnLoadScene;
        StartCoroutine(LoadSceneProcess(sceneindex));
    }

    private void OnLoadScene(Scene scene,LoadSceneMode mode)
    {
        //scene -> 이제 불러 와질 씬
        SceneManager.sceneLoaded -= OnLoadScene;    //체인묶는 걸 1회성으로 두기 위해서 넣고 바로 뺴주네
        GameManager.Instance.InstantiateManagerForNextScene(scene.buildIndex);
    }

    private IEnumerator LoadSceneProcess(int sceneindex)
    {
        //동기로 씬을 불러오면 해당 씬이 모두 다 로드 될떄까지 멈춤
        //비동기로 씬을 불러오면 다른거 작업하다 다되면 넘어감
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneindex);         
        if (operation == null) Debug.LogError("It's a scene that doesn't exist");
        operation.allowSceneActivation = false;                                        

        float timer = 0f;
        while (!operation.isDone)
        {
            //Debug.Log(operation.progress);
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
