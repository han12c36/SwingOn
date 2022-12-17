using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : Enemy
{
    public Material OriginMaterial;
    public Material DamagedMaterial;

    [SerializeField]
    protected Enemy_2ActionTable actionTable;

    public Enemy_2ActionTable ActionTable { get { return actionTable; } set { actionTable = value; } }

    protected override void Initialize()
    {
        status.unitName = Enums.UnitNameTable.Enemy2;
        status.maxHp = 5;
        status.curHp = status.maxHp;
        status.preHp = status.curHp;
        status.AttRange = 1.5f;
        status.Speed = 3.5f;
        navAgent.speed = status.Speed;
        components.aniCtrl.speed = 1.0f;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        actionTable.SetCurAction((int)Enums.Enemy_1Actions.Idle);
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    protected override void LateUpdate()
    {
        base.LateUpdate();
    }

    private void OnDrawGizmos()
    {
        Color col = Color.red;
        col.a = 0.3f;
        Gizmos.color = col;
        Gizmos.DrawSphere(transform.position, status.AttRange);
    }
}
