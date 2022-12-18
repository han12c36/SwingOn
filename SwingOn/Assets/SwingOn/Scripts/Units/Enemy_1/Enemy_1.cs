using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Enemy_1Pattern
{
    Spawn,
    Heal,
    MeleeAtt,
    End
}

public class Enemy_1 : Enemy
{
    public Material OriginMaterial;
    public Material DamagedMaterial;
    private float idleWaitTime = 1.5f;
    private float timer;
    [SerializeField]
    private float changeMaterialTimer;
    public bool isCast;
    public float recognizeRange = 3f;
    [SerializeField]
    protected Enemy_1ActionTable actionTable;
    [SerializeField]
    private Enemy_1Weapon enemyWeapon;

    public Enemy_1ActionTable ActionTable { get { return actionTable; } set { actionTable = value; } }
    public Enemy_1Weapon EnemyWeapon { get { return enemyWeapon; } set { enemyWeapon = value; } }

    public float Timer { get { return timer; } set { timer = value; } }
    public float IdleWaitTime { get { return idleWaitTime; } set { idleWaitTime = value; } }

    protected override void Initialize()
    {
        status.unitName = Enums.UnitNameTable.Enemy1;
        if (enemyType == Enums.EnemyType.Normal)
        {
            status.maxHp = 5;
            status.curHp = status.maxHp;
            status.preHp = status.curHp;
            status.AttRange = 1f;
            status.Speed = 3.5f;
            navAgent.speed = status.Speed;
            components.aniCtrl.speed = 1.0f;
        }
        else if(enemyType == Enums.EnemyType.Hard)
        {
            status.maxHp = 10;
            status.curHp = status.maxHp;
            status.preHp = status.curHp;
            status.AttRange = 2f;
            status.Speed = 0.6f;
            navAgent.speed = status.Speed;
            components.aniCtrl.speed = 0.7f;
        }
        actionTable = GetComponent<Enemy_1ActionTable>();
        enemyWeapon = GetComponentInChildren<Enemy_1Weapon>();
        enemyWeapon.Owner = this;
        //enemyWeapon.detectionLayer = LayerMask.NameToLayer("Player");
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

    //public int Think(float[] probs)
    //{
    //    float total = 0;
    //    foreach (float elem in probs) { total += elem; }
    //    float randomPoint = Random.value * total;
    //    for (int i = 0; i < probs.Length; i++)
    //    {
    //        if (randomPoint < probs[i]) return i;
    //        else randomPoint -= probs[i];
    //    }
    //    return probs.Length - 1;
    //}

    public void Enemy_1_Cast() { isCast = true; }
}
