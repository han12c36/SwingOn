using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1Att_1 : Action<Enemy_1>
{
    public override void ActionEnter(Enemy_1 script)
    {
        base.ActionEnter(script);
        me.MoveStop();
        //여기서 일반 몹과 네임드 몹으로 나뉜다.
        //일반몹은 그냥 와서 떄리기만
        //네임드 몹은 도중 소환 기술 사용
        me.transform.LookAt(me.GetTarget.transform.position);
        me.GetAniCtrl.SetBool("isAtt", true);
    }

    public override void ActionUpdate()
    {
    }
    public override void ActionExit()
    {
        me.GetAniCtrl.SetBool("isAtt", false);
    }

}
