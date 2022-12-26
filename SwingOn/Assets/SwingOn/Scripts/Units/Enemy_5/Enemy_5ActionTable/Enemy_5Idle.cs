using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_5Idle : Action<Enemy_5>
{
    float timer;
    public override void ActionEnter(Enemy_5 script)
    {
        base.ActionEnter(script);
        me.GetAniCtrl.SetBool("isIdle", true);
    }
    public override void ActionUpdate()
    {
        if (timer < me.ActionTable.Enemy_5WaitTime) timer += Time.deltaTime;
        else
        {
            timer = 0.0f;
            if (me.GetDistToTarget <= me.ActionTable.thinkRange)
            {
                me.ActionTable.SetCurAction(me.ActionTable.Enemy_5WalkThink());
            }
            else
            {
                if(me.GetDistToTarget > me.ActionTable.fallAttRange)
                {
                    me.ActionTable.SetCurAction((int)Enums.Enemy_5Actions.Run);
                }
                else
                {
                    me.ActionTable.SetCurAction(me.ActionTable.Enemy_5FallAttThink());
                }
            }
        }
    }

    public override void ActionExit()
    {
        timer = 0.0f;
        me.GetAniCtrl.SetBool("isIdle", false);
    }
}
