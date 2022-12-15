using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_3 : Action<Player>
{
    public override void ActionEnter(Player script)
    {
        base.ActionEnter(script);
        me.GetAniCtrl.ResetTrigger("NormalSwing");
        me.GetAniCtrl.ResetTrigger("HardSwing");
        me.GetAniCtrl.ResetTrigger("Skill_2");
        me.GetAniCtrl.SetBool("DoubleBlitz", false);
        me.ActionTable.Att_Finish = false;
        me.GetAniCtrl.applyRootMotion = false;
        me.GetAniCtrl.SetTrigger("Skill_3");
        me.GetAniCtrl.SetBool("ModeChange",true);
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
            me.ActionTable.AttType = Enums.PlayerAttType.Hard;
        }
        else if (me.ActionTable.AttType == Enums.PlayerAttType.Hard)
        {
            me.ActionTable.AttType = Enums.PlayerAttType.Normal;
        }
        me.ActionTable.ModeChange = false;
        me.GetAniCtrl.SetBool("ModeChange", false);
    }
}
