using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1Trace : Action<Enemy_1>
{
    public override void ActionEnter(Enemy_1 script)
    {
        base.ActionEnter(script);
        me.GetAniCtrl.SetBool("isMove", true);
    }
    public override void ActionUpdate()
    {
        me.transform.LookAt(me.GetTarget.transform.position);
        me.MoveOrder(me.GetTarget.transform, 2.0f);

        if (me.GetDistToTarget < me.status.AttRange)
        {
            me.ActionTable.SetCurAction((int)Enums.Enemy_1Actions.Att_1);
        }

    }
    public override void ActionExit()
    {
        me.GetAniCtrl.SetBool("isMove", false);
        me.MoveStop();
    }
}
