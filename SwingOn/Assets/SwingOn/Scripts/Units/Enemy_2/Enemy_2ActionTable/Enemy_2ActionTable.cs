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
    private float[] patternValue = new float[3] { 45f, 10f, 45f };

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
        //int index =  MySTL.Think(patternValue);
        int index = 0;
        return index + 3;
    }

    public void ReadyComplete() { isReadyComplete = true; }
    public void HitFinish() { isHitFinish = true; }
    public void FireBulletToPlayer() { FireBullet(owner.GetTarget.transform.position); }
    public void FireBulletToRandomPos()
    {
        for(int i=  0; i < 3; i++)
        {
            Vector3 randVec = MySTL.RandomVec(owner.GetTarget.transform.position, 3.5f);
            FireBullet(randVec);
        }
    }

    public void FireBullet(Vector3 targetPos)
    {
        GameObject obj = PoolingManager.Instance.LentalObj("Enemy_2Bullet");
        obj.transform.position = owner.makeBulletPos.position;
        Enemy_2Bullet bullet = obj.GetComponent<Enemy_2Bullet>();
        bullet.Owner = owner;
        bullet.startPos = owner.makeBulletPos.position;
        bullet.endPos = targetPos;
    }

    public void OnOffWeaponCollider_Enemy(int value)
    {
        if (value == 0) owner.EnemyWeapon.OnOffWeaponCollider(true);
        else owner.EnemyWeapon.OnOffWeaponCollider(false);
    }
}
