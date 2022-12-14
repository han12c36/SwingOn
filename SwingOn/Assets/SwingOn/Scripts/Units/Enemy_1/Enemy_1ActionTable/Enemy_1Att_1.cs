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
        //else PatternIndex = ((Enemy_1)me).Think(me.PatternValue);
        else PatternIndex = (int)Enemy_1Pattern.Heal;

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
        }

        if(me.isCast)
        {
            me.isCast = false;
            if (PatternIndex == (int)Enemy_1Pattern.Spawn)
            {
                RandomNum = Random.Range(2,4);
                for (int i = 0; i < RandomNum; i++)
                {
                    GameObject obj = PoolingManager.Instance.LentalObj("Enemy_1_Un");
                    obj.transform.position = MySTL.RandomVec(me.transform.position, 5f);
                }
            }
            else if (PatternIndex == (int)Enemy_1Pattern.Heal)
            {
                Debug.Log("Шњ Сп");
                Collider[] nearEnemy = Physics.OverlapSphere(me.transform.position, me.recognizeRange);
                for(int i = 0; i < nearEnemy.Length; i++)
                {
                    if(nearEnemy[i].gameObject.layer == LayerMask.NameToLayer("Enemy"))
                    {
                        Enemy enemy = nearEnemy[i].gameObject.GetComponent<Enemy>();
                        //enemy.status.curHp += 2;
                        PoolingManager.Instance.PlayEffect("Effect_Heal", enemy.transform.position);
                    }
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
        switch (PatternIndex)
        {
            case (int)Enemy_1Pattern.Spawn:
                me.GetAniCtrl.SetTrigger("isCast");
                break;
            case (int)Enemy_1Pattern.Heal:
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
