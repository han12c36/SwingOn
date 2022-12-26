using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_4 : Enemy
{
    public Material OriginMaterial;
    public Material DamagedMaterial;

    public Transform makeBulletEffectPos;

    private float idleWaitTime = 1.5f;
    private float timer;
    [SerializeField]
    private float changeMaterialTimer;
    [SerializeField]
    protected Enemy_4ActionTable actionTable;
    [SerializeField]
    private Enemy_4Weapon enemyWeapon;

    public Enemy_4ActionTable ActionTable { get { return actionTable; } set { actionTable = value; } }
    public Enemy_4Weapon EnemyWeapon { get { return enemyWeapon; } set { enemyWeapon = value; } }

    public float Timer { get { return timer; } set { timer = value; } }
    public float IdleWaitTime { get { return idleWaitTime; } set { idleWaitTime = value; } }

    protected override void Initialize()
    {
        status.unitName = Enums.UnitNameTable.Enemy3;
        status.maxHp = 5;
        status.curHp = status.maxHp;
        status.preHp = status.curHp;
        status.AttRange = 1.7f;
        status.Speed = 1.5f;
        navAgent.speed = status.Speed;
        components.aniCtrl.speed = 1.0f;
        actionTable = GetComponent<Enemy_4ActionTable>();
        enemyWeapon = GetComponentInChildren<Enemy_4Weapon>();
        enemyWeapon.Owner = this;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        actionTable.SetCurAction((int)Enums.Enemy_4Actions.Ready);
    }

    protected override void Start() { base.Start(); }

    protected override void Update() { base.Update(); }
    protected override void FixedUpdate() { base.FixedUpdate(); }
    protected override void LateUpdate() { base.LateUpdate(); }
}
