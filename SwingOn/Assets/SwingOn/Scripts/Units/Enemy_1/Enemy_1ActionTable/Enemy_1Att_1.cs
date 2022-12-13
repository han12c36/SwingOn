using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1Att_1 : Action<Enemy_1>
{
    int PatternIndex;
    public override void ActionEnter(Enemy_1 script)
    {
        base.ActionEnter(script);
        me.MoveStop();
        me.transform.LookAt(me.GetTarget.transform.position);
        if (me.enemyType == Enums.EnemyType.Normal) PatternIndex = (int)Enemy_1Pattern.MeleeAtt;
        else PatternIndex = ((Enemy_1)me).Think(me.PatternValue);
        SetTrigger_Enemy_1(PatternIndex);
    }

    public override void ActionUpdate()
    {
        //�б� ó���ϱ�
        if (me.GetAniCtrl.GetCurrentAnimatorStateInfo(0).IsName("Att"))
        {
            if (me.isCurrentAnimationOver(1.0f))
            {
                me.ActionTable.SetCurAction((int)Enums.Enemy_1Actions.Idle);
            }
        }
        if (me.GetAniCtrl.GetCurrentAnimatorStateInfo(0).IsName("Cast"))
        {
            if (me.isCurrentAnimationOver(1.0f))
            {
                me.ActionTable.SetCurAction((int)Enums.Enemy_1Actions.Idle);
            }
            else
            {
                if (PatternIndex == (int)Enemy_1Pattern.Spawn)
                {
                    Debug.Log("��ȯ ��");
                    //
                    //ini(me.Egg)
                }
                else if (PatternIndex == (int)Enemy_1Pattern.Hill)
                {
                    Debug.Log("�� ��");
                }
            }
        }
    }
    public override void ActionExit()
    {
        PatternIndex = 0;
    }

    private void SetTrigger_Enemy_1(int PatternIndex)
    {
        // 1. ��ȯ 2. ���� �� 3. �Ϲ� ��������
        switch (PatternIndex)
        {
            case (int)Enemy_1Pattern.Spawn:
                me.GetAniCtrl.SetTrigger("isCast");
                break;
            case (int)Enemy_1Pattern.Hill:
                me.GetAniCtrl.SetTrigger("isCast");
                break;
            case (int)Enemy_1Pattern.MeleeAtt:
                me.GetAniCtrl.SetTrigger("isAtt");
                break;
            default:
                break;
        }
    }
}
