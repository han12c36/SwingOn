using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTitleManager : Manager<MainTitleManager>
{
    private void Awake()
    {
        Debug.Log("����Ÿ��Ʋ �Ŵ��� �����ũ");
    }
    private void OnEnable()
    {
        Debug.Log("����Ÿ��Ʋ �Ŵ��� �¿��̺�");
    }
    private void Start()
    {
        Debug.Log("����Ÿ��Ʋ �Ŵ��� ��ŸƮ");
    }

    private void Update()
    {
    }
    private void OnDisable()
    {
        Debug.Log("����Ÿ��Ʋ �Ŵ��� �𽺿��̺�");
    }
}
