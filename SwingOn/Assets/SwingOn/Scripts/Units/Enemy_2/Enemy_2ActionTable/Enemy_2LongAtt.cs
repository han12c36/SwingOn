using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2LongAtt : Action<Enemy_2>
{
     public override void ActionEnter(Enemy_2 script)
    {
        base.ActionEnter(script);
        me.MoveStop();
        me.GetAniCtrl.SetTrigger("isLongAtt");
    }
    public override void ActionUpdate()
    {
        if (me.GetAniCtrl.GetCurrentAnimatorStateInfo(0).IsName("LongAtt"))
        {
            if (me.isCurrentAnimationOver(1.0f))
            {
                me.ActionTable.SetCurAction((int)Enums.Enemy_2Actions.Idle);
            }
        }
    }
    public override void ActionExit()
    {
        me.GetAniCtrl.ResetTrigger("isLongAtt");
    }
}
