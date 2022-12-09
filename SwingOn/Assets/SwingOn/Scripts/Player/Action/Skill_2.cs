using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_2 : Action
{
    public override void ActionEnter(Player script)
    {
        base.ActionEnter(script);
        me.MoveCtrl.CanMove = false;
        me.GetAniCtrl.SetTrigger("Skill_2");
    }
    public override void ActionUpdate()
    {
        if (me.GetAniCtrl.GetCurrentAnimatorStateInfo(0).IsName("NormalBlitz"))
        {
            if (!me.GetActionTable.isCurrentAnimationOver(0.62f)) NormalBlitz();
        }

        if (!me.GetAniCtrl.GetBool("DoubleBlitz"))
        {
            if (me.GetActionTable.AttType == Enums.PlayerAttType.Hard)
            {
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    me.GetAniCtrl.SetBool("DoubleBlitz", true);
                }
            }
        }
        if (me.GetAniCtrl.GetCurrentAnimatorStateInfo(0).IsName("HardBlitz"))
        {
            //if (!me.GetActionTable.isCurrentAnimationOver(0.62f)) 
            HardBlitz();
        }

        if (me.GetActionTable.Blitz_Finish)
        {
            me.GetActionTable.Blitz_Finish = false;
            me.GetActionTable.SetCurAction((int)Enums.PlayerActions.None);
        }
    }
    public override void ActionFixedUpdate()
    {

    }

    public override void ActionExit()
    {
        me.GetAniCtrl.ResetTrigger("Skill_2");
        me.GetAniCtrl.SetBool("DoubleBlitz", false);
        me.MoveCtrl.CanMove = true;
    }

    private void NormalBlitz()
    {
        Debug.Log("Àü¹æÂî¸£±â!");
    }
    private void HardBlitz()
    {
        Debug.Log("¼¡ µ¹¾Æ¼­ µÚ¿¡¼­ Ã­Ã­ªy");
    }

}
