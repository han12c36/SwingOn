using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Manager<GameManager>
{
    //게임매니저는 생성될떄 등록된 돈디스트로이 매니저를 모두 생성하게 한다...?
    //씬 전환이 일어나면 해당 매니저의 디스에이블이 발동한다
    //꺼졋다 다시 켜지는 매니저는 저장된 정보다 다 날라감


    [SerializeField]
    private CoroutineHelper coroutineHelper;

    public int count;
    public bool isPaused;

    public CoroutineHelper GetCoroutineHelper { get { return coroutineHelper; } }

    private Structs.UserSaveDatas saveData;

    public Structs.UserSaveDatas SaveData { get { return saveData; } set { saveData = value; } }

    //[RuntimeInitializeOnLoadMethod]
    //private void InstantiateGameManger()
    //{
    //    if (InstantiateManger(true) != this) Destroy(this);
    //    sceneCtrl = GetComponent<SceneController>();
    //}

    private void Awake()
    {
        if (InstantiateManger(true) != this) Destroy(this);
        //sceneCtrl = GetComponent<SceneController>();
        coroutineHelper = GetComponent<CoroutineHelper>();
    }

    public override void OnEnable()
    {
        base.OnEnable();
    }


    public override void OnDisable()
    {
        base.OnDisable();
    }
    private void Start()
    {
    }

    private void Update()
    {
    }

    public void HitStop(float fps)
    {
        if(!isPaused)
        {
            isPaused = true;
            StartCoroutine(TimeStop(fps));
        }
    }
    IEnumerator TimeStop(float fps)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(Time.deltaTime * fps);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public override void InstantiateManagerForNextScene(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        switch (scene.buildIndex)
        {
            case (int)SceneIndex.Intro :
                InstantiateManagerForIntroScene();
                break;
            case (int)SceneIndex.MainTitle:
                InstantiateManagerForMainTitleScene();
                break;
            case (int)SceneIndex.Menu:
                InstantiateManagerForMenuScene();
                break;
            case (int)SceneIndex.InGame:
                InstantiateManagerForInGameScene();
                break;
            case (int)SceneIndex.Loading:
                InstantiateManagerForLoadingScene();
                break;
            default:
                break;
        }
    }

    //=====================================================================================
    // 해당씬에 필요한 매니저들
    // 씬 넘어갈떄마다 잘 삭제되는지 확인하자
    private void InstantiateManagerForLoadingScene()
    {
    }
    private void InstantiateManagerForIntroScene()
    {
        //InstantiateManger(true);
    }
    private void InstantiateManagerForMainTitleScene()
    {
        MainTitleManager.InstantiateManger(false);
    }
    private void InstantiateManagerForMenuScene()
    {
        MenuManager.InstantiateManger(false);
    }
    private void InstantiateManagerForInGameScene()
    {
        InGameManager.InstantiateManger(false);
        CameraManager.InstantiateManger(false);
        PoolingManager.InstantiateManger(false);
        UIManager.InstantiateManger(false);
    }
    //=====================================================================================
    public void Button_GoLoadingScene()
    {
        if (SceneController.Instance.CurSceneIndex == SceneIndex.Menu)
        {
            SceneController.Instance.LoadScene((int)SceneIndex.Loading);
        }
    }
    //public void Button_GoInGameScene()
    //{
    //    if (SceneController.Instance.CurSceneIndex == SceneIndex.Menu)
    //    {
    //
    //        SceneController.Instance.LoadScene((int)SceneIndex.InGame);
    //    }
    //}
    public void Button_GoMainTitleScene()
    {
        if (SceneController.Instance.CurSceneIndex == SceneIndex.InGame)
        {
            SceneController.Instance.LoadScene((int)SceneIndex.Menu);
        }
    }

    public void Button_Quit()
    {
        Application.Quit();
        PlayerPrefs.SetFloat("BestLifeTime", saveData.bestLifeTime);
        PlayerPrefs.SetFloat("BestDamage", saveData.bestDamage);
        PlayerPrefs.SetInt("PlaybleStageIndex", saveData.playableStageIndex);
    }

    void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            isPaused = true;
            Time.timeScale = 0.0f;
        }
        else
        {
            if (isPaused)
            {
                isPaused = false;
                Time.timeScale = 1.0f;
            }
        }
    }
    //=====================================================================================
}