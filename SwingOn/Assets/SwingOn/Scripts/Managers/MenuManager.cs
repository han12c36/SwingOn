using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : Manager<MenuManager>
{
    public Text bestTime;
    public Text bestDamage;

    void Start()
    {
        bestTime = GameObject.Find("").GetComponent<Text>();
        bestDamage = GameObject.Find("").GetComponent<Text>();
    }

    void Update()
    {
        
    }
}
