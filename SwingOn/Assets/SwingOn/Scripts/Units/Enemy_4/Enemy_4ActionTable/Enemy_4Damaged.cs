using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_4Damaged : Action<Enemy_4>
{
    public override void ActionEnter(Enemy_4 script)
    {
        base.ActionEnter(script);
        me.hitCount--;
        GameManager.Instance.GetCoroutineHelper.StartCoroutine(me.ActionTable.ChangeMaterial(me.OriginMaterial, me.DamagedMaterial, 0.3f));
        me.GetAniCtrl.SetTrigger("isDamaged");
        //me.MoveStop();
    }

    public override void ActionUpdate()
    {
        if (me.ActionTable.isHitFinish)
        {
            me.ActionTable.isHitFinish = false;
            me.ActionTable.SetCurAction((int)Enums.Enemy_4Actions.Idle);
        }
    }
    public override void ActionExit()
    {
        me.GetAniCtrl.ResetTrigger("isDamaged");
    }
}
