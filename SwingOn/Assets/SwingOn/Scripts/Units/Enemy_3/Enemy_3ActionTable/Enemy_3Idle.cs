using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_3Idle : Action<Enemy_3>
{
    float timer;
    public override void ActionEnter(Enemy_3 script)
    {
        base.ActionEnter(script);
        me.GetAniCtrl.SetBool("isIdle", true);
    }
    public override void ActionUpdate()
    {
        if (timer < me.ActionTable.Enemy_3WaitTime) timer += Time.deltaTime;
        else
        {
            timer = 0.0f;
            if (me.GetDistToTarget >= me.status.AttRange) me.ActionTable.SetCurAction(me.ActionTable.Enemy_3Think());
            else me.ActionTable.SetCurAction((int)Enums.Enemy_3Actions.MeleeAtt);
        }
    }
    public override void ActionExit()
    {
        timer = 0.0f;
        me.GetAniCtrl.SetBool("isIdle", false);
    }
}
