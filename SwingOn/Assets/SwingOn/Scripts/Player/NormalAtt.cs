using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAtt : Action
{
    public override void ActionEnter(Player script)
    {
        base.ActionEnter(script);
        me.GetActionTable.ClickCount++;
        
    }

    public override void ActionUpdate()
    {
        if(me.GetActionTable.isAttFinish)
        {
            me.GetActionTable.isAttFinish = false;
            me.GetActionTable.SetCurAction((int)Enums.PlayerActions.None);
        }
    }
    public override void ActionExit()
    {
        Debug.Log("노말공격 나가기");
    }
}
