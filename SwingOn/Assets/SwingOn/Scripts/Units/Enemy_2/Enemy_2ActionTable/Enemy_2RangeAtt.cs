using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2RangeAtt : Action<Enemy_2>
{
     public override void ActionEnter(Enemy_2 script)
    {
        base.ActionEnter(script);
        me.GetAniCtrl.SetTrigger("isRangeAtt");
    }
    public override void ActionUpdate()
    {
        if (me.GetAniCtrl.GetCurrentAnimatorStateInfo(0).IsName("RangeAtt"))
        {
            if (me.isCurrentAnimationOver(1.0f))
            {
                me.ActionTable.SetCurAction((int)Enums.Enemy_2Actions.Idle);
            }
        }
    }
    public override void ActionExit()
    {
        me.GetAniCtrl.ResetTrigger("isRangeAtt");
    }
}
