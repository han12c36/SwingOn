using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_3 : Action
{
    public override void ActionEnter(Player script)
    {
        base.ActionEnter(script);
        //me.GetAniCtrl.SetLayerWeight(1, 1.0f);
        //me.GetAniCtrl.SetLayerWeight(2, 1.0f);
        me.GetAniCtrl.ResetTrigger("NormalSwing");
        me.GetAniCtrl.ResetTrigger("HardSwing");
        me.GetActionTable.Att_Finish = false;
        me.GetAniCtrl.applyRootMotion = false;
        me.GetAniCtrl.SetTrigger("Skill_3");
        me.GetAniCtrl.SetBool("ModeChange",true);
    }
    public override void ActionUpdate()
    {
        //무기 바끼는 이펙트 넣어주고
        //분노 모션같은거?
        if(me.GetActionTable.Equipt_Finish)
        {
            me.GetActionTable.Equipt_Finish = false;
            me.GetActionTable.SetCurAction((int)Enums.PlayerActions.None);
        }
    }
    public override void ActionFixedUpdate()
    {
    }

    public override void ActionExit()
    {
        if (me.GetActionTable.AttType == Enums.PlayerAttType.Normal)
        {
            me.GetActionTable.AttType = Enums.PlayerAttType.Hard;
        }
        else if (me.GetActionTable.AttType == Enums.PlayerAttType.Hard)
        {
            me.GetActionTable.AttType = Enums.PlayerAttType.Normal;
        }
        me.GetAniCtrl.SetBool("ModeChange", false);
        //me.GetAniCtrl.SetLayerWeight(1, 0.0f);
        //me.GetAniCtrl.SetLayerWeight(2, 0.0f);
    }
}
