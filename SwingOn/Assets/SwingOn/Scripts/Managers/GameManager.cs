using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Manager<GameManager>
{
    //게임매니저는 생성될떄 등록된 돈디스트로이 매니저를 모두 생성하게 한다...?
    private SceneController sceneCtrl;

    public SceneController SceneCtrl { get { return sceneCtrl; } }

    protected override void Awake()
    {
        Debug.Log("게임 매니저 어웨이크");
        InstantiateManger(true);
        sceneCtrl = GetComponent<SceneController>();
    }

    private void OnEnable()
    {
        Debug.Log("게임 매니저 인에이블");
    }
    //[RuntimeInitializeOnLoadMethod]
    

    private void OnDisable()
    {
        Debug.Log("게임 매니저 디스 에이블");
    }
    private void Start()
    {
        Debug.Log("게임 매니저 스타트");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            if(sceneCtrl.CurSceneIndex == SceneIndex.Intro)
            {
                sceneCtrl.LoadScene((int)SceneIndex.MainTitle);
            }
            else if (sceneCtrl.CurSceneIndex == SceneIndex.MainTitle)
            {
                sceneCtrl.LoadScene((int)SceneIndex.InGame);
            }
            else if (sceneCtrl.CurSceneIndex == SceneIndex.InGame)
            {
                sceneCtrl.LoadScene((int)SceneIndex.Intro);
            }
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
        Debug.Log("인트로씬에 필요한 매니저 생성");
        InstantiateManger(true);
    }
    private void InstantiateManagerForMainTitleScene()
    {
        Debug.Log("메인 타이틀씬에 필요한 매니저 생성");
        MainTitleManager.InstantiateManger(false);
    }
    private void InstantiateManagerForInGameScene()
    {
        Debug.Log("인게임씬에 필요한 매니저 생성");
        InGameManager.InstantiateManger(false);
    }
    //=====================================================================================
}