using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_2 : Action<Player>
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
            if (!me.ActionTable.isCurrentAnimationOver(0.62f)) NormalBlitz();
        }

        if (!me.GetAniCtrl.GetBool("DoubleBlitz"))
        {
            if (me.ActionTable.AttType == Enums.PlayerAttType.Hard)
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

        if (me.ActionTable.Blitz_Finish)
        {
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
    }

    private void NormalBlitz()
    {
        Debug.Log("전방찌르기!");
        //몬스터가 있다는 가정하에 몬스터 뒤쪽까지 찌르기로 돌진
    }
    private void HardBlitz()
    {
        Debug.Log("샥 돌아서 뒤에서 챠챠챱");
        //몬스터가 있다는 가정하에 몬스터 등 방향으로 휙 돌기
    }

}
