using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : Manager<InGameManager>
{
    private Vector3 playerStartPos = Vector3.zero;

    private void Awake()
    {
        Player.instance.transform.position = playerStartPos;
        Debug.Log("�ΰ��� �Ŵ��� �����ũ");
    }
    private void OnEnable()
    {
        Debug.Log("�ΰ��� �Ŵ��� �¿��̺�");
    }
    private void Start()
    {
        Debug.Log("�ΰ��� �Ŵ��� ��ŸƮ");
    }

    private void Update()
    {
    }
    private void OnDisable()
    {
        Debug.Log("�ΰ��� �Ŵ��� �𽺿��̺�");
    }
}
