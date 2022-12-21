using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionTable : ActionTable<Player>
{
    [SerializeField]
    private Enums.PlayerActions preAction_e;
    [SerializeField]
    private Enums.PlayerActions curAction_e;

    [SerializeField]
    private bool isAttFinish;
    [SerializeField]
    private bool isEquptFinish;
    [SerializeField]
    private bool isDashFinish;
    [SerializeField]
    private bool isPowerSlash;
    [SerializeField]
    private bool isGroundBreakFinish;
    [SerializeField]
    private bool isBlitzFinish;
    [SerializeField]
    private bool modeChange;

    public bool isHitFinish;
    public bool isFindNearEnemy;
    public int ignoreLayer;
    public int originLayer;
    public int WeaponLayer;

    private float animationSpeed = 1.0f;
    public float blitzRange = 10.0f;

    [SerializeField]
    private Enums.PlayerAttType attType;

    public bool tryChangeHardMode;
    public bool tryChangeSpeedMode;

    [SerializeField]
    private float modeDurtaionTimer;
    public float ModeDurationTime = 10.0f;
    [Header("NormalDash")]
    public float normalDashSpeed = 0.1f;        //노말 대쉬 속도
    public float normalDashDistance = 5.0f;     //노말 대쉬 길이
    [Header("HardDash")]
    public float hardDashSpeed = 0.2f;          //하드 대쉬 속도
    public float hardDashDistance = 3.0f;       //하드 대쉬 길이
    [Header("NormalBlitz")]
    public float normalBlitzSpeed = 0.2f;          //기습 속도

    public bool Att_Finish { get { return isAttFinish; } set { isAttFinish = value; } }
    public bool Equipt_Finish { get { return isEquptFinish; } set { isEquptFinish = value; } }
    public bool Dash_Finish { get { return isDashFinish; } set { isDashFinish = value; } }
    public bool Power_Slash { get { return isPowerSlash; } set { isPowerSlash = value; } }
    public bool GroundBreak_Finish { get { return isGroundBreakFinish; } set { isGroundBreakFinish = value; } }
    public bool Blitz_Finish { get { return isBlitzFinish; } set { isBlitzFinish = value; } }
    public bool ModeChange { get { return modeChange; } set { modeChange = value; } }
    public float AnimationSpeed { get { return animationSpeed; } set { animationSpeed = value; } }
    public float ModeDurtaionTimer { get { return modeDurtaionTimer; } set { modeDurtaionTimer = value; } }

    public Enums.PlayerAttType AttType { get { return attType; } set { attType = value; } }

    protected override void Initialize()
    {
        if (owner == null) owner = GetComponent<Player>();
        if (owner != null)
        {
            owner.ActionTable = this;
            actions = new Action<Player>[(int)Enums.PlayerActions.End];
        }
        
        actions[(int)Enums.PlayerActions.None] = new None();
        actions[(int)Enums.PlayerActions.NormalAtt] = new NormalAtt();
        actions[(int)Enums.PlayerActions.SpeedAtt] = new SpeedAtt();
        actions[(int)Enums.PlayerActions.HardAtt] = new HardAtt();
        actions[(int)Enums.PlayerActions.Skill_1] = new Skill_1();
        actions[(int)Enums.PlayerActions.Skill_2] = new Skill_2();
        actions[(int)Enums.PlayerActions.Skill_3] = new Skill_3();
        actions[(int)Enums.PlayerActions.Damaged] = new Player_Damaged();
        actions[(int)Enums.PlayerActions.Death] = new Player_Death();

        attType = Enums.PlayerAttType.Normal;
    }
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();

        SetCurAction((int)Enums.PlayerActions.None);
        animationSpeed = 1.0f;
        originLayer = LayerMask.NameToLayer("Player");
        ignoreLayer = LayerMask.NameToLayer("Ignore");
        WeaponLayer = LayerMask.NameToLayer("PlayerWeapon");

    }
    protected override void Update()
    {
        base.Update();

        if (owner.status.curHp > 0)
        {
            if (owner.hitCount > 0)
            {
                SetCurAction((int)Enums.PlayerActions.Damaged);
            }
            else
            {
                Mode_Change();
                ComboAtt();
                DashAtt();
                BlitzAtt();
            }
        }
        else
        {
            if (curAction_e != Enums.PlayerActions.Death)
            {
                SetCurAction((int)Enums.PlayerActions.Death);
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


    private void Mode_Change()
    {
        if (curAction == actions[(int)Enums.PlayerActions.Skill_3]) return;
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (curAction == actions[(int)Enums.PlayerActions.Skill_1] ||
               curAction == actions[(int)Enums.PlayerActions.Skill_2]) return;
            if (AttType == Enums.PlayerAttType.Normal)
            {
                modeChange = true;
                tryChangeHardMode = true;
                SetCurAction((int)Enums.PlayerActions.Skill_3);
            }
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (curAction == actions[(int)Enums.PlayerActions.Skill_1] ||
               curAction == actions[(int)Enums.PlayerActions.Skill_2]) return;
            if (AttType == Enums.PlayerAttType.Normal)
            {
                modeChange = true;
                tryChangeSpeedMode = true;
                SetCurAction((int)Enums.PlayerActions.Skill_3);
            }
        }
        else if (attType == Enums.PlayerAttType.Hard || attType == Enums.PlayerAttType.Speed)
        {
            if (modeDurtaionTimer < ModeDurationTime)
            {
                modeDurtaionTimer += Time.deltaTime;
            }
            else
            {
                modeDurtaionTimer = 0.0f;
                modeChange = true;
                SetCurAction((int)Enums.PlayerActions.Skill_3);
            }
        }
    }
    private void ComboAtt()
    {
        if (curAction == actions[(int)Enums.PlayerActions.Skill_1] ||
               curAction == actions[(int)Enums.PlayerActions.Skill_2] ||
               curAction == actions[(int)Enums.PlayerActions.Skill_3]) return;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (AttType == Enums.PlayerAttType.Normal)
            {
                SetCurAction((int)Enums.PlayerActions.NormalAtt);
            }
            else if(AttType == Enums.PlayerAttType.Speed)
            {
                SetCurAction((int)Enums.PlayerActions.SpeedAtt);
            }
            else if (AttType == Enums.PlayerAttType.Hard)
            {
                SetCurAction((int)Enums.PlayerActions.HardAtt);
            }
        }
    }
    private void DashAtt()
    {
        if (curAction == actions[(int)Enums.PlayerActions.Skill_1] ||
                   curAction == actions[(int)Enums.PlayerActions.Skill_2] ||
                   curAction == actions[(int)Enums.PlayerActions.Skill_3]) return;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetCurAction((int)Enums.PlayerActions.Skill_1);
        }
    }
    private void BlitzAtt()
    {
        if (curAction == actions[(int)Enums.PlayerActions.Skill_1] ||
                curAction == actions[(int)Enums.PlayerActions.Skill_2] ||
                curAction == actions[(int)Enums.PlayerActions.Skill_3]) return;
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetCurAction((int)Enums.PlayerActions.Skill_2);
        }
    }

    public void PlayEffect(string effectName) 
    {
        PoolingManager.Instance.PlayEffect(effectName, owner.backEffect_Pos, owner.gameObject);
    }
    public void PlayChargeEffect(string effectName)
    {
        //speed모드 차지모션,발사모션 사용중
        if(effectName != "Effect_GroundBreak")
        {
            PoolingManager.Instance.PlayEffect_DontRotation(effectName, owner.chargeEffect_Pos, owner.gameObject);
        }
        else
        {
            PoolingManager.Instance.PlayEffect_DontRotation(effectName, owner.GroundEffect_Pos, owner.gameObject);
        }
    }

    public void AttFinish() { if (!modeChange) isAttFinish = true; }
    public void EquiptFinish() { isEquptFinish = true; }
    public void DashFinish() { if (!modeChange) isDashFinish = true; }
    public void PowerSlashFinish() { if (!modeChange) isPowerSlash = true; }
    public void GroundBreakFinish() { if (!modeChange) isGroundBreakFinish = true; }
    public void BlitzFinish() { if (!modeChange) isBlitzFinish = true; }
    public void FindNearEnemy() { if(!isFindNearEnemy) isFindNearEnemy = true; }
    public void HitFinish() { isHitFinish = true; }

    public void OnOffWeaponCollider(int value)
    {
        if (value == 0) owner.PlayerWeapon.OnOffWeaponCollider(true);
        else owner.PlayerWeapon.OnOffWeaponCollider(false);
    }
    public void OnOffDashCollider(int value)
    {
        if (value == 0) owner.dashCollder.enabled = true;
        else owner.dashCollder.enabled = false;
    }

    public bool isCurrentAnimationOver(float time)
    {
        return owner.GetAniCtrl.GetCurrentAnimatorStateInfo(0).normalizedTime > time;
    }

    public override void SetCurAction(int index)
    {
        preAction_e = (Enums.PlayerActions)preAction_i;
        base.SetCurAction(index);
        curAction_e = (Enums.PlayerActions)curAction_i;
    }

    public Enemy FindTarget()
    {
        float minDistance = float.PositiveInfinity;
        Enemy targetEnemy = null;
        Collider[] nearEnemy = Physics.OverlapSphere(owner.transform.position, owner.ActionTable.blitzRange);
        foreach (Collider enemy in nearEnemy)
        {
            if (enemy.gameObject.GetComponent<Enemy>() != null)
            {
                float dist = Vector3.Distance(owner.transform.position, enemy.transform.position);
                if (dist < minDistance)
                {
                    minDistance = dist;
                    targetEnemy = enemy.gameObject.GetComponent<Enemy>();
                }
            }
        }
        return targetEnemy;
    }

    public void ChangeLayer(Transform parent,int LayerIndex,int dontChangeLayerIndex)
    {
        if(parent.gameObject.layer != dontChangeLayerIndex)
        {
            parent.gameObject.layer = LayerIndex;
        }
		for (int i = 0; i < parent.childCount; ++i)
        {
            if (parent.childCount != 0)
            {
                ChangeLayer(parent.GetChild(i),LayerIndex, dontChangeLayerIndex);
            }

            if(parent.GetChild(i).gameObject.layer != dontChangeLayerIndex)
            {
                parent.GetChild(i).gameObject.layer = LayerIndex;
            }
        }


    }
}
