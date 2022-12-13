using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1Death : Action<Enemy_1>
{
    public override void ActionEnter(Enemy_1 script)
    {
        base.ActionEnter(script);
        me.GetAniCtrl.SetTrigger("isDeath");
        me.MoveStop();
        PoolingManager.Instance.ReturnObj(me.gameObject);
    }
    public override void ActionUpdate()
    {
    }
    public override void ActionExit()
    {
    }
}
