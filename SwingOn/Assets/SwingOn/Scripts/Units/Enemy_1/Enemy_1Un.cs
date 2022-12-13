using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1Un : Enemy<Enemy_1Un>
{
    public Structs.Status status;
    public Structs.UnitComponents component;
    public float awakeTime = 3.0f;
    public float timer;
    public GameObject enemy_1_N;

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

    //애도 상태머신 간단하게 만들어서 죽을때 바뀌는걸로 하자

    //PoolingManager.Instance.ReturnObj(gameObject);
    //GameObject obj = PoolingManager.Instance.LentalObj("Enemy_1_N");
    //obj.transform.position = transform.position;

    private void Update()
    {
        base.Update();
        if(timer < awakeTime) timer += Time.deltaTime;
        else
        {
            timer = 0.0f;
            gameObject.SetActive(false);
        }
    }
}
