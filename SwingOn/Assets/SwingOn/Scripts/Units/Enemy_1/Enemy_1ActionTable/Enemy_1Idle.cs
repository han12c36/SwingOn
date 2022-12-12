using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1Idle : Action<Enemy_1>
{
    public override void ActionEnter(Enemy_1 script)
    {
        base.ActionEnter(script);
        me.GetAniCtrl.SetBool("isIdle", true);
        me.MoveStop();
    }
    public override void ActionUpdate()
    {
        if(me.GetTarget)
        {
            if (me.Timer < me.IdleWaitTime) me.Timer += Time.deltaTime;
            else
            {
                me.ActionTable.SetCurAction((int)Enums.Enemy_1Actions.Trace);
                me.Timer = 0.0f;
            }
        }
    }
    public override void ActionExit()
    {
        me.Timer = 0.0f;
        me.GetAniCtrl.SetBool("isIdle", false);
    }
}
