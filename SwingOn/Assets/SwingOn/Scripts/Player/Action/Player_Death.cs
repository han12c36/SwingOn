using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Death : Action<Player>
{
    public override void ActionEnter(Player script)
    {
        base.ActionEnter(script);
        GameManager.Instance.GetCoroutineHelper.StartCoroutine(me.ActionTable.ChangeMaterial(me.OriginMaterial, me.DamagedMaterial, 0.3f));
        me.GetAniCtrl.SetTrigger("isDeath");
        me.MoveCtrl.CanMove = false;
    }
    public override void ActionUpdate()
    {
    }
    public override void ActionExit()
    {
        //me.MoveCtrl.CanMove = true;
    }
}
