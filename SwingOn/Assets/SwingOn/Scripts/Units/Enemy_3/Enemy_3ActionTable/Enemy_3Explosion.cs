using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_3Explosion : Action<Enemy_3>
{
    float explosionTimer = 0.0f;
    float Enemy_3ExplosionTime = 4f;

    public override void ActionEnter(Enemy_3 script)
    {
        base.ActionEnter(script);
        me.GetAniCtrl.SetBool("isRoll",true);
    }
    public override void ActionUpdate()
    {
        if (!me.isHold)
        {
            if (explosionTimer <= Enemy_3ExplosionTime)
            {
                explosionTimer += Time.deltaTime;
                Vector3 vec = new Vector3(me.GetTarget.transform.position.x, 0.0f, me.GetTarget.transform.position.z);
                me.transform.LookAt(vec);
                me.MoveOrder(me.GetTarget.transform, me.status.Speed * 1.5f);
            }
            else
            {
                me.MoveStop();
                me.GetAniCtrl.SetBool("isRoll", false);
                me.GetAniCtrl.SetTrigger("isExplosion");
            }
        }
    }
    public override void ActionExit()
    {
        explosionTimer = 0.0f;
        me.GetAniCtrl.SetBool("isRoll", false);
        me.GetAniCtrl.ResetTrigger("isExplosion");
    }
}
