using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_1 : Action
{
    private Vector3 startPos;
    private Vector3 targetPos;

    public override void ActionEnter(Player script)
    {
        base.ActionEnter(script);
        me.GetAniCtrl.SetTrigger("Skill_1");
        Debug.Log("대쉬진입");
        startPos = me.transform.position;
    }
    public override void ActionUpdate()
    {
        Dash();
        if (me.GetAniCtrl.GetCurrentAnimatorStateInfo(0).IsName("Skill_1"))
        {
            if (me.GetActionTable.isCurrentAnimationOver(1.0f))
            {
                me.GetActionTable.SetCurAction((int)Enums.PlayerActions.None);
            }
        }
    }
    public override void ActionFixedUpdate()
    {
    }

    public override void ActionExit()
    {
        Debug.Log("대쉬끝");
        startPos = Vector3.zero;
        targetPos = Vector3.zero;
    }
    private void Dash()
    {
        Debug.Log("대쉬중");
        Vector3 targetPos = me.transform.forward * me.GetActionTable.DashLength;
        me.transform.position += Vector3.Lerp(startPos, targetPos, me.GetActionTable.dashSpeed);
    }
}
 