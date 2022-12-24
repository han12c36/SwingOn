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

    public override void ActionEnter(Player script)
    {
        base.ActionEnter(script);
        Debug.Log("스킬 1 들어옴");
        me.ActionTable.ChangeLayer(me.transform.root, me.ActionTable.ignoreLayer,me.ActionTable.WeaponLayer);
        me.MoveCtrl.CanMove = false;
        me.GetAniCtrl.SetTrigger("Skill_1");

        M = me.components.rigid.mass;       //w / g = M
        dir = me.transform.forward;
    }
    public override void ActionUpdate()
    {
        if (!me.GetAniCtrl.GetBool("DoubleDash"))
        {
            if (me.ActionTable.AttType == Enums.PlayerAttType.Hard)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    me.GetAniCtrl.SetBool("DoubleDash", true);
                }
            }
        }
        if (!me.GetAniCtrl.GetBool("PowerSlash"))
        {
            if (me.ActionTable.AttType == Enums.PlayerAttType.Speed)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    me.GetAniCtrl.SetBool("PowerSlash", true);
                }
            }
        }
        
        if (me.ActionTable.Dash_Finish || me.ActionTable.Power_Slash)
        {
            me.ActionTable.Dash_Finish = false;
            me.ActionTable.Power_Slash = false;
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
        me.GetAniCtrl.ResetTrigger("Skill_1");
        me.GetAniCtrl.SetBool("DoubleDash", false);
        me.GetAniCtrl.SetBool("PowerSlash", false);
        me.ActionTable.Dash_Finish = false;
        me.ActionTable.Power_Slash = false;
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
 