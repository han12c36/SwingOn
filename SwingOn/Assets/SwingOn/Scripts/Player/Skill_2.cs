using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_2 : Action
{
    public override void ActionEnter(Player script)
    {
        base.ActionEnter(script);
        me.GetAniCtrl.SetTrigger("Skill_2");
    }
    public override void ActionUpdate()
    {
    }
    public override void ActionFixedUpdate()
    {
    }

    public override void ActionExit()
    {
    }
}
