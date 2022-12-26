using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2Trace : Action<Enemy_2>
{
    public override void ActionEnter(Enemy_2 script)
    {
        base.ActionEnter(script);
        me.GetAniCtrl.SetBool("isMove", true);
    }
    public override void ActionUpdate()
    {
        if (!me.isHold)
        {
            Vector3 vec = new Vector3(me.GetTarget.transform.position.x, 0.0f, me.GetTarget.transform.position.z);
            me.transform.LookAt(vec);
            me.MoveOrder(me.GetTarget.transform, me.status.Speed);
            if (me.GetDistToTarget < me.status.AttRange) me.ActionTable.SetCurAction((int)Enums.Enemy_2Actions.MeleeAtt);
        }
    }
    public override void ActionExit()
    {
        me.GetAniCtrl.SetBool("isMove", false);
    }
}
