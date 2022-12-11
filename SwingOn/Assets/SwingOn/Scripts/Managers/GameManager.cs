using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Manager<GameManager>
{
    //���ӸŴ����� �����ɋ� ��ϵ� ����Ʈ���� �Ŵ����� ��� �����ϰ� �Ѵ�...?
    private SceneController sceneCtrl;

    public SceneController SceneCtrl { get { return sceneCtrl; } }

    protected override void Awake()
    {
        Debug.Log("���� �Ŵ��� �����ũ");
        InstantiateManger(true);
        sceneCtrl = GetComponent<SceneController>();
    }

    private void OnEnable()
    {
        Debug.Log("���� �Ŵ��� �ο��̺�");
    }
    //[RuntimeInitializeOnLoadMethod]
    

    private void OnDisable()
    {
        Debug.Log("���� �Ŵ��� �� ���̺�");
    }
    private void Start()
    {
        Debug.Log("���� �Ŵ��� ��ŸƮ");
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
    // �ش���� �ʿ��� �Ŵ�����
    // �� �Ѿ������ �� �����Ǵ��� Ȯ������
    private void InstantiateManagerForIntroScene()
    {
        Debug.Log("��Ʈ�ξ��� �ʿ��� �Ŵ��� ����");
        InstantiateManger(true);
    }
    private void InstantiateManagerForMainTitleScene()
    {
        Debug.Log("���� Ÿ��Ʋ���� �ʿ��� �Ŵ��� ����");
        MainTitleManager.InstantiateManger(false);
    }
    private void InstantiateManagerForInGameScene()
    {
        Debug.Log("�ΰ��Ӿ��� �ʿ��� �Ŵ��� ����");
        InGameManager.InstantiateManger(false);
    }
    //=====================================================================================
}