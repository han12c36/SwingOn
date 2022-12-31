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
    Loading,
    End
}

public class SceneController : Manager<SceneController>
{
    [SerializeField]
    private SceneIndex curScene;

    public SceneIndex CurSceneIndex { get { return curScene; } set { curScene = value; } }

    public CanvasGroup loadingCanvas;
    public Slider progressBar;

    private void Awake()
    {
        if (InstantiateManger(true) != this) Destroy(this);
    }

    public void Update()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (CurSceneIndex == SceneIndex.Intro)
            {
                LoadScene((int)SceneIndex.MainTitle);
            }
        }
        else if (Input.anyKeyDown)
        {
            if (CurSceneIndex == SceneIndex.MainTitle)
            {
                LoadScene((int)SceneIndex.Menu);
            }
        }
    }

    public void LoadScene(int sceneindex)
    {
        if ((int)curScene == sceneindex) return;
        //Debug.Log("다음 씬 로드 전에 씬 : " + SceneManager.GetActiveScene().name);
        //if (sceneindex == (int)SceneIndex.InGame) loadingCanvas.gameObject.SetActive(true);
        if ((int)curScene == (int)SceneIndex.Menu)
        {
            progressBar.value = 0.0f;
            loadingCanvas.gameObject.SetActive(true);
        }
        SceneManager.sceneLoaded += OnLoadScene;
        StartCoroutine(LoadSceneProcess(sceneindex));
    }
    private void OnLoadScene(Scene scene, LoadSceneMode mode)
    {
        //scene -> 이제 불러 와질 씬
        if (SceneManager.GetActiveScene().buildIndex == (int)curScene)
        {
            Debug.Log("온로드씬 함수 호출");
            Debug.Log(SceneManager.GetActiveScene().name);
            StartCoroutine(Fade(false));
            SceneManager.sceneLoaded -= OnLoadScene;
            //GameManager.Instance.InstantiateManagerForNextScene(scene,mode);
        }
    }

    private IEnumerator LoadSceneProcess(int sceneindex)
    {
        Debug.Log("로딩절차..중");
        yield return StartCoroutine(Fade(true));
        // 코루틴안에서 코루틴 실행시키면서 yield return으로 실행시키면 호출된 코루틴이 끝날때까지 기다리게 만들수 있음. 즉 Fade timer시간인 1초만큼 기다림
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneindex);
        //if (operation == null) Debug.LogError("It's a scene that doesn't exist");
        Debug.Log(SceneManager.GetActiveScene().name);
        operation.allowSceneActivation = false; //90퍼까지 로드하고 대기하겠다.

        float timer = 0f;
        while (!operation.isDone)
        {
            yield return null;
            if ((int)curScene == (int)SceneIndex.Menu)
            {
                timer += Time.deltaTime;
                if (operation.progress < 0.9f)
                {
                    progressBar.value = Mathf.Lerp(progressBar.value, operation.progress, timer * 0.1f);
                    if (progressBar.value >= operation.progress) timer = 0f;
                }
                else
                {
                    progressBar.value = Mathf.Lerp(progressBar.value, 1f, timer);
                    if (progressBar.value == 1.0f)
                    {
                        operation.allowSceneActivation = true;
                        curScene = (SceneIndex)sceneindex;
                        yield break;
                    }
                }
            }
            else
            {
                if (operation.progress >= 0.9f)
                {
                    operation.allowSceneActivation = true;
                    curScene = (SceneIndex)sceneindex;
                }
            }
        }

        //float timer = 0f;
        //while (!operation.isDone)
        //{
        //    yield return null;      //코루틴과 업데이트를 동시에 빠르게 진0행시킴으로써 비동기 효과를 주겠다.
        //    progressBar.value = operation.progress;
        //    Debug.Log(operation.progress);
        //    if(operation.progress >= 0.9f)
        //    {
        //        timer += Time.unscaledDeltaTime;
        //        progressBar.value = 1;
        //        if (timer >= 1f)
        //        {
        //            curScene = (SceneIndex)sceneindex;
        //            operation.allowSceneActivation = true;
        //            yield break;
        //        }
        //    }
        //}


    }

    //페이드 인 : 점점 들어나는 것
    //페이드 아웃 : 점점 사라지는 것

    IEnumerator Fade(bool isFadeIn) // 로딩끝났을때 페이드인/아웃 효과
    {
        //if (loadingCanvas == null) yield return null;
        if (!isFadeIn)
        {
            //progressBar.value = 0.0f;
            loadingCanvas.gameObject.SetActive(false);
            Debug.Log("로딩창 끄기");
        }
        else
        {
            Debug.Log("로딩창 켜기");
        }

        float timer = 0;
        while (timer <= 1f)
        {
            yield return null;
            timer += Time.unscaledDeltaTime * 4f;
            loadingCanvas.alpha = isFadeIn ? Mathf.Lerp(0f, 1f, timer) : Mathf.Lerp(1f, 0f, timer);
        }
        
        //yield return null;
    }

    //=====================================================================================
    //ButtonFunc

}
