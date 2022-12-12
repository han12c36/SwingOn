using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1Damaged : Action<Enemy_1>
{
    float nuckBackSpeed = 0.3f;
    Vector3 nuckBackPos = Vector3.zero;
    public override void ActionEnter(Enemy_1 script)
    {
        base.ActionEnter(script);
        GameManager.Instance.GetCoroutineHelper.StartCoroutine(me.ChangeMaterial(me.OriginMaterial, me.DamagedMaterial, 0.3f));
        me.GetAniCtrl.SetTrigger("isDamaged");
        me.MoveStop();
        Vector3 vec = (-me.transform.forward).normalized * nuckBackSpeed;
        nuckBackPos = me.transform.position + vec;
    }

    public override void ActionUpdate()
    {
        //µÚ·Î »ìÂ¦ ³Ë¹é
        me.transform.position = Vector3.Lerp(me.transform.position, nuckBackPos, 0.05f);
        if (me.GetAniCtrl.GetCurrentAnimatorStateInfo(0).IsName("Damaged"))
        {
            if (me.isCurrentAnimationOver(1.0f))
            {
                me.ActionTable.SetCurAction((int)Enums.Enemy_1Actions.Trace);
            }
        }
    }
    public override void ActionExit()
    {
    }
}
