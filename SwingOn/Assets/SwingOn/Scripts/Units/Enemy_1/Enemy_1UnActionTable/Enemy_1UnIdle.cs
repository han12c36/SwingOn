using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1UnIdle : Action<Enemy_1Un>
{
    public override void ActionEnter(Enemy_1Un script)
    {
        base.ActionEnter(script);
    }
    public override void ActionUpdate()
    {
        if(me.timer < me.awakeTime) me.timer += Time.deltaTime;
        else
        {
            me.timer = 0.0f;
            me.ActionTable.SetCurAction((int)Enums.Enemy_1EggActions.Death);
        }

    }
    public override void ActionExit()
    {
        me.timer = 0.0f;
    }
}
