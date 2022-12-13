using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1Un : Enemy<Enemy_1Un>
{
    public Material OriginMaterial;
    public Material DamagedMaterial;
    public float awakeTime = 6.0f;
    public float timer;

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
        actionTable = GetComponent<ActionTable<Enemy_1Un>>();
    }
}
