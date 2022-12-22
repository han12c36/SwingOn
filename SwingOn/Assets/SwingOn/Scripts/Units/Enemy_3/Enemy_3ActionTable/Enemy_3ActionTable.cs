using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_3ActionTable : ActionTable<Enemy_3>
{
    [SerializeField]
    private Enums.Enemy_3Actions preAction_e;
    [SerializeField]
    private Enums.Enemy_3Actions curAction_e;

    public bool isHitFinish;
    public bool isReadyComplete;

    public float Enemy_3WaitTime = 1.5f;
    private float[] TraceOrRoll = new float[3] { 50.0f, 0.0f, 50.0f};

    protected override void Initialize()
    {
        if (owner == null) owner = GetComponent<Enemy_3>();
        if (owner != null)
        {
            owner.ActionTable = this;
            actions = new Action<Enemy_3>[(int)Enums.Enemy_3Actions.End];
        }
        actions[(int)Enums.Enemy_3Actions.Ready] = new Enemy_3Ready();
        actions[(int)Enums.Enemy_3Actions.Idle] = new Enemy_3Idle();
        actions[(int)Enums.Enemy_3Actions.Trace] = new Enemy_3Trace();
        actions[(int)Enums.Enemy_3Actions.MeleeAtt] = new Enemy_3MeleeAtt();
        actions[(int)Enums.Enemy_3Actions.Explosion] = new Enemy_3Explosion();
        actions[(int)Enums.Enemy_3Actions.Damaged] = new Enemy_3Damaged();
        actions[(int)Enums.Enemy_3Actions.Death] = new Enemy_3Death();
    }
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        SetCurAction((int)Enums.Enemy_3Actions.Ready);
    }

    protected override void Update()
    {
        base.Update();
        if (owner.isHold) owner.MoveStop();
        if (owner.status.curHp > 0)
        {
            if (owner.hitCount > 0)
            {
                SetCurAction((int)Enums.Enemy_3Actions.Damaged);
            }
        }
        else
        {
            if (curAction_e != Enums.Enemy_3Actions.Death)
            {
                SetCurAction((int)Enums.Enemy_3Actions.Death);
            }
        }
    }

    protected override void FixedUpdate() { base.FixedUpdate(); }
    protected override void LateUpdate() { base.LateUpdate(); }

    public override void SetCurAction(int index)
    {
        preAction_e = (Enums.Enemy_3Actions)preAction_i;
        base.SetCurAction(index);
        curAction_e = (Enums.Enemy_3Actions)curAction_i;
    }
    public bool isCurrentAnimationOver(float time)
    {
        return owner.GetAniCtrl.GetCurrentAnimatorStateInfo(0).normalizedTime > time;
    }
    public int Enemy_3Think()
    {
        //2 4
        //int index = MySTL.Think(TraceOrRoll);
        //int index = 0;
        int index = 2;
        return index + 2;
    }

    public void DamagedFinish() { isHitFinish = true; }
    public void ReadyComplete() { isReadyComplete = true; }
    public void OnOffWeaponCollider_Enemy(int value)
    {
        if (value == 0) owner.EnemyWeapon.OnOffWeaponCollider(true);
        else owner.EnemyWeapon.OnOffWeaponCollider(false);
    }
    public void Explosion()
    {
        GameObject effect = PoolingManager.Instance.LentalObj("Effect_Enemy_3Explosion");
        effect.transform.position = owner.transform.position;
        PoolingManager.Instance.ReturnObj(owner.gameObject);
    }
}
