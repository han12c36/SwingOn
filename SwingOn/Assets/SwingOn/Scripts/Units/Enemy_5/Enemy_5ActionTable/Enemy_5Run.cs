using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_5Run : Action<Enemy_5>
{
    public override void ActionEnter(Enemy_5 script)
    {
        base.ActionEnter(script);
        me.GetAniCtrl.SetBool("isRun", true);
    }
    public override void ActionUpdate()
    {
        if (!me.isHold)
        {
            Vector3 vec = new Vector3(me.GetTarget.transform.position.x, 0.0f, me.GetTarget.transform.position.z);
            me.transform.LookAt(vec);
            me.MoveOrder(me.GetTarget.transform, me.status.Speed);
            if(me.GetDistToTarget < me.status.AttRange) me.ActionTable.SetCurAction(me.ActionTable.Enemy_5ShortAttThink());
        }
    }
    public override void ActionExit()
    {
        me.GetAniCtrl.SetBool("isRun", false);
    }
}
