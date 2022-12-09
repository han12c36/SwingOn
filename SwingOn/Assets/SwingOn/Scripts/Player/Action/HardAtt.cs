using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardAtt : Action
{
    public override void ActionEnter(Player script)
    {
        base.ActionEnter(script);
        me.MoveCtrl.CanMove = false;
        me.GetAniCtrl.SetTrigger("HardSwing");
        me.GetAniCtrl.applyRootMotion = true;
    }

    public override void ActionUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) me.GetAniCtrl.SetTrigger("HardSwing");

        if (me.GetActionTable.Att_Finish)
        {
            me.GetActionTable.Att_Finish = false;
            me.GetActionTable.SetCurAction((int)Enums.PlayerActions.None);
        }
    }
    public override void ActionExit()
    {
        me.GetActionTable.Att_Finish = false;
        me.GetAniCtrl.applyRootMotion = false;
        me.MoveCtrl.CanMove = true;
    }
}
