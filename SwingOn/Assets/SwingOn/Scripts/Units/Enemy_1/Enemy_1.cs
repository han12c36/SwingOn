using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : Enemy<Enemy_1>
{
    public float idleWaitTime = 1.5f;

    [SerializeField]
    private float timer;

    public float Timer { get { return timer; } set { timer = value; } }

    protected override void Initialize()
    {
        status.unitName = Enums.UnitNameTable.Enemy1;
        status.maxHp = 100;
        status.curHp = 100;
        status.AttRange = 2.0f;
        actionTable = GetComponent<ActionTable<Enemy_1>>();
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
