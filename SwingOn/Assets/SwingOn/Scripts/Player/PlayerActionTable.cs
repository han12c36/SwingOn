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
    private bool isBlitzFinish;
    [SerializeField]
    private bool modeChange;

    public bool isFindNearEnemy;

    private float animationSpeed = 1.0f;
    public float blitzRange = 10.0f;
    
    [SerializeField]
    private Enums.PlayerAttType attType;
    [SerializeField]
    private float modeDurtaionTimer;
    public float hardModeDurationTime = 10.0f;
    [Header("NormalDash")]
    public float normalDashSpeed = 0.1f;        //�븻 �뽬 �ӵ�
    public float normalDashDistance = 5.0f;     //�븻 �뽬 ����
    [Header("HardDash")]
    public float hardDashSpeed = 0.2f;          //�ϵ� �뽬 �ӵ�
    public float hardDashDistance = 3.0f;       //�ϵ� �뽬 ����
    [Header("NormalBlitz")]
    public float normalBlitzSpeed = 0.2f;          //��� �ӵ�
    //public float hardDashDistance = 3.0f;       //�ϵ� �뽬 ����

    public bool Att_Finish { get { return isAttFinish; } set { isAttFinish = value; } }
    public bool Equipt_Finish { get { return isEquptFinish; } set { isEquptFinish = value; } }
    public bool Dash_Finish { get { return isDashFinish; } set { isDashFinish = value; } }
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
        actions[(int)Enums.PlayerActions.HardAtt] = new HardAtt();
        actions[(int)Enums.PlayerActions.Skill_1] = new Skill_1();
        actions[(int)Enums.PlayerActions.Skill_2] = new Skill_2();
        actions[(int)Enums.PlayerActions.Skill_3] = new Skill_3();
        
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
    }
    protected override void Update()
    {
        base.Update();

        Mode_Change();
        ComboAtt();
        DashAtt();
        BlitzAtt();
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
                SetCurAction((int)Enums.PlayerActions.Skill_3);
            }
        }
        else if (attType == Enums.PlayerAttType.Hard)
        {
            if (modeDurtaionTimer < hardModeDurationTime)
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
    public void AttFinish() { if (!modeChange) isAttFinish = true; }
    public void EquiptFinish() { isEquptFinish = true; }
    public void DashFinish() { if (!modeChange) isDashFinish = true; }
    public void BlitzFinish() { if (!modeChange) isBlitzFinish = true; }
    public void FindNearEnemy() { if(!isFindNearEnemy) isFindNearEnemy = true; }

    public void OnOffWeaponCollider(int value)
    {
        if (value == 0) owner.PlayerWeapon.OnOffWeaponCollider(true);
        else owner.PlayerWeapon.OnOffWeaponCollider(false);
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
}
