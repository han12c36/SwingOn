using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2Ready : Action<Enemy_2>
{
    float startDelayTime = 2.0f;
    float timer;
    public override void ActionEnter(Enemy_2 script)
    {
        base.ActionEnter(script);
    }
    public override void ActionUpdate()
    {
        if (timer < startDelayTime) timer += Time.deltaTime;
        else
        {
            timer = 0.0f;
            if (!me.GetAniCtrl.GetCurrentAnimatorStateInfo(0).IsName("Spawn to Ground Idle"))
            {
                me.GetAniCtrl.SetTrigger("isStart");
            }
        }

        if(me.ActionTable.isReadyComplete)
        {
            me.ActionTable.isReadyComplete = false;
            me.ActionTable.SetCurAction((int)Enums.Enemy_2Actions.Idle);
        }
    }
    public override void ActionExit()
    {
        me.GetAniCtrl.ResetTrigger("isStart");
    }
}
