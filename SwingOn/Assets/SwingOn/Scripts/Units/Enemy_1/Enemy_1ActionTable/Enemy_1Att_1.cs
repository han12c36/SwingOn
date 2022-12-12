using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1Att_1 : Action<Enemy_1>
{
    public override void ActionEnter(Enemy_1 script)
    {
        base.ActionEnter(script);
        me.MoveStop();
        //���⼭ �Ϲ� ���� ���ӵ� ������ ������.
        //�Ϲݸ��� �׳� �ͼ� �����⸸
        //���ӵ� ���� ���� ��ȯ ��� ���
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
