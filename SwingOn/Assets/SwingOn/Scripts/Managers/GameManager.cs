using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Manager<GameManager>
{
    //게임매니저는 생성될떄 등록된 돈디스트로이 매니저를 모두 생성하게 한다...?
    //씬 전환이 일어나면 해당 매니저의 디스에이블이 발동한다
    //꺼졋다 다시 켜지는 매니저는 저장된 정보다 다 날라감

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
    // 해당씬에 필요한 매니저들
    // 씬 넘어갈떄마다 잘 삭제되는지 확인하자
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