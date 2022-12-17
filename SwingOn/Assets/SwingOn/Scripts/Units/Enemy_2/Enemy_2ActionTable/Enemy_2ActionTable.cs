using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2ActionTable : ActionTable<Enemy_2>
{
    [SerializeField]
    private Enums.Enemy_2Actions preAction_e;
    [SerializeField]
    private Enums.Enemy_2Actions curAction_e;

    public bool isHitFinish;
    public bool isReadyComplete;

    public float Enemy_2WaitTime = 1.5f;

    protected override void Initialize()
    {
        if (owner == null) owner = GetComponent<Enemy_2>();
        if (owner != null)
        {
            owner.ActionTable = this;
            actions = new Action<Enemy_2>[(int)Enums.Enemy_2Actions.End];
        }
        actions[(int)Enums.Enemy_2Actions.Ready] = new Enemy_2Ready();
        actions[(int)Enums.Enemy_2Actions.Idle] = new Enemy_2Idle();
        actions[(int)Enums.Enemy_2Actions.Trace] = new Enemy_2Trace();
        actions[(int)Enums.Enemy_2Actions.MeleeAtt] = new Enemy_2MeleeAtt();
        actions[(int)Enums.Enemy_2Actions.LongAtt] = new Enemy_2LongAtt();
        actions[(int)Enums.Enemy_2Actions.RangeAtt] = new Enemy_2RangeAtt();
        actions[(int)Enums.Enemy_2Actions.Damaged] = new Enemy_2Damaged();
        actions[(int)Enums.Enemy_2Actions.Death] = new Enemy_2Death();
    }
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        SetCurAction((int)Enums.Enemy_2Actions.Ready);
    }

    protected override void Update()
    {
        base.Update();
        if (owner.isHold) owner.MoveStop();
        if (owner.status.curHp > 0)
        {
            if (owner.hitCount > 0)
            {
                SetCurAction((int)Enums.Enemy_2Actions.Damaged);
            }
        }
        else
        {
            if (curAction_e != Enums.Enemy_2Actions.Death)
            {
                SetCurAction((int)Enums.Enemy_2Actions.Death);
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
        preAction_e = (Enums.Enemy_2Actions)preAction_i;
        base.SetCurAction(index);
        curAction_e = (Enums.Enemy_2Actions)curAction_i;
    }
    public bool isCurrentAnimationOver(float time)
    {
        return owner.GetAniCtrl.GetCurrentAnimatorStateInfo(0).normalizedTime > time;
    }

    public int Enemy_2Think()
    {
        //3 4 5
        return 4;
    }

    public void ReadyComplete() { isReadyComplete = true; }
    public void HitFinish() { isHitFinish = true; }
    public void MakeBullet() 
    {
        GameObject bullet =  PoolingManager.Instance.LentalObj("Enemy_2Bullet");
        bullet.transform.position = owner.makeBulletPos.position;
    }
}
