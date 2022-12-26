using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_5ActionTable : ActionTable<Enemy_5>
{
    [SerializeField]
    private Enums.Enemy_5Actions preAction_e;
    [SerializeField]
    private Enums.Enemy_5Actions curAction_e;

    public bool isHitFinish;

    protected override void Initialize()
    {
        if (owner == null) owner = GetComponent<Enemy_5>();
        if (owner != null)
        {
            owner.ActionTable = this;
            actions = new Action<Enemy_5>[(int)Enums.Enemy_5Actions.End];
        }

        //actions[(int)Enums.Enemy_5Actions.Idle] = new Enemy_5Idle();
        //actions[(int)Enums.Enemy_5Actions.Trace] = new Enemy_5Trace();
        //actions[(int)Enums.Enemy_5Actions.Att_1] = new Enemy_5Att_1();
        //actions[(int)Enums.Enemy_5Actions.Att_2] = new Enemy_5Att_2();
        //actions[(int)Enums.Enemy_5Actions.Damaged] = new Enemy_5Damaged();
        //actions[(int)Enums.Enemy_5Actions.Death] = new Enemy_5Death();

    }
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        SetCurAction((int)Enums.Enemy_5Actions.Ready);
    }

    protected override void Update()
    {
        base.Update();
        if (owner.isHold) owner.MoveStop();
        if (owner.status.curHp > 0)
        {
            if (owner.hitCount > 0)
            {
                if (curAction_e != Enums.Enemy_5Actions.Death)
                {
                    SetCurAction((int)Enums.Enemy_5Actions.Damaged);
                }
            }
        }
        else
        {
            if (curAction_e != Enums.Enemy_5Actions.Death)
            {
                SetCurAction((int)Enums.Enemy_5Actions.Death);
            }
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    protected override void LateUpdate()
    {
        base.LateUpdate();
    }

    public override void SetCurAction(int index)
    {
        preAction_e = (Enums.Enemy_5Actions)preAction_i;
        base.SetCurAction(index);
        curAction_e = (Enums.Enemy_5Actions)curAction_i;
    }

    //public void OnOffWeaponCollider_Enemy(int value)
    //{
    //    if (value == 0) owner.EnemyWeapon.OnOffWeaponCollider(true);
    //    else owner.EnemyWeapon.OnOffWeaponCollider(false);
    //}


    public void HitFinish() { isHitFinish = true; }
}
