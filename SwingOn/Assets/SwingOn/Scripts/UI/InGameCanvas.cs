using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCanvas : MonoBehaviour
{
    public GameObject ScorePanel;
    public int playerCurHp;


    private void Awake()
    {
        if (ScorePanel.activeSelf) ScorePanel.gameObject.SetActive(false);
    }
    private void Start()
    {
    }
    private void Update()
    {
        playerCurHp = Player.instance.status.curHp;
        if(Player.instance.status.curHp <= 0)
        {
            if(!ScorePanel.activeSelf) ScorePanel.gameObject.SetActive(true);
        }
    }
}
