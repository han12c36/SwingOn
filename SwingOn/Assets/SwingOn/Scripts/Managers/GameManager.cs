using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Manager<GameManager>
{

    private SceneController sceneCtrl;

    public SceneController SceneCtrl { get { return sceneCtrl; } }
    private void Awake()
    {
        InstantiateManger(true);
        Debug.Log("게임 매니저 어웨이크");
        //DontDestroyOnLoad(this.gameObject);
        sceneCtrl = GetComponent<SceneController>();
    }
    
    private void OnEnable()
    {
        Debug.Log("게임 매니저 인에이블");
    }
    [RuntimeInitializeOnLoadMethod]
    private void InstantiateGameManger()
    {
        InstantiateManger(true);
        //DontDestroyOnLoad(this.gameObject);
        sceneCtrl = GetComponent<SceneController>();
    }

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
}