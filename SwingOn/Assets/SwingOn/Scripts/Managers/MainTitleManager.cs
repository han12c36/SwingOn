using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTitleManager : Manager<MainTitleManager>
{
    private void Awake()
    {
        Debug.Log("메인타이틀 매니저 어웨이크");
    }
    private void OnEnable()
    {
        Debug.Log("메인타이틀 매니저 온에이블");
    }
    private void Start()
    {
        Debug.Log("메인타이틀 매니저 스타트");
    }

    private void Update()
    {
    }
    private void OnDisable()
    {
        Debug.Log("메인타이틀 매니저 디스에이블");
    }
}
