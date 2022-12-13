using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1UnActionTable : ActionTable<Enemy_1Un>
{
    [SerializeField]
    private Enums.Enemy_1EggActions preAction_e;
    [SerializeField]
    private Enums.Enemy_1EggActions curAction_e;

    protected override void Initialize()
    {
        if (owner == null) owner = GetComponent<Enemy_1Un>();
        if (owner != null)
        {
            owner.ActionTable = this;
            actions = new Action<Enemy_1Un>[(int)Enums.Enemy_1EggActions.End];
        }

        actions[(int)Enums.Enemy_1EggActions.Idle] = new Enemy_1UnIdle();
        actions[(int)Enums.Enemy_1EggActions.Damaged] = new Enemy_1UnDamaged();
        actions[(int)Enums.Enemy_1EggActions.Death] = new Enemy_1UnDeath();
    }

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();

        SetCurAction((int)Enums.Enemy_1EggActions.Idle);
    }
    protected override void Update()
    {
        base.Update();
        if(owner.status.curHp <= 0 || owner.timer >= owner.awakeTime)
        {
            if (curAction_e != Enums.Enemy_1EggActions.Death)
            {
                SetCurAction((int)Enums.Enemy_1EggActions.Death);
            }

        }
        if (owner.status.curHp > 0)
        {
            if (owner.status.preHp > owner.status.curHp)
            {
                SetCurAction((int)Enums.Enemy_1EggActions.Damaged);
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
        preAction_e = (Enums.Enemy_1EggActions)preAction_i;
        base.SetCurAction(index);
        curAction_e = (Enums.Enemy_1EggActions)curAction_i;
    }
}
