using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_2 : Action<Player>
{
    //Enemy targetEnemy;
    public override void ActionEnter(Player script)
    {
        base.ActionEnter(script);

        me.ActionTable.ChangeLayer(me.transform.root, me.ActionTable.ignoreLayer, me.ActionTable.WeaponLayer);
        me.MoveCtrl.CanMove = false;

        if (me.ActionTable.AttType == Enums.PlayerAttType.Normal)
        {
            me.GetAniCtrl.SetTrigger("Tornado");
            me.ActionTable.isTornadoCool = true;
        }
        else if (me.ActionTable.AttType == Enums.PlayerAttType.Speed)
        {
            me.GetAniCtrl.SetTrigger("PowerSlash");
        }
        else if (me.ActionTable.AttType == Enums.PlayerAttType.Hard)
        {
            me.GetAniCtrl.SetTrigger("HardTornado");
        }
    }
    public override void ActionUpdate()
    {
        if (me.ActionTable.Tornado_Finish)
        {
            me.ActionTable.Tornado_Finish = false;
            me.ActionTable.SetCurAction((int)Enums.PlayerActions.None);
        }
        if (me.ActionTable.PowerSlash_Finish)
        {
            me.ActionTable.PowerSlash_Finish = false;
            me.ActionTable.SetCurAction((int)Enums.PlayerActions.None);
        }
        if (me.ActionTable.HardTornado_Finish)
        {
            me.ActionTable.HardTornado_Finish = false;
            me.ActionTable.SetCurAction((int)Enums.PlayerActions.None);
        }
    }
    public override void ActionFixedUpdate()
    {
    }

    public override void ActionExit()
    {
        me.GetAniCtrl.ResetTrigger("Tornado");
        me.GetAniCtrl.ResetTrigger("PowerSlash");
        me.GetAniCtrl.ResetTrigger("HardTornado");
        me.ActionTable.Tornado_Finish = false;
        me.ActionTable.PowerSlash_Finish = false;
        me.ActionTable.HardTornado_Finish = false;
        me.MoveCtrl.CanMove = true;
        me.ActionTable.ChangeLayer(me.transform.root, me.ActionTable.originLayer, me.ActionTable.WeaponLayer);
    }
}