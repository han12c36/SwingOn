using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Manager<UIManager>
{
    public GameObject scorePanel;

    private void Awake()
    {
    }
    private void Start()
    {
    }
    void Update()
    {
        if(Player.instance.status.curHp <= 0)
        {
            scorePanel.SetActive(true);
        }
    }
}
