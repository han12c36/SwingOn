using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_2 : Action<Player>
{
    Enemy targetEnemy;
    public override void ActionEnter(Player script)
    {
        base.ActionEnter(script);
        me.MoveCtrl.CanMove = false;
        targetEnemy = FindTarget();
        if(targetEnemy != null) me.GetAniCtrl.SetTrigger("Skill_2");
    }
    public override void ActionUpdate()
    {
        if (me.GetAniCtrl.GetCurrentAnimatorStateInfo(0).IsName("NormalBlitz"))
        {
            if (!me.ActionTable.isCurrentAnimationOver(0.62f))
            {
                NormalBlitz();
            }
        }

        if (!me.GetAniCtrl.GetBool("DoubleBlitz"))
        {
            if (me.ActionTable.AttType == Enums.PlayerAttType.Hard)
            {
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    if(targetEnemy != null)
                    {
                        targetEnemy.isHold = true;
                        Vector3 vec = targetEnemy.transform.position + (-targetEnemy.transform.forward).normalized * (targetEnemy.transform.localScale.z * 0.7f);
                        vec = new Vector3(vec.x, 0.0f, vec.z);
                        me.transform.position = vec;
                        me.transform.LookAt(targetEnemy.transform.forward);
                        me.GetAniCtrl.SetBool("DoubleBlitz", true);
                    }
                }
            }
        }
        if (me.GetAniCtrl.GetCurrentAnimatorStateInfo(0).IsName("HardBlitz"))
        {
            //if (!me.GetActionTable.isCurrentAnimationOver(0.62f)) 
            HardBlitz();
        }

        if (me.ActionTable.Blitz_Finish || targetEnemy == null)
        {
            if(targetEnemy != null)targetEnemy.isHold = false;
            me.ActionTable.Blitz_Finish = false;
            me.ActionTable.SetCurAction((int)Enums.PlayerActions.None);
        }
    }
    public override void ActionFixedUpdate()
    {

    }

    public override void ActionExit()
    {
        me.GetAniCtrl.ResetTrigger("Skill_2");
        me.GetAniCtrl.SetBool("DoubleBlitz", false);
        me.ActionTable.Blitz_Finish = false;
        me.MoveCtrl.CanMove = true;
        targetEnemy = null;
    }

    private void NormalBlitz()
    {
        if(targetEnemy != null)
        {
            Vector3 vec =  targetEnemy.transform.position + (me.transform.position - targetEnemy.transform.position).normalized * (targetEnemy.transform.localScale.z * 0.7f);
            vec = new Vector3(vec.x, 0.0f, vec.z);
            me.transform.position = Vector3.Lerp(me.transform.position, vec,me.ActionTable.normalBlitzSpeed);
            me.transform.LookAt(vec);
        }
    }
    private void HardBlitz()
    {
        Debug.Log("샥 돌아서 뒤에서 챠챠챱");
        //몬스터가 있다는 가정하에 몬스터 등 방향으로 휙 돌기
    }

    private Enemy FindTarget()
    {
        float minDistance = float.PositiveInfinity;
        Enemy targetEnemy = null;
        Collider[] nearEnemy = Physics.OverlapSphere(me.transform.position, me.ActionTable.blitzRange);
        foreach(Collider enemy in nearEnemy)
        {
            if(enemy.gameObject.GetComponent<Enemy>() != null)
            {
                float dist = Vector3.Distance(me.transform.position, enemy.transform.position);
                if (dist < minDistance)
                {
                    minDistance = dist;
                    targetEnemy = enemy.gameObject.GetComponent<Enemy>();
                }
            }
        }
        return targetEnemy;
    }
}
