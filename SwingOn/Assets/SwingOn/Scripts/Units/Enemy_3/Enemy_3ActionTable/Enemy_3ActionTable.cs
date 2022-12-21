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
    //private float[] patternValue = new float[3] { 45f, 10f, 45f };

    protected override void Initialize()
    {
        if (owner == null) owner = GetComponent<Enemy_3>();
        if (owner != null)
        {
            owner.ActionTable = this;
            actions = new Action<Enemy_3>[(int)Enums.Enemy_3Actions.End];
        }
        //actions[(int)Enums.Enemy_2Actions.Ready] = new Enemy_2Ready();
        //actions[(int)Enums.Enemy_2Actions.Idle] = new Enemy_2Idle();
        //actions[(int)Enums.Enemy_2Actions.Trace] = new Enemy_2Trace();
        //actions[(int)Enums.Enemy_2Actions.MeleeAtt] = new Enemy_2MeleeAtt();
        //actions[(int)Enums.Enemy_2Actions.LongAtt] = new Enemy_2LongAtt();
        //actions[(int)Enums.Enemy_2Actions.RangeAtt] = new Enemy_2RangeAtt();
        //actions[(int)Enums.Enemy_2Actions.Damaged] = new Enemy_2Damaged();
        //actions[(int)Enums.Enemy_2Actions.Death] = new Enemy_2Death();
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
}
