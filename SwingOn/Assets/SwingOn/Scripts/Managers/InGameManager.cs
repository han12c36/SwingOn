using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : Manager<InGameManager>
{
    private Vector3 playerStartPos = Vector3.zero;

    private void Awake()
    {
        Player.instance.transform.position = playerStartPos;
        Debug.Log("인게임 매니저 어웨이크");
    }
    private void OnEnable()
    {
        Debug.Log("인게임 매니저 온에이블");
    }
    private void Start()
    {
        Debug.Log("인게임 매니저 스타트");
    }

    private void Update()
    {
    }
    private void OnDisable()
    {
        Debug.Log("인게임 매니저 디스에이블");
    }
}
