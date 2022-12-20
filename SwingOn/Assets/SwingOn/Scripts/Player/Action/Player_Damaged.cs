using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Damaged : Action<Player>
{
    public override void ActionEnter(Player script)
    {
        base.ActionEnter(script);
        me.hitCount--;
        GameManager.Instance.GetCoroutineHelper.StartCoroutine(me.ActionTable.ChangeMaterial(me.OriginMaterial, me.DamagedMaterial, 0.3f));
        me.GetAniCtrl.SetTrigger("isDamaged");
        //me.MoveCtrl.CanMove = false;
    }
    public override void ActionUpdate()
    {
        if (me.ActionTable.isHitFinish)
        {
            me.ActionTable.isHitFinish = false;
            me.ActionTable.SetCurAction((int)Enums.PlayerActions.None);
        }
    }
    public override void ActionExit()
    {
        //me.MoveCtrl.CanMove = true;
    }
}
