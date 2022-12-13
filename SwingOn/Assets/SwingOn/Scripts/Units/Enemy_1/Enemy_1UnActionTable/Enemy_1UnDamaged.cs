using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1UnDamaged : Action<Enemy_1Un>
{
    public override void ActionEnter(Enemy_1Un script)
    {
        base.ActionEnter(script);
        GameManager.Instance.GetCoroutineHelper.StartCoroutine(me.ChangeMaterial(me.OriginMaterial, me.DamagedMaterial, 0.3f));
    }
    public override void ActionUpdate()
    {
    }
    public override void ActionExit()
    {
    }
}
