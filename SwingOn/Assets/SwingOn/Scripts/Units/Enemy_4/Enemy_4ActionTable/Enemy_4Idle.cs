using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_4Idle : Action<Enemy_4>
{
    float timer;
    public override void ActionEnter(Enemy_4 script)
    {
        base.ActionEnter(script);
        me.GetAniCtrl.SetBool("isIdle", true);
    }
    public override void ActionUpdate()
    {
        if (timer < me.ActionTable.Enemy_4WaitTime) timer += Time.deltaTime;
        else
        {
            timer = 0.0f;
            if (me.GetDistToTarget >= me.status.AttRange) me.ActionTable.SetCurAction(me.ActionTable.Enemy_4Think());
            else me.ActionTable.SetCurAction(me.ActionTable.Enemy_4AttThink());
        }
    }
    public override void ActionExit()
    {
        timer = 0.0f;
        me.GetAniCtrl.SetBool("isIdle", false);
    }
}
