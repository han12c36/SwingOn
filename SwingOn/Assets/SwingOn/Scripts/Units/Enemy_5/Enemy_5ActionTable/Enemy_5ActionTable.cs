using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_5ActionTable : ActionTable<Enemy_5>
{
    [SerializeField]
    private Enums.Enemy_5Actions preAction_e;
    [SerializeField]
    private Enums.Enemy_5Actions curAction_e;

    private float[] WalkOrRun = new float[3] { 50.0f, 0.0f, 50.0f };
    private float[] RangeOrJump = new float[3] { 50.0f, 0.0f, 50.0f };
    private float[] MeleeOrPrickle = new float[3] { 50.0f, 0.0f, 50.0f };

    public float Enemy_5WaitTime = 1.5f;
    public float thinkRange = 5.0f;
    public float fallAttRange = 8.0f;

    public bool isHitFinish;
    public bool isSreamFinish;
    public bool isReadyComplete;
    protected override void Initialize()
    {
        if (owner == null) owner = GetComponent<Enemy_5>();
        if (owner != null)
        {
            owner.ActionTable = this;
            actions = new Action<Enemy_5>[(int)Enums.Enemy_5Actions.End];
        }

        actions[(int)Enums.Enemy_5Actions.Ready] = new Enemy_5Ready();
        actions[(int)Enums.Enemy_5Actions.Idle] = new Enemy_5Idle();
        actions[(int)Enums.Enemy_5Actions.Walk] = new Enemy_5Walk();
        actions[(int)Enums.Enemy_5Actions.Run] = new Enemy_5Run();
        actions[(int)Enums.Enemy_5Actions.MeleeAtt] = new Enemy_5MeleeAtt();
        actions[(int)Enums.Enemy_5Actions.RangeAtt] = new Enemy_5RangeAtt();
        actions[(int)Enums.Enemy_5Actions.PrickleAtt] = new Enemy_5PrickleAtt();
        actions[(int)Enums.Enemy_5Actions.JumpAtt] = new Enemy_5JumpAtt();
        actions[(int)Enums.Enemy_5Actions.Damaged] = new Enemy_5Damaged();
        actions[(int)Enums.Enemy_5Actions.Death] = new Enemy_5Death();
    }
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        SetCurAction((int)Enums.Enemy_5Actions.Ready);
    }

    protected override void Update()
    {
        base.Update();
        if (owner.isHold) owner.MoveStop();
        if (owner.status.curHp > 0)
        {
            if (owner.hitCount > 0)
            {
                if (curAction_e != Enums.Enemy_5Actions.Death)
                {
                    SetCurAction((int)Enums.Enemy_5Actions.Damaged);
                }
            }
        }
        else
        {
            if (curAction_e != Enums.Enemy_5Actions.Death)
            {
                SetCurAction((int)Enums.Enemy_5Actions.Death);
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
        preAction_e = (Enums.Enemy_5Actions)preAction_i;
        base.SetCurAction(index);
        curAction_e = (Enums.Enemy_5Actions)curAction_i;
    }

    public void OnOffWeaponCollider_Enemy(int value)
    {
        if (value == 0) owner.EnemyWeapon.OnOffWeaponCollider(true);
        else owner.EnemyWeapon.OnOffWeaponCollider(false);
    }

    public void DamagedFinish() { isHitFinish = true; }
    public void ScreamFinish() { isSreamFinish = true; }

    public void ReadyComplete() { isReadyComplete = true; }
    public int Enemy_5Think()
    {
        //°È±â : 2
        //¶Ù±â : 3
        if(owner.status.curHp >= owner.status.maxHp * 0.5f) WalkOrRun = new float[3] { 70.0f, 0.0f, 30.0f };
        else WalkOrRun = new float[3] { 30.0f, 0.0f, 70.0f };
        int index = MySTL.Think(WalkOrRun);
        //int index = 0;
        return index + 2;
    }

    public int Enemy_5WalkThink()
    {
        WalkOrRun = new float[3] { 70.0f, 0.0f, 30.0f };
        int index = MySTL.Think(WalkOrRun);
        return index + 2;
    }
    public int Enemy_5RunThink()
    {
        WalkOrRun = new float[3] { 30.0f, 0.0f, 70.0f };
        int index = MySTL.Think(WalkOrRun);
        return index + 2;
    }


    public int Enemy_5FallAttThink()
    {
        //°È±â : 5
        //¶Ù±â : 7
        //int index = MySTL.Think(RangeOrJump);
        int index = 0;
        return index + 5;
    }
    public int Enemy_5ShortAttThink()
    {
        //°È±â : 4
        //¶Ù±â : 6
        //int index = MySTL.Think(MeleeOrPrickle);
        int index = 0;
        return index + 4;
    }
}
