using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1Idle : Action<Enemy_1>
{
    public override void ActionEnter(Enemy_1 script)
    {
        base.ActionEnter(script);
    }
    public override void ActionUpdate()
    {
        if(me.GetPlayer)
        {
            if (me.Timer < me.idleWaitTime) me.Timer += Time.deltaTime;
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
    }
}
