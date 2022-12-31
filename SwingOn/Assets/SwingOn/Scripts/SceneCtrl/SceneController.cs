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
        //Debug.Log("���� �� �ε� ���� �� : " + SceneManager.GetActiveScene().name);
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
        //scene -> ���� �ҷ� ���� ��
        if (SceneManager.GetActiveScene().buildIndex == (int)curScene)
        {
            Debug.Log("�·ε�� �Լ� ȣ��");
            Debug.Log(SceneManager.GetActiveScene().name);
            StartCoroutine(Fade(false));
            SceneManager.sceneLoaded -= OnLoadScene;
            //GameManager.Instance.InstantiateManagerForNextScene(scene,mode);
        }
    }

    private IEnumerator LoadSceneProcess(int sceneindex)
    {
        Debug.Log("�ε�����..��");
        yield return StartCoroutine(Fade(true));
        // �ڷ�ƾ�ȿ��� �ڷ�ƾ �����Ű�鼭 yield return���� �����Ű�� ȣ��� �ڷ�ƾ�� ���������� ��ٸ��� ����� ����. �� Fade timer�ð��� 1�ʸ�ŭ ��ٸ�
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneindex);
        //if (operation == null) Debug.LogError("It's a scene that doesn't exist");
        Debug.Log(SceneManager.GetActiveScene().name);
        operation.allowSceneActivation = false; //90�۱��� �ε��ϰ� ����ϰڴ�.

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
        //    yield return null;      //�ڷ�ƾ�� ������Ʈ�� ���ÿ� ������ ��0���Ŵ���ν� �񵿱� ȿ���� �ְڴ�.
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

    //���̵� �� : ���� ���� ��
    //���̵� �ƿ� : ���� ������� ��

    IEnumerator Fade(bool isFadeIn) // �ε��������� ���̵���/�ƿ� ȿ��
    {
        //if (loadingCanvas == null) yield return null;
        if (!isFadeIn)
        {
            //progressBar.value = 0.0f;
            loadingCanvas.gameObject.SetActive(false);
            Debug.Log("�ε�â ����");
        }
        else
        {
            Debug.Log("�ε�â �ѱ�");
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
