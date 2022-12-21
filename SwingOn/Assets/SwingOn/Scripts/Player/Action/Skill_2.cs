using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_2 : Action<Player>
{
    //Enemy targetEnemy;
    public override void ActionEnter(Player script)
    {
        base.ActionEnter(script);

        me.ActionTable.ChangeLayer(me.transform.root, me.ActionTable.ignoreLayer, me.ActionTable.WeaponLayer);
        me.MoveCtrl.CanMove = false;

        if (me.ActionTable.targetEnemy == null)
        {
            me.GetAniCtrl.SetTrigger("GroundBreak");
        }
        else
        {
            me.GetAniCtrl.SetTrigger("Skill_2");
            me.transform.LookAt(me.ActionTable.targetEnemy.transform.position);
        }
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
            if (me.ActionTable.AttType == Enums.PlayerAttType.Speed)
            {
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    if (me.ActionTable.targetEnemy != null)
                    {
                        me.ActionTable.targetEnemy.isHold = true;
                        Vector3 vec = me.ActionTable.targetEnemy.transform.position + (-me.ActionTable.targetEnemy.transform.forward).normalized * (me.ActionTable.targetEnemy.transform.localScale.z * 0.8f);
                        me.transform.position = vec;
                        me.transform.LookAt(me.transform.position + me.ActionTable.targetEnemy.transform.forward);
                        me.GetAniCtrl.SetBool("DoubleBlitz", true);
                    }
                }
            }
        }

        if (me.ActionTable.AttType == Enums.PlayerAttType.Hard)
        {
            if (me.ActionTable.GroundBreak_Finish)
            {
                me.ActionTable.GroundBreak_Finish = false;
                me.ActionTable.SetCurAction((int)Enums.PlayerActions.None);
            }
        }
        if (me.ActionTable.AttType == Enums.PlayerAttType.Normal || me.ActionTable.AttType == Enums.PlayerAttType.Speed)
        {
            if(me.ActionTable.Blitz_Finish || me.ActionTable.targetEnemy.status.curHp < 0 )//|| me.ActionTable.targetEnemy == null)
            {
                me.ActionTable.targetEnemy.isHold = false;
                me.ActionTable.targetEnemy = null;
                me.ActionTable.Blitz_Finish = false;
                me.ActionTable.SetCurAction((int)Enums.PlayerActions.None);
            }
        }
    }
    public override void ActionFixedUpdate()
    {

    }

    public override void ActionExit()
    {
        me.GetAniCtrl.ResetTrigger("Skill_2");
        me.GetAniCtrl.ResetTrigger("GroundBreak");
        me.GetAniCtrl.SetBool("DoubleBlitz", false);
        me.ActionTable.Blitz_Finish = false;
        me.ActionTable.GroundBreak_Finish = false;
        me.MoveCtrl.CanMove = true;
        me.ActionTable.GroundBreak_Finish = false;
        me.ActionTable.Blitz_Finish = false;

        if (me.ActionTable.targetEnemy != null)
        {
            me.ActionTable.targetEnemy.isHold = false;
            me.ActionTable.targetEnemy = null;
        }
        me.ActionTable.ChangeLayer(me.transform.root, me.ActionTable.originLayer, me.ActionTable.WeaponLayer);
    }

    private void NormalBlitz()
    {
        if(me.ActionTable.targetEnemy != null)
        {
            Vector3 vec =  me.ActionTable.targetEnemy.transform.position + (me.transform.position - me.ActionTable.targetEnemy.transform.position).normalized * (me.ActionTable.targetEnemy.transform.localScale.z * 0.8f);
            me.transform.position = Vector3.Lerp(me.transform.position, vec,me.ActionTable.normalBlitzSpeed);
        }
    }
}
