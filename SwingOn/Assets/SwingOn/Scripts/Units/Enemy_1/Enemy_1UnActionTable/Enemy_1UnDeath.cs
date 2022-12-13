using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1UnDeath : Action<Enemy_1Un>
{
    public override void ActionEnter(Enemy_1Un script)
    {
        base.ActionEnter(script);
        me.timer = 0.0f;
        me.GetAniCtrl.applyRootMotion = true;
        me.GetAniCtrl.SetTrigger("isDeath");
    }
    public override void ActionUpdate()
    {
        if (me.GetAniCtrl.GetCurrentAnimatorStateInfo(0).IsName("Spawn"))
        {
            if (me.isCurrentAnimationOver(0.1f))
            {
                GameObject obj = PoolingManager.Instance.LentalObj("Enemy_1_N");
                obj.transform.position = me.transform.position;
                obj.transform.rotation = me.transform.rotation;
                me.GetAniCtrl.applyRootMotion = false;
                PoolingManager.Instance.ReturnObj(me.gameObject);
            }
        }
    }
    public override void ActionExit()
    {
    }
}
