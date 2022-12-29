using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum SceneIndex
{
    Intro,
    MainTitle,
    Menu,
    InGame,
    End
}

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private SceneIndex curScene;

    public SceneIndex CurSceneIndex { get { return curScene; }}

    public CanvasGroup loadingCanvas;

    public void LoadScene(int sceneindex)
    {
        if ((int)curScene == sceneindex) return;
        //Debug.Log("다음 씬 로드 전에 씬 : " + SceneManager.GetActiveScene().name);
        SceneManager.sceneLoaded += OnLoadScene;
        StartCoroutine(LoadSceneProcess(sceneindex));
    }

    private void OnLoadScene(Scene scene,LoadSceneMode mode)
    {
        //scene -> 이제 불러 와질 씬
        SceneManager.sceneLoaded -= OnLoadScene;    //체인묶는 걸 1회성으로 두기 위해서 넣고 바로 뺴주네
        GameManager.Instance.InstantiateManagerForNextScene(scene.buildIndex);
        StartCoroutine(Fade(false));
    }

    private IEnumerator LoadSceneProcess(int sceneindex)
    {
        yield return StartCoroutine(Fade(true)); 
        // 코루틴안에서 코루틴 실행시키면서 yield return으로 실행시키면 호출된 코루틴이 끝날때까지 기다리게 만들수 있음. 즉 Fade timer시간인 1초만큼 기다림

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
    //페이드 인 : 점점 들어나는 것
    //페이드 아웃 : 점점 사라지는 것

    IEnumerator Fade(bool isFadeIn) // 로딩끝났을때 페이드인/아웃 효과
    {
        if (loadingCanvas == null) yield return null;
        float timer = 0;
        while (timer <= 1f)
        {
            yield return null;
            timer += Time.unscaledDeltaTime * 4f;
            loadingCanvas.alpha = isFadeIn ? Mathf.Lerp(0f, 1f, timer) : Mathf.Lerp(1f, 0f, timer);
        }
        //if (!isFadeIn) loadingCanvas.gameObject.SetActive(false);
    }
}
