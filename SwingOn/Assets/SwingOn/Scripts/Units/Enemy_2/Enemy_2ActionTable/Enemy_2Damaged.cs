using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2Damaged : Action<Enemy_2>
{
    float nuckBackSpeed = 1.5f;
    Vector3 nuckBackPos = Vector3.zero;
    public override void ActionEnter(Enemy_2 script)
    {
        base.ActionEnter(script);
        me.hitCount--;
        GameManager.Instance.GetCoroutineHelper.StartCoroutine(me.ActionTable.ChangeMaterial(me.OriginMaterial, me.DamagedMaterial, 0.3f));
        me.GetAniCtrl.SetTrigger("isDamaged");
        me.MoveStop();
        Vector3 vec = Vector3.zero;
        if (!me.isHold) vec = (me.transform.position - me.GetTarget.transform.position).normalized * nuckBackSpeed;
        nuckBackPos = me.transform.position + vec;
    }

    public override void ActionUpdate()
    {
        if (!me.isHold) me.transform.position = Vector3.Lerp(me.transform.position, nuckBackPos, 0.05f);

        if (me.ActionTable.isHitFinish)
        {
            me.ActionTable.isHitFinish = false;
            me.ActionTable.SetCurAction((int)Enums.Enemy_2Actions.Idle);
            //if (me.GetDistToTarget > me.status.AttRange)
            //{
            //    me.ActionTable.SetCurAction((int)Enums.Enemy_1Actions.Trace);
            //}
            //else
            //{
            //    me.ActionTable.SetCurAction(me.ActionTable.Enemy_2Think());
            //}
        }
    }
    public override void ActionExit()
    {
    }
}
