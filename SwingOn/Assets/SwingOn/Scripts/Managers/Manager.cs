using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Manager<T> : MonoBehaviour where T :  MonoBehaviour
{
    private static T instance;

	public static T Instance
    {
        get 
        {
			if (!instance)
			{
				instance = FindObjectOfType(typeof(T)) as T;
				if (instance == null) Debug.Log("no Singleton obj");
			}
			return instance;
			//if (instance == null) instance = InstantiateManager(false);
            //return null;
        }
    }

    //매니저 생성 전제조건 : 
    //하이라키창에는 인트로씬의 게임매니저 제외하곤 어떤 매니저도 올라가있으면 안됨
    //-> 게임매니저만 자체 어웨이크에서 본인을 돈디스트로이 걸어줌
    //-> 어떤 어웨이크에서도 게임매니저를 참조하면안됨
    //순서 : 게임매니저 어웨이크 -> 이후 다른 매니저들 생성

    //하이라키창이나 매니저프리팹폴더에 없으면 못만들어
    protected static T InstantiateManger(bool isDonDestroy)
    {
        //하이라키 창에 있으면 그냥 반환
        if (instance == null)
        {
            //하이라키창에서 본인의 유무를 검출
            //만약 하이라키창에 있다면 걔를 부술수 있는 박스에 넣어
            string managerName = typeof(T).Name;
            //T managerObj = FindObjectOfType(typeof(T)) as T;
            GameObject managerObj = GameObject.Find(managerName);

            if (!managerObj)
            {
                //하이라키창에 없다면 매니저 프리팹폴더에 있는 놈을 만들어서 박스에 넣어줘
                GameObject prefab = Resources.Load("ManagerPrefabs/" + typeof(T).Name) as GameObject;

                if (prefab != null)
                {
                    managerObj = Instantiate(prefab);
                    managerObj.name = prefab.name.Replace("(Clone)", string.Empty);
                }
                else
                {
                    Debug.LogError(typeof(T).Name + "Invalid format of request.");
                }
            }
            
            T newInstance = managerObj.GetComponent<T>();

            if (isDonDestroy) RegistrationTo_DontDestroyManagerBox(newInstance);
            else RegistrationTo_CanDestroyManagerBox(newInstance);

            return newInstance;
        }
        return Instance;
    }

    private static void RegistrationTo_CanDestroyManagerBox(T manager)
    {
        manager.transform.SetParent(GetManagerBox("CanDestroy_ManagerBox",false).transform);
    }
    private static void RegistrationTo_DontDestroyManagerBox(T manager)
    {
        manager.transform.SetParent(GetManagerBox("DontDestroy_ManagerBox",true).transform);
    }
    private static GameObject GetManagerBox(string BoxName,bool isDontDestroy)
    {
        GameObject boxObj = GameObject.Find(BoxName);
        if (boxObj == null)
        {
            boxObj = new GameObject();
            boxObj.name = BoxName;
        }
        if (isDontDestroy) DontDestroyOnLoad(boxObj);
        return boxObj;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public virtual void OnEnable()
    {
        SceneManager.sceneLoaded += InstantiateManagerForNextScene;
    }

    public virtual void OnDisable()
    {
        SceneManager.sceneLoaded -= InstantiateManagerForNextScene;
    }

    public virtual void InstantiateManagerForNextScene(Scene scene, LoadSceneMode mode) { }
}
//게임매니저를 통해서 각 필요 씬에 매니저를 만들껀데 만드는 즉시 어웨이크에서
//씬 전환시 부술 매니저인지 부수지 못하는 매니저인지 박스에 담아서 관리하기

