using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class None : Action
{
    public override void ActionEnter(Player script)
    {
        base.ActionEnter(script);
        Debug.Log("아무것도 아닌 상태 들어옴");
        me.GetActionTable.isAttFinish = false;
    }

    public override void ActionUpdate()
    {
    }
    public override void ActionExit()
    {
    }
}
