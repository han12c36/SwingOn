using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : Enemy<Enemy_1>
{
    public float idleWaitTime = 1.5f;
    public float moveSpeed = 2f;
    
    [SerializeField]
    private float timer;

    public float Timer { get { return timer; } set { timer = value; } }

    protected override void Initialize()
    {
        status.unitName = Enums.UnitNameTable.Enemy1;
        status.maxHp = 5;
        status.curHp = status.maxHp;
        status.preHp = status.curHp;
        status.AttRange = 1.0f;
        actionTable = GetComponent<ActionTable<Enemy_1>>();
        navAgent.speed = 2.0f;
    }
    protected override void OnEnable() 
    {
        base.OnEnable();
    }
    protected override void OnDisable() 
    {
        base.OnDisable();
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
}
