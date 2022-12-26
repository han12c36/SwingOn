using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_5Walk : Action<Enemy_5>
{

    public override void ActionEnter(Enemy_5 script)
    {
        base.ActionEnter(script);
        me.GetAniCtrl.SetBool("isWalk", true);
    }
    public override void ActionUpdate()
    {
        if (!me.isHold)
        {
            //Vector3 vec = new Vector3(me.GetTarget.transform.position.x, 0.0f, me.GetTarget.transform.position.z);
            Vector3 targetVec = new Vector3(-me.GetTarget.transform.position.x, me.GetTarget.transform.position.y, -me.GetTarget.transform.position.z);
            Vector3 vec = new Vector3(targetVec.x, 0.0f, targetVec.z);
            me.transform.LookAt(vec);
            me.MoveOrder(targetVec, me.status.Speed * 0.5f);
            if(me.GetDistToTarget >= me.ActionTable.fallAttRange) me.ActionTable.SetCurAction(me.ActionTable.Enemy_5FallAttThink());
        }
    }
    public override void ActionExit()
    {
        me.GetAniCtrl.SetBool("isWalk", false);
    }
}
