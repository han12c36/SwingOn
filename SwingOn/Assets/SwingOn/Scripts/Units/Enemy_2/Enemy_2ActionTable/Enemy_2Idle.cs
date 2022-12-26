using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2Idle : Action<Enemy_2>
{
    float timer;

    public override void ActionEnter(Enemy_2 script)
    {
        base.ActionEnter(script);
        me.GetAniCtrl.SetBool("isIdle",true);
    }
    public override void ActionUpdate()
    {
        if (timer < me.ActionTable.Enemy_2WaitTime) timer += Time.deltaTime;
        else
        {
            timer = 0.0f;
            if (me.GetDistToTarget >= me.status.AttRange)
            {
                me.ActionTable.SetCurAction(me.ActionTable.Enemy_2Think());
            }
            else me.ActionTable.SetCurAction((int)Enums.Enemy_2Actions.MeleeAtt);
        }
    }
    public override void ActionExit()
    {
        me.GetAniCtrl.SetBool("isIdle", false);
    }
}
