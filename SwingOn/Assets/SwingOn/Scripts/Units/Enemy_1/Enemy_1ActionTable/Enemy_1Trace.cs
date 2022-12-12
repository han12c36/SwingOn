using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1Trace : Action<Enemy_1>
{
    public override void ActionEnter(Enemy_1 script)
    {
        base.ActionEnter(script);
        me.MoveOrder(me.GetTarget.transform, 2.0f);
        me.GetAniCtrl.SetBool("isMove", true);
    }
    public override void ActionUpdate()
    {
        me.transform.LookAt(me.GetTarget.transform.position);
        //me.transform.position = Vector3.MoveTowards(me.transform.position, me.GetTarget.transform.position, me.moveSpeed);
    }
    public override void ActionExit()
    {
        me.GetAniCtrl.SetBool("isMove", false);
        me.MoveStop();
    }
}
