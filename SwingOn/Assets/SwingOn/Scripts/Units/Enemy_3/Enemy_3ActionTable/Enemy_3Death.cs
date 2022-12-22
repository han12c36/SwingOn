using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_3Death : Action<Enemy_3>
{
    float ExistTime = 3.0f;
    float timer;
    public override void ActionEnter(Enemy_3 script)
    {
        base.ActionEnter(script);
        me.isHold = false;
        me.hitCount = 0;
        me.GetAniCtrl.SetTrigger("isDeath");
        me.MoveStop();
        me.components.collider.enabled = false;
        GameManager.Instance.GetCoroutineHelper.StartCoroutine(me.ActionTable.ChangeMaterial(me.OriginMaterial, me.DamagedMaterial, 0.3f));
    }
    public override void ActionUpdate()
    {
        if (timer < ExistTime) timer += Time.deltaTime;
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
