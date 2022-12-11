using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAtt : Action<Player>
{
    public override void ActionEnter(Player script)
    {
        base.ActionEnter(script);
        me.MoveCtrl.CanMove = false;
        me.GetAniCtrl.SetTrigger("NormalSwing");
        me.GetAniCtrl.applyRootMotion = true;
    }

    public override void ActionUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) me.GetAniCtrl.SetTrigger("NormalSwing");

        if (me.ActionTable.Att_Finish)
        {
            me.ActionTable.Att_Finish = false;
            me.ActionTable.SetCurAction((int)Enums.PlayerActions.None);
        }
    }
    public override void ActionExit()
    {
        me.GetAniCtrl.ResetTrigger("NormalSwing");
        me.ActionTable.Att_Finish = false;
        me.GetAniCtrl.applyRootMotion = false;
        me.MoveCtrl.CanMove = true;
    }
}
