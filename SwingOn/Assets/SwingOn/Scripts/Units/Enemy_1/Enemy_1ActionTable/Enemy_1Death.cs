using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1Death : Action<Enemy_1>
{
    float ExistTime = 3.0f;
    float timer;
    public override void ActionEnter(Enemy_1 script)
    {
        base.ActionEnter(script);
        Debug.Log("�׾�...");
        me.GetAniCtrl.SetTrigger("isDeath");
        me.MoveStop();
    }
    public override void ActionUpdate()
    {
        if(timer < ExistTime) timer += Time.deltaTime;
        else
        {
            timer = 0.0f;
            PoolingManager.Instance.ReturnObj(me.gameObject);
        }
    }
    public override void ActionExit()
    {
        timer = 0.0f;
        me.GetAniCtrl.ResetTrigger("isDeath");
    }
}
