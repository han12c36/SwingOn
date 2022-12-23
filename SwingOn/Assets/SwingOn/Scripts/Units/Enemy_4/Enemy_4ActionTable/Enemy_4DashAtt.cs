using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_4DashAtt : Action<Enemy_4>
{
    Vector3 vec;
    float timer;
    public override void ActionEnter(Enemy_4 script)
    {
        base.ActionEnter(script);
        me.MoveStop();
        me.GetAniCtrl.SetBool("isScream", true);
    }
    public override void ActionUpdate()
    {
        if (me.ActionTable.isSreamFinish)
        {
            me.ActionTable.isSreamFinish = false;
            me.GetAniCtrl.SetBool("isScream", false);
            me.GetAniCtrl.SetTrigger("isDashAtt");
            me.transform.LookAt(me.GetTarget.transform.position);
            vec = me.GetTarget.transform.position;
        }

        if (me.GetAniCtrl.GetCurrentAnimatorStateInfo(0).IsName("DashAtt"))
        {
            if (me.isCurrentAnimationOver(1.0f))
            {
                me.ActionTable.SetCurAction((int)Enums.Enemy_4Actions.Idle);
            }
            else
            {
                timer += Time.deltaTime;
                //me.transform.position = Vector3.Lerp(me.transform.position, vec, timer);
            }
        }
    }
    public override void ActionExit()
    {
        me.GetAniCtrl.ResetTrigger("isDashAtt");
        me.GetAniCtrl.SetBool("isScream", false);
        me.ActionTable.isSreamFinish = false;
    }
}
