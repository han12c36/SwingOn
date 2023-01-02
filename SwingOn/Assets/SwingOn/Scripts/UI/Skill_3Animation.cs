using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_3Animation : MonoBehaviour
{
    public Player player;
    private Animator aniCtrl;

    void Start()
    {
        player = InGameManager.Instance.GetPlayer;
        aniCtrl = GetComponent<Animator>();
    }

    public void Skill_3Bounce() { aniCtrl.SetTrigger("Bounce"); }
    void Update()
    {
        
    }
}
