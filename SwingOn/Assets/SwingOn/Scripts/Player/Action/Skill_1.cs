using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_1 : Action
{
    private Vector3 startPos;
    private Vector3 targetPos_1;
    private Vector3 targetPos_2;

    public override void ActionEnter(Player script)
    {
        base.ActionEnter(script);
        me.MoveCtrl.CanMove = false;
        me.GetAniCtrl.SetTrigger("Skill_1");
        startPos = me.transform.position;
        targetPos_1 = startPos + me.transform.forward * me.GetActionTable.normalDashDistance;
    }
    public override void ActionUpdate()
    {
        if (me.GetAniCtrl.GetCurrentAnimatorStateInfo(0).IsName("NormalDash"))
        {
            if(!me.GetActionTable.isCurrentAnimationOver(0.61f)) NormalDash(); //0.645
        }
        
        if (!me.GetAniCtrl.GetBool("DoubleDash"))
        {
            if (me.GetActionTable.AttType == Enums.PlayerAttType.Hard)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    me.GetAniCtrl.SetBool("DoubleDash", true);
                    targetPos_2 = targetPos_1 + me.transform.forward * me.GetActionTable.hardDashDistance;
                }
            }
        }
        if (me.GetAniCtrl.GetCurrentAnimatorStateInfo(0).IsName("HardDash"))
        {
            if (!me.GetActionTable.isCurrentAnimationOver(0.18f)) HardDash(); // 0.18
        }

        if (me.GetActionTable.Dash_Finish)
        {
            me.GetActionTable.Dash_Finish = false;
            me.GetActionTable.SetCurAction((int)Enums.PlayerActions.None);
        }
    }
    public override void ActionFixedUpdate()
    {
    }

    public override void ActionExit()
    {
        me.GetAniCtrl.ResetTrigger("Skill_1");
        me.GetAniCtrl.SetBool("DoubleDash", false);
        me.MoveCtrl.CanMove = true;
        startPos = Vector3.zero;
        targetPos_1 = Vector3.zero;
        targetPos_2 = Vector3.zero;
    }
    private void NormalDash()
    {
        me.transform.position = Vector3.Lerp(me.transform.position, targetPos_1, me.GetActionTable.normalDashSpeed);
    }
    private void HardDash()
    {
        me.transform.position = Vector3.Lerp(me.transform.position, targetPos_2, me.GetActionTable.hardDashSpeed);
    }
}
 