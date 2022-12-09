using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class None : Action
{
    public override void ActionEnter(Player script)
    {
        base.ActionEnter(script);
        me.MoveCtrl.CanMove = true;
        me.GetActionTable.AnimationSpeed = 1.0f;
    }

    public override void ActionUpdate()
    {
    }
    public override void ActionExit()
    {
    }
}
