using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1Damaged : Action<Enemy_1>
{
    float nuckBackSpeed = 4.5f;
    Vector3 nuckBackPos = Vector3.zero;
    public override void ActionEnter(Enemy_1 script)
    {
        base.ActionEnter(script);
        me.GetAniCtrl.SetTrigger("isDamaged");
        me.MoveStop();
        Vector3 vec = (-me.transform.forward).normalized * nuckBackSpeed;
        nuckBackPos = me.transform.position + vec;
        me.GetComponentInChildren<SkinnedMeshRenderer>().material = me.DamagedMaterial;
    }

    public override void ActionUpdate()
    {
        //µÚ·Î »ìÂ¦ ³Ë¹é
        me.transform.position = Vector3.Lerp(me.transform.position, nuckBackPos, 0.05f);
    }
    public override void ActionExit()
    {
        me.GetComponentInChildren<SkinnedMeshRenderer>().material = me.OriginMaterial;
    }
}
