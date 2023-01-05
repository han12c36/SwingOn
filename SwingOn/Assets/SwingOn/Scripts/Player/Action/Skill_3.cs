using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_3 : Action<Player>
{
    public override void ActionEnter(Player script)
    {
        base.ActionEnter(script);
        me.hardGauge = 0;
        me.GetAniCtrl.ResetTrigger("NormalSwing");
        me.GetAniCtrl.ResetTrigger("SpeedSwing");
        me.GetAniCtrl.ResetTrigger("HardSwing");
        me.GetAniCtrl.ResetTrigger("Dash");
        me.GetAniCtrl.ResetTrigger("Blitz");
        me.GetAniCtrl.ResetTrigger("GroundBreak");
        me.GetAniCtrl.ResetTrigger("Tornado");
        me.GetAniCtrl.ResetTrigger("Dash");
        me.GetAniCtrl.ResetTrigger("PowerSlash");
        me.GetAniCtrl.ResetTrigger("HardTornado");
        me.ActionTable.Att_Finish = false;
        me.GetAniCtrl.applyRootMotion = false;
        me.GetAniCtrl.SetTrigger("Skill_3");
        me.GetAniCtrl.SetBool("ModeChange",true);
        me.PlayerWeapon.OnOffWeaponCollider(false);
    }
    public override void ActionUpdate()
    {
        if (me.ActionTable.Equipt_Finish)
        {
            me.ActionTable.Equipt_Finish = false;
            me.ActionTable.ModeChange = false;
            me.ActionTable.SetCurAction((int)Enums.PlayerActions.None);
        }
    }
    public override void ActionFixedUpdate()
    {
    }

    public override void ActionExit()
    {
        if (me.ActionTable.AttType == Enums.PlayerAttType.Normal)
        {
            if (me.ActionTable.tryChangeHardMode)
            {
                me.ActionTable.tryChangeHardMode = false;
                me.ActionTable.AttType = Enums.PlayerAttType.Hard;
            }
            else if(me.ActionTable.tryChangeSpeedMode)
            {
                me.ActionTable.tryChangeSpeedMode = false;
                me.ActionTable.AttType = Enums.PlayerAttType.Speed;
            }
        }
        else if (me.ActionTable.AttType == Enums.PlayerAttType.Hard ||
            me.ActionTable.AttType == Enums.PlayerAttType.Speed)
        {
            me.ActionTable.AttType = Enums.PlayerAttType.Normal;
        }
        me.ActionTable.playerUICtrl.ChangeModeIcon(me.ActionTable.AttType);
        me.ActionTable.ModeChange = false;
        me.GetAniCtrl.SetBool("ModeChange", false);
    }
}
