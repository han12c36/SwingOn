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
        me.MoveCtrl.CanMove = false;
    }

    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(me.GetActionTable.AttType == Enums.PlayerAttType.Normal)
            {
                me.GetActionTable.SetCurAction((int)Enums.PlayerActions.NormalAtt);
            }
            else if(me.GetActionTable.AttType == Enums.PlayerAttType.Hard)
            {
                me.GetActionTable.SetCurAction((int)Enums.PlayerActions.HardAtt);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            me.GetActionTable.SetCurAction((int)Enums.PlayerActions.Skill_1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            me.GetActionTable.SetCurAction((int)Enums.PlayerActions.Skill_2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            me.GetActionTable.SetCurAction((int)Enums.PlayerActions.Skill_3);
        }
    }
}
