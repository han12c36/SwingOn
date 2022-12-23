using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_4MeleeAtt : Action<Enemy_4>
{
    public override void ActionEnter(Enemy_4 script)
    {
        base.ActionEnter(script);
        me.MoveStop();
        me.GetAniCtrl.SetBool("isScream", true);
    }
    public override void ActionUpdate()
    {
        if(me.ActionTable.isSreamFinish)
        {
            me.ActionTable.isSreamFinish = false;
            me.GetAniCtrl.SetBool("isScream", false);
            me.GetAniCtrl.SetTrigger("isMeleeAtt");
            me.transform.LookAt(me.GetTarget.transform.position);
        }

        if (me.GetAniCtrl.GetCurrentAnimatorStateInfo(0).IsName("MeleeAtt"))
        {
            if (me.isCurrentAnimationOver(1.0f))
            {
                me.ActionTable.SetCurAction((int)Enums.Enemy_4Actions.Idle);
            }
        }
    }
    public override void ActionExit()
    {
        me.GetAniCtrl.ResetTrigger("isMeleeAtt");
        me.GetAniCtrl.SetBool("isScream", false);
        me.ActionTable.isSreamFinish = false;
    }
}
