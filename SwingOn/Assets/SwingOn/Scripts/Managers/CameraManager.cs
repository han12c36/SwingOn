using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Manager<CameraManager>
{
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private Transform offset;
    [SerializeField]
    private Camera camera;

    public GameObject Target {  get{ return target; } set{ target = value; } } 
    private void Awake()
    {
        if (GameManager.Instance.SceneCtrl.CurSceneIndex == SceneIndex.InGame)
        {
            //target = FindObjectOfType(typeof(Player)) as GameObject;
            target = GameObject.Find("Player");
            if (target == null) Debug.LogError("Target does not exist.");
        }
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        
    }
}
