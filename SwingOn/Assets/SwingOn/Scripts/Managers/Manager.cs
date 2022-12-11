using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    protected virtual void Awake()
    {
    }

    //�Ŵ��� ���� �������� : 
    //���̶�Űâ���� ��Ʈ�ξ��� ���ӸŴ��� �����ϰ� � �Ŵ����� �ö������� �ȵ�
    //-> ���ӸŴ����� ��ü �����ũ���� ������ ����Ʈ���� �ɾ���
    //-> � �����ũ������ ���ӸŴ����� �����ϸ�ȵ�
    //���� : ���ӸŴ��� �����ũ -> ���� �ٸ� �Ŵ����� ����

    //���̶�Űâ�̳� �Ŵ��������������� ������ �������
    protected static void InstantiateManger(bool isDonDestroy)
    {
        if (instance == null)
        {
            //���̶�Űâ���� ������ ������ ����
            //���� ���̶�Űâ�� �ִٸ� �¸� �μ��� �ִ� �ڽ��� �־�
            T managerObj = FindObjectOfType(typeof(T)) as T;

            if (!managerObj)
            {
                //���̶�Űâ�� ���ٸ� �Ŵ��� ������������ �ִ� ���� ���� �ڽ��� �־���
                T prefab = Resources.Load("ManagerPrefabs/" + typeof(T).Name) as T;
                if (prefab)
                {
                    managerObj = Instantiate(prefab);
                    managerObj.name = prefab.name.Replace("(Clone)", string.Empty);
                }
                else
                {
                    //���̶�Ű���� ���� �������������� ����
                    //�׷� �׳� �ƿ� ���ο������ �����
                    GameObject newManager = new GameObject();
                    managerObj = newManager.AddComponent<T>();
                    if(managerObj == null)
                    {
                        //��ũ��Ʈ������? �׸� �ƹ��͵����µ� ��ĳ�����?
                        Debug.LogError("Invalid format of request.");
                    }
                }
            }
            instance = managerObj.GetComponent<T>();

            if (isDonDestroy) RegistrationTo_DontDestroyManagerBox(managerObj);
            else RegistrationTo_CanDestroyManagerBox(managerObj);
        }
        //if (instance != this)
        //{
            //�ִµ� �� ��������� �ı�����(������ ���⼭ �Ƹ� �����ũ,�ο��̺�,�𽺿��̺� �� ȣ��Ǽ� ȥ����������)
        //    Destroy(gameObject);
        //}
    }

    private static void RegistrationTo_CanDestroyManagerBox(T manager)
    {
        manager.transform.SetParent(GetManagerBox("CanDestroy_ManagerBox").transform);
    }
    private static void RegistrationTo_DontDestroyManagerBox(T manager)
    {
        manager.transform.SetParent(GetManagerBox("DontDestroy_ManagerBox").transform);
    }
    private static GameObject GetManagerBox(string BoxName)
    {
        GameObject boxObj = GameObject.Find(BoxName);
        if (boxObj == null)
        {
            boxObj = new GameObject();
            boxObj.name = BoxName;
        }
        return boxObj;
    }

    //protected void InstantiateManger(bool isDontDestroy)
    //{
	//	if (instance == null)
	//	{
	//		T managerObj = FindObjectOfType(typeof(T)) as T;
	//		if (!managerObj)
	//		{
	//			T prefab = Resources.Load("ManagerPrefabs/" + typeof(T).Name) as T;
	//			if (prefab)
	//			{
	//				managerObj = Instantiate(prefab);
	//				managerObj.name = prefab.name.Replace("(Clone)", string.Empty);
	//			}
	//		}
	//		instance = managerObj.GetComponent<T>();
    //
	//		GameObject boxObj = FindManagerBoxes(isDontDestroy);
	//		managerObj.transform.SetParent(boxObj.transform);
	//	}
	//	else if (instance != this) Destroy(gameObject);
	//}

    //public static GameObject FindManagerBoxes(bool isDontDestroy)
	//{
	//	GameObject boxObj = null;
    //
	//	if (isDontDestroy)
	//	{
	//		boxObj = CheckGameObjectExist("ManagerBox_DontDestroy");
	//		DontDestroyOnLoad(boxObj);
	//	}
	//	else
	//	{
	//		boxObj = CheckGameObjectExist("ManagerBox_Destory");
	//	}
    //
	//	return boxObj;
	//}
	//public static GameObject CheckGameObjectExist(string name)
	//{
	//	GameObject temp = GameObject.Find(name);
    //
	//	if (temp == null)
	//	{
	//		temp = new GameObject();
	//		temp.name = name;
	//	}
    //
	//	return temp;
	//}
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
//���ӸŴ����� ���ؼ� �� �ʿ� ���� �Ŵ����� ���鲫�� ����� ��� �����ũ����
//�� ��ȯ�� �μ� �Ŵ������� �μ��� ���ϴ� �Ŵ������� �ڽ��� ��Ƽ� �����ϱ�

