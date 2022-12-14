using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1Un : Enemy
{
    public Material OriginMaterial;
    public Material DamagedMaterial;
    public float awakeTime = 6.0f;
    public float timer;
    protected Enemy_1UnActionTable actionTable;
    //private EnemyWeapon<Enemy_1Un> enemyWeapon;

    public Enemy_1UnActionTable ActionTable { get { return actionTable; } set { actionTable = value; } }
    //public EnemyWeapon<Enemy_1Un> EnemyWeapon { get { return enemyWeapon; } set { enemyWeapon = value; } }
    protected override void Initialize()
    {
        status.unitName = Enums.UnitNameTable.Enemy1;
        status.maxHp = 5;
        status.curHp = status.maxHp;
        status.preHp = status.curHp;
        status.AttRange = 0f;
        status.Speed = 0f;
        //navAgent.speed = status.Speed;
        components.aniCtrl.speed = 1f;
        actionTable = GetComponent<Enemy_1UnActionTable>();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        actionTable.SetCurAction((int)Enums.Enemy_1EggActions.Idle);
    }
}
