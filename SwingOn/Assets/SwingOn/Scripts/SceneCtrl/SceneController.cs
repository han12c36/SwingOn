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
        //Debug.Log("���� �� �ε� ���� �� : " + SceneManager.GetActiveScene().name);
        SceneManager.sceneLoaded += OnLoadScene;
        StartCoroutine(LoadSceneProcess(sceneindex));
    }

    private void OnLoadScene(Scene scene,LoadSceneMode mode)
    {
        //scene -> ���� �ҷ� ���� ��
        SceneManager.sceneLoaded -= OnLoadScene;    //ü�ι��� �� 1ȸ������ �α� ���ؼ� �ְ� �ٷ� ���ֳ�
        GameManager.Instance.InstantiateManagerForNextScene(scene.buildIndex);
        StartCoroutine(Fade(false));
    }

    private IEnumerator LoadSceneProcess(int sceneindex)
    {
        yield return StartCoroutine(Fade(true)); 
        // �ڷ�ƾ�ȿ��� �ڷ�ƾ �����Ű�鼭 yield return���� �����Ű�� ȣ��� �ڷ�ƾ�� ���������� ��ٸ��� ����� ����. �� Fade timer�ð��� 1�ʸ�ŭ ��ٸ�

        //����� ���� �ҷ����� �ش� ���� ��� �� �ε� �ɋ����� ����
        //�񵿱�� ���� �ҷ����� �ٸ��� �۾��ϴ� �ٵǸ� �Ѿ
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
    //���̵� �� : ���� ���� ��
    //���̵� �ƿ� : ���� ������� ��

    IEnumerator Fade(bool isFadeIn) // �ε��������� ���̵���/�ƿ� ȿ��
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
