using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_4ActionTable : ActionTable<Enemy_4>
{
    [SerializeField]
    private Enums.Enemy_4Actions preAction_e;
    [SerializeField]
    private Enums.Enemy_4Actions curAction_e;

    public bool isHitFinish;
    public bool isSreamFinish;
    public bool isReadyComplete;

    public float Enemy_4WaitTime = 1.5f;
    //private float[] WalkOrRunOrRange = new float[4] { 70.0f, 30.0f,0.0f, 20.0f };
    private float[] WalkOrRunOrRange = new float[4] { 70.0f, 30.0f, 0.0f, 70.0f };
    private float[] AttPattern = new float[3] { 50.0f, 0.0f, 20.0f };

    protected override void Initialize()
    {
        if (owner == null) owner = GetComponent<Enemy_4>();
        if (owner != null)
        {
            owner.ActionTable = this;
            actions = new Action<Enemy_4>[(int)Enums.Enemy_4Actions.End];
        }
        actions[(int)Enums.Enemy_4Actions.Ready] = new Enemy_4Ready();
        actions[(int)Enums.Enemy_4Actions.Idle] = new Enemy_4Idle();
        actions[(int)Enums.Enemy_4Actions.Walk] = new Enemy_4Walk();
        actions[(int)Enums.Enemy_4Actions.Run] = new Enemy_4Run();
        actions[(int)Enums.Enemy_4Actions.MeleeAtt] = new Enemy_4MeleeAtt();
        actions[(int)Enums.Enemy_4Actions.RangeAtt] = new Enemy_4RangeAtt();
        actions[(int)Enums.Enemy_4Actions.DashAtt] = new Enemy_4DashAtt();
        actions[(int)Enums.Enemy_4Actions.Damaged] = new Enemy_4Damaged();
        actions[(int)Enums.Enemy_4Actions.Death] = new Enemy_4Death();
    }
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        SetCurAction((int)Enums.Enemy_4Actions.Ready);
    }

    protected override void Update()
    {
        base.Update();
        if (owner.isHold) owner.MoveStop();
        if (owner.status.curHp > 0)
        {
            if (owner.hitCount > 0)
            {
                SetCurAction((int)Enums.Enemy_4Actions.Damaged);
            }
        }
        else
        {
            if (curAction_e != Enums.Enemy_4Actions.Death)
            {
                SetCurAction((int)Enums.Enemy_4Actions.Death);
            }
        }
    }

    protected override void FixedUpdate() { base.FixedUpdate(); }
    protected override void LateUpdate() { base.LateUpdate(); }

    public override void SetCurAction(int index)
    {
        preAction_e = (Enums.Enemy_4Actions)preAction_i;
        base.SetCurAction(index);
        curAction_e = (Enums.Enemy_4Actions)curAction_i;
    }
    public bool isCurrentAnimationOver(float time)
    {
        return owner.GetAniCtrl.GetCurrentAnimatorStateInfo(0).normalizedTime > time;
    }
    public int Enemy_4Think()
    {
        //¶Ù±â : 2
        //°È±â : 3
        //¿ø°Å¸® : 5
        //int index = MySTL.Think(WalkOrRunOrRange);
        int index = 3;
        return index + 2;
    }
    public int Enemy_4AttThink()
    {
        //±ÙÁ¢   : 4
        //´ë½¬   : 6
        //int index = MySTL.Think(AttPattern);
        int index = 0;
        return index + 4;
    }


    public void DamagedFinish() { isHitFinish = true; }
    public void ScreamFinish() { isSreamFinish = true; }

    public void ReadyComplete() { isReadyComplete = true; }
    public void OnOffWeaponCollider_Enemy(int value)
    {
        if (value == 0) owner.EnemyWeapon.OnOffWeaponCollider(true);
        else owner.EnemyWeapon.OnOffWeaponCollider(false);
    }

    public void Enemy_4RandomFire()
    {
        int fireCount = Random.Range(3, 6);
        for (int i = 0; i < fireCount; i++)
        {
            Vector3 randVec = MySTL.RandomVec(owner.GetTarget.transform.position, 6.0f);
            Enemy_4Fire(randVec);
        }
        Enemy_4Fire(owner.GetTarget.transform.position);
    }


    public void Enemy_4Fire(Vector3 targetPos)
    {
        GameObject effect = PoolingManager.Instance.LentalObj("Effect_Enemy_4Bullet");
        effect.transform.position = owner.makeBulletEffectPos.transform.position;
        GameObject obj = PoolingManager.Instance.LentalObj("Enemy_4Bullet");
        //effect.transform.position = owner.makeBulletEffectPos.transform.position;
        Enemy_4Bullet bullet = obj.GetComponent<Enemy_4Bullet>();
        bullet.Owner = owner;
        bullet.startPos = owner.makeBulletEffectPos.position;
        //Vector3 vec = new Vector3(owner.GetTarget.transform.position.x, owner.GetTarget.transform.position.y, owner.GetTarget.transform.position.z);
        bullet.endPos = targetPos;
        bullet.transform.LookAt(bullet.endPos);
        Debug.Log("ÃÑ¾Ë »ý¼º");
    }
}
