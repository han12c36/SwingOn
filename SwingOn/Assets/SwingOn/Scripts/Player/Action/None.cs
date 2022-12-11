using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class None : Action<Player>
{
    public override void ActionEnter(Player script)
    {
        base.ActionEnter(script);
        me.MoveCtrl.CanMove = true;
        me.ActionTable.AnimationSpeed = 1.0f;
    }

    public override void ActionUpdate()
    {
    }
    public override void ActionExit()
    {
    }
}
