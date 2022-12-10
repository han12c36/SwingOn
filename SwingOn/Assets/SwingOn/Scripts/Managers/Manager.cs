using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager<T> : MonoBehaviour where T :  MonoBehaviour
{
    private static T instance;

	public T Instance
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
	protected void InstantiateManger(bool isDontDestroy)
    {
		if (instance == null)
		{
			T managerObj = FindObjectOfType(typeof(T)) as T;
			if (!managerObj)
			{
				T prefab = Resources.Load("ManagerPrefabs/" + typeof(T).Name) as T;
				if (prefab)
				{
					managerObj = Instantiate(prefab);
					managerObj.name = prefab.name.Replace("(Clone)", string.Empty);
				}
			}
			instance = managerObj.GetComponent<T>();

			GameObject boxObj = FindManagerBoxes(isDontDestroy);
			managerObj.transform.SetParent(boxObj.transform);
		}
		else if (instance != this) Destroy(gameObject);
	}
	public static GameObject FindManagerBoxes(bool isDontDestroy)
	{
		GameObject boxObj = null;

		if (isDontDestroy)
		{
			boxObj = CheckGameObjectExist("ManagerBox_DontDestroy");
			DontDestroyOnLoad(boxObj);
		}
		else
		{
			boxObj = CheckGameObjectExist("ManagerBox_Destory");
		}

		return boxObj;
	}
	public static GameObject CheckGameObjectExist(string name)
	{
		GameObject temp = GameObject.Find(name);

		if (temp == null)
		{
			temp = new GameObject();
			temp.name = name;
		}

		return temp;
	}
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
