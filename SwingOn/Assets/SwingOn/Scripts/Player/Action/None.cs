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
        GetInput();
    }
    public override void ActionExit()
    {
    }

    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            me.GetActionTable.SetCurAction((int)Enums.PlayerActions.Skill_1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            me.GetActionTable.SetCurAction((int)Enums.PlayerActions.Skill_2);
        }
    }
}
