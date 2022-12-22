using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Manager<GameManager>
{
    //���ӸŴ����� �����ɋ� ��ϵ� ����Ʈ���� �Ŵ����� ��� �����ϰ� �Ѵ�...?
    //�� ��ȯ�� �Ͼ�� �ش� �Ŵ����� �𽺿��̺��� �ߵ��Ѵ�
    //������ �ٽ� ������ �Ŵ����� ����� ������ �� ����

    [SerializeField]
    private SceneController sceneCtrl;
    private CoroutineHelper coroutineHelper;

    public int count;

    public SceneController SceneCtrl { get { return sceneCtrl; } }
    public CoroutineHelper GetCoroutineHelper { get { return coroutineHelper; } }


    //[RuntimeInitializeOnLoadMethod]
    //private void InstantiateGameManger()
    //{
    //    if (InstantiateManger(true) != this) Destroy(this);
    //    sceneCtrl = GetComponent<SceneController>();
    //}

    private void Awake()
    {
        if (InstantiateManger(true) != this) Destroy(this);
        sceneCtrl = GetComponent<SceneController>();
        coroutineHelper = GetComponent<CoroutineHelper>();
    }

    private void OnEnable()
    {
    }
    

    private void OnDisable()
    {
    }
    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (sceneCtrl.CurSceneIndex == SceneIndex.Intro)
            {
                sceneCtrl.LoadScene((int)SceneIndex.MainTitle);
            }
            //else if (sceneCtrl.CurSceneIndex == SceneIndex.MainTitle)
            //{
            //    sceneCtrl.LoadScene((int)SceneIndex.Intro);
            //}
        }
        else if(Input.anyKeyDown)
        {
            if (sceneCtrl.CurSceneIndex == SceneIndex.MainTitle)
            {
                sceneCtrl.LoadScene((int)SceneIndex.InGame);
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            count++;
        }
    }

    public void InstantiateManagerForNextScene(int NextSceneIndex)
    {
        switch (NextSceneIndex)
        {
            case (int)SceneIndex.Intro :
                InstantiateManagerForIntroScene();
                break;
            case (int)SceneIndex.MainTitle:
                InstantiateManagerForMainTitleScene();
                break;
            case (int)SceneIndex.InGame:
                InstantiateManagerForInGameScene();
                break;
            default:
                break;
        }
    }

    //=====================================================================================
    // �ش���� �ʿ��� �Ŵ�����
    // �� �Ѿ������ �� �����Ǵ��� Ȯ������
    private void InstantiateManagerForIntroScene()
    {
        //InstantiateManger(true);
    }
    private void InstantiateManagerForMainTitleScene()
    {
        MainTitleManager.InstantiateManger(false);
    }
    private void InstantiateManagerForInGameScene()
    {
        InGameManager.InstantiateManger(false);
        CameraManager.InstantiateManger(false);
        PoolingManager.InstantiateManger(false);
    }
    //=====================================================================================

    //=====================================================================================
    //ButtonFunc
    public void Button_GoInGameScene()
    {
        if(GameManager.Instance.sceneCtrl.CurSceneIndex == SceneIndex.MainTitle)
        {
            GameManager.Instance.sceneCtrl.LoadScene((int)SceneIndex.InGame);
        }
    }
    public void Button_GoMainTitleScene()
    {
        if (GameManager.Instance.sceneCtrl.CurSceneIndex == SceneIndex.InGame)
        {
            GameManager.Instance.sceneCtrl.LoadScene((int)SceneIndex.MainTitle);
        }
    }
    //=====================================================================================
}