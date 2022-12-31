using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_1 : Action<Player>
{

    float timer;
    float M;
    Vector3 F;
    Vector3 A;
    Vector3 V0;
    Vector3 dir;

    float speed = 1.0f;

    public override void ActionEnter(Player script)
    {
        base.ActionEnter(script);
        me.ActionTable.ChangeLayer(me.transform.root, me.ActionTable.ignoreLayer,me.ActionTable.WeaponLayer);
        me.MoveCtrl.CanMove = false;

        if (me.ActionTable.AttType == Enums.PlayerAttType.Normal)
        {
            me.GetAniCtrl.SetTrigger("Dash");
        }
        else if (me.ActionTable.AttType == Enums.PlayerAttType.Speed)
        {
            me.GetAniCtrl.SetTrigger("Blitz");
        }
        else if (me.ActionTable.AttType == Enums.PlayerAttType.Hard)
        {
            me.GetAniCtrl.SetTrigger("GroundBreak");
        }

        M = me.components.rigid.mass;       //w / g = M
        dir = me.transform.forward;
    }
    public override void ActionUpdate()
    {
        //if (!me.GetAniCtrl.GetBool("DoubleDash"))
        //{
        //    if (me.ActionTable.AttType == Enums.PlayerAttType.Hard)
        //    {
        //        if (Input.GetKeyDown(KeyCode.Alpha1))
        //        {
        //            me.GetAniCtrl.SetBool("DoubleDash", true);
        //        }
        //    }
        //}
        //if (!me.GetAniCtrl.GetBool("PowerSlash"))
        //{
        //    if (me.ActionTable.AttType == Enums.PlayerAttType.Speed)
        //    {
        //        if (Input.GetKeyDown(KeyCode.Alpha1))
        //        {
        //            me.GetAniCtrl.SetBool("PowerSlash", true);
        //        }
        //    }
        //}

        if (me.ActionTable.Dash_Finish)
        {
            me.ActionTable.Dash_Finish = false;
            me.GetAniCtrl.SetBool("Skill_1", false);
            me.ActionTable.SetCurAction((int)Enums.PlayerActions.None);
        }
        if (me.ActionTable.Blitz_Finish)
        {
            me.ActionTable.targetEnemy.isHold = false;
            me.ActionTable.targetEnemy = null;
            me.ActionTable.Blitz_Finish = false;
            me.GetAniCtrl.SetBool("Skill_1", false);
            me.ActionTable.SetCurAction((int)Enums.PlayerActions.None);
        }
        if (me.ActionTable.GroundBreak_Finish)
        {
            me.ActionTable.GroundBreak_Finish = false;
            me.GetAniCtrl.SetBool("Skill_1", false);
            me.ActionTable.SetCurAction((int)Enums.PlayerActions.None);
        }
    }
    public override void ActionFixedUpdate()
    {
        if (me.GetAniCtrl.GetCurrentAnimatorStateInfo(0).IsName("NormalDash"))
        {
            if (!me.ActionTable.isCurrentAnimationOver(0.4f)) NormalDash(); //0.645
            else
            {
                me.components.rigid.velocity = Vector3.zero;
                timer = 0.0f;
            }
        }
        if (me.GetAniCtrl.GetCurrentAnimatorStateInfo(0).IsName("HardDash"))
        {
            if (!me.ActionTable.isCurrentAnimationOver(0.18f)) HardDash(); // 0.18
            else
            {
                me.components.rigid.velocity = Vector3.zero;
                timer = 0.0f;
            }
        }
    }

    public override void ActionExit()
    {
        me.GetAniCtrl.SetBool("Skill_1", false);
        me.GetAniCtrl.ResetTrigger("Dash");
        me.GetAniCtrl.ResetTrigger("Blitz");
        me.GetAniCtrl.ResetTrigger("GroundBreak");
        me.ActionTable.Dash_Finish = false;
        me.ActionTable.Blitz_Finish = false;
        me.ActionTable.GroundBreak_Finish = false;
        me.MoveCtrl.CanMove = true;
        me.ActionTable.ChangeLayer(me.transform.root, me.ActionTable.originLayer, me.ActionTable.WeaponLayer);
        timer = 0.0f;
        F = Vector3.zero;
        A = Vector3.zero;
        V0 = Vector3.zero;
        dir = Vector3.zero;
    }

    private void NormalDash()
    {
        Debug.Log("일반대쉬");
        if (timer <= 1.0f) timer += Time.fixedDeltaTime;
        //Power = me.ActionTable.hardDashPower;
        float normalPower = 7.0f;
        A = dir * normalPower;
        float decelerateValue = normalPower - timer * normalPower;
        if (decelerateValue > normalPower * 0.93f)
        {
            A *= decelerateValue;
            F = M * A;
            V0 = F;
            me.components.rigid.velocity = V0 + A * timer;
        }
    }

    private void HardDash()
    {
        Debug.Log("하드대쉬");
        if (timer <= 1.0f) timer += Time.fixedDeltaTime;
        float HardPower = 10.0f;
        A = dir * HardPower;
        float decelerateValue = HardPower - timer * HardPower;
        if (decelerateValue > HardPower * 0.93f)
        {
            A *= decelerateValue;
            F = M * A;
            V0 = F;
            me.components.rigid.velocity = V0 + A * timer;
        }
    }
}

//if (!me.GetAniCtrl.GetBool("DoubleBlitz"))
//{
//    if (me.ActionTable.AttType == Enums.PlayerAttType.Speed)
//    {
//        if (Input.GetKeyDown(KeyCode.Alpha2))
//        {
//            if (me.ActionTable.targetEnemy != null)
//            {
//                me.ActionTable.targetEnemy.isHold = true;
//                Vector3 vec = me.ActionTable.targetEnemy.transform.position + (-me.ActionTable.targetEnemy.transform.forward).normalized * //(me.ActionTable.targetEnemy.transform.localScale.z * 0.8f);
//                me.transform.position = vec;
//                me.transform.LookAt(me.transform.position + me.ActionTable.targetEnemy.transform.forward);
//                me.GetAniCtrl.SetBool("DoubleBlitz", true);
//            }
//        }
//    }
//}

//if (me.GetAniCtrl.GetCurrentAnimatorStateInfo(0).IsName("NormalBlitz"))
//{
//    if (!me.ActionTable.isCurrentAnimationOver(0.62f))
//    {
//        NormalBlitz();
//    }
//}

//private void NormalBlitz()
//{
//    if (me.ActionTable.targetEnemy != null)
//    {
//        Vector3 vec = me.ActionTable.targetEnemy.transform.position + (me.transform.position - //me.ActionTable.targetEnemy.transform.position).normalized * (me.ActionTable.targetEnemy.transform.localScale.z * 0.8f);
//        me.transform.position = Vector3.Lerp(me.transform.position, vec, me.ActionTable.normalBlitzSpeed);
//    }
//}