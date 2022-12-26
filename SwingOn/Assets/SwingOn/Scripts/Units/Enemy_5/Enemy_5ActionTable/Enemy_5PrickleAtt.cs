using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_5PrickleAtt : Action<Enemy_5>
{
    public override void ActionEnter(Enemy_5 script)
    {
        base.ActionEnter(script);
        me.MoveStop();
        me.GetAniCtrl.SetBool("isScream", true);
    }
    public override void ActionUpdate()
    {
        if (me.ActionTable.isSreamFinish)
        {
            me.ActionTable.isSreamFinish = false;
            me.GetAniCtrl.SetBool("isScream", false);
            me.GetAniCtrl.SetTrigger("isPrickleAtt");
            me.transform.LookAt(me.GetTarget.transform.position);
        }

        if (me.GetAniCtrl.GetCurrentAnimatorStateInfo(0).IsName("PrickleAtt"))
        {
            if (me.isCurrentAnimationOver(1.0f))
            {
                me.ActionTable.SetCurAction((int)Enums.Enemy_5Actions.Idle);
            }
        }
    }
    public override void ActionExit()
    {
        me.GetAniCtrl.ResetTrigger("isPrickleAtt");
        me.GetAniCtrl.SetBool("isScream", false);
        me.ActionTable.isSreamFinish = false;
    }
}
