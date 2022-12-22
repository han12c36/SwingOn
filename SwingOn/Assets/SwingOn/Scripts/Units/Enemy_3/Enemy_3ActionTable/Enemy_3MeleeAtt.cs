using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_3MeleeAtt : Action<Enemy_3>
{
    public override void ActionEnter(Enemy_3 script)
    {
        base.ActionEnter(script);
        me.MoveStop();
        me.GetAniCtrl.SetTrigger("isMeleeAtt");
        me.transform.LookAt(me.GetTarget.transform.position);
    }
    public override void ActionUpdate()
    {
        if (me.GetAniCtrl.GetCurrentAnimatorStateInfo(0).IsName("Att"))
        {
            if (me.isCurrentAnimationOver(1.0f))
            {
                me.ActionTable.SetCurAction((int)Enums.Enemy_3Actions.Idle);
            }
        }
    }
    public override void ActionExit()
    {
        me.GetAniCtrl.ResetTrigger("isMeleeAtt");
    }
}
