using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1Trace : Action<Enemy_1>
{
    public override void ActionEnter(Enemy_1 script)
    {
        base.ActionEnter(script);
    }
    public override void ActionUpdate()
    {
        me.transform.LookAt(me.GetPlayer.transform.position);
    }
    public override void ActionExit()
    {
    }
}
