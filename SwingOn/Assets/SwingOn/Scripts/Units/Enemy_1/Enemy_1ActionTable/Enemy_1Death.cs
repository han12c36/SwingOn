using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1Death : Action<Enemy_1>
{
    public override void ActionEnter(Enemy_1 script)
    {
        base.ActionEnter(script);
        me.GetAniCtrl.SetTrigger("isDeath");
        me.MoveStop();
        //전부 초기화 시키기(풀링에서 가져와야하니까)
    }
    public override void ActionUpdate()
    {
    }
    public override void ActionExit()
    {
    }
}
