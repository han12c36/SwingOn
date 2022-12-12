using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1ActionTable : ActionTable<Enemy_1>
{
    [SerializeField]
    private Enums.Enemy_1Actions preAction_e;
    [SerializeField]
    private Enums.Enemy_1Actions curAction_e;


    protected override void Initialize()
    {
        if (owner == null) owner = GetComponent<Enemy_1>();
        if (owner != null)
        {
            owner.ActionTable = this;
            actions = new Action<Enemy_1>[(int)Enums.Enemy_1Actions.End];
        }

        actions[(int)Enums.Enemy_1Actions.Idle] = new Enemy_1Idle();
        actions[(int)Enums.Enemy_1Actions.Trace] = new Enemy_1Trace();
        actions[(int)Enums.Enemy_1Actions.Att_1] = new Enemy_1Att_1();
        actions[(int)Enums.Enemy_1Actions.Att_2] = new Enemy_1Att_2();
        actions[(int)Enums.Enemy_1Actions.Damaged] = new Enemy_1Damaged();
        actions[(int)Enums.Enemy_1Actions.Death] = new Enemy_1Death();

    }
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();

        SetCurAction((int)Enums.Enemy_1Actions.Idle);
    }
    protected override void Update()
    {
        base.Update();
        if(owner.status.curHp > 0)
        {
            if (owner.status.preHp > owner.status.curHp)
            {
                SetCurAction((int)Enums.Enemy_1Actions.Damaged);
            }
            else if (owner.GetDistToTarget < owner.status.AttRange)
            {
                SetCurAction((int)Enums.Enemy_1Actions.Att_1);
            }
            else if(owner.GetDistToTarget >= owner.status.AttRange)
            {
                SetCurAction((int)Enums.Enemy_1Actions.Trace);
            }
            else
            {
                SetCurAction((int)Enums.Enemy_1Actions.Idle);
            }
        }
        else
        {
            if(curAction_e != Enums.Enemy_1Actions.Death)
            {
                SetCurAction((int)Enums.Enemy_1Actions.Death);
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
        preAction_e = (Enums.Enemy_1Actions)preAction_i;
        base.SetCurAction(index);
        curAction_e = (Enums.Enemy_1Actions)curAction_i;
    }
    public void OnOffWeaponCollider_Enemy(int value)
    {
        if (value == 0) owner.EnemyWeapon.OnOffWeaponCollider(true);
        else owner.EnemyWeapon.OnOffWeaponCollider(false);
    }
}
