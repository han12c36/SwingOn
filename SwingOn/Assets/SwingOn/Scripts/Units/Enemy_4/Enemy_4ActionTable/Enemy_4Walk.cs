using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_4Walk : Action<Enemy_4>
{
    public override void ActionEnter(Enemy_4 script)
    {
        base.ActionEnter(script);
        me.GetAniCtrl.SetBool("isWalk", true);

    }
    public override void ActionUpdate()
    {
        if (!me.isHold)
        {
            Vector3 vec = new Vector3(me.GetTarget.transform.position.x, 0.0f, me.GetTarget.transform.position.z);
            me.transform.LookAt(vec);
            me.MoveOrder(me.GetTarget.transform, me.status.Speed);
            if (me.GetDistToTarget < me.status.AttRange) me.ActionTable.SetCurAction(me.ActionTable.Enemy_4AttThink());
        }
    }
    public override void ActionExit()
    {
        me.GetAniCtrl.SetBool("isWalk", false);
    }
}
