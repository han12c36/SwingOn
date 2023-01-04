using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_3Animation : MonoBehaviour
{
    private Animator aniCtrl;
    public ButtonCoolTime button;

    void Start()
    {
        aniCtrl = GetComponent<Animator>();
    }

    public void Skill_3Bounce() { aniCtrl.SetTrigger("Bounce"); }
}
