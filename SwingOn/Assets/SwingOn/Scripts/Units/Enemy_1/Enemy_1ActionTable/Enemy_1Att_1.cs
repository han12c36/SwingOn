using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1Att_1 : Action<Enemy_1>
{
    int PatternIndex;
    int RandomNum;
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
                    Debug.Log("소환 중");
                    //랜덤위치 찾아와서 거따 소환
                }
                else if (PatternIndex == (int)Enemy_1Pattern.Hill)
                {
                    Debug.Log("힐 중");
                    //근처 범위 몬스터 가져와서 힐

                }
            }
        }
    }
    public override void ActionExit()
    {
        PatternIndex = 0;
        RandomNum = 0;
    }

    private void SetTrigger_Enemy_1(int PatternIndex)
    {
        // 1. 소환 2. 광역 힐 3. 일반 근접공격
        switch (PatternIndex)
        {
            case (int)Enemy_1Pattern.Spawn:
                {
                    RandomNum = Random.Range(3, 6);
                    Debug.Log(RandomNum);
                    me.GetAniCtrl.SetTrigger("isCast");
                    for(int i = 0; i < RandomNum; i++)
                    {
                        GameObject obj = PoolingManager.Instance.LentalObj("Enemy_1_Un");
                        obj.transform.position = MySTL.RandomVec(me.transform.position, 5f);
                    }
                }
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
