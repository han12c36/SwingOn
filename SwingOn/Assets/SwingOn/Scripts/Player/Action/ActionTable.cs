using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTable : MonoBehaviour
{
    public Player owner;
    private Action[] actions;
    private Action preAction;
    private Action curAction;

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
    private float animationSpeed = 1.0f;

    [SerializeField]
    private Enums.PlayerAttType attType;
    [SerializeField]
    private bool modeChange;

    public float hardModeDurationTime = 10.0f;
    [SerializeField]
    private float modeDurtaionTimer;

    public Vector3 dashstartPos;
    public Vector3 dashtargetPos;
    public float dashSpeed = 0.1f;      //�뽬 �ӵ�
    public float DashLength = 5.0f;     //�뽬 ����

    public bool ModeChange { get { return modeChange; } set { modeChange = value; } }
    public bool Att_Finish { get { return isAttFinish; } set { isAttFinish = value; } }
    public bool Equipt_Finish { get { return isEquptFinish; } set { isEquptFinish = value; } }
    public bool Dash_Finish { get { return isDashFinish; } set { isDashFinish = value; } }

    public float AnimationSpeed { get { return animationSpeed; } set { animationSpeed = value; } }
    public float ModeDurtaionTimer { get { return modeDurtaionTimer; } set { modeDurtaionTimer = value; } }

    public Enums.PlayerAttType AttType { get { return attType; } set { attType = value; } }

    private void Initialize()
    {
        if(owner == null) owner = GetComponent<Player>();
        if (owner != null)
        {
            owner.SetActionTable = this;
            actions = new Action[(int)Enums.PlayerActions.End];
        }
    }
    private void Awake()
    {
        Initialize();
        actions[(int)Enums.PlayerActions.None] = new None();
        actions[(int)Enums.PlayerActions.NormalAtt] = new NormalAtt();
        actions[(int)Enums.PlayerActions.HardAtt] = new HardAtt();
        actions[(int)Enums.PlayerActions.Skill_1] = new Skill_1();
        actions[(int)Enums.PlayerActions.Skill_2] = new Skill_2();
        actions[(int)Enums.PlayerActions.Skill_3] = new Skill_3();

        attType = Enums.PlayerAttType.Normal;
    }
    private void Start()
    {
        SetCurAction((int)Enums.PlayerActions.None);
        animationSpeed = 1.0f;
    }
    private void Update()
    {
        Mode_Change();

        if (Att_Finish) Debug.Log("�����ٰ� ����");
        if (curAction != null) curAction.ActionUpdate();

    }
    private void FixedUpdate()
    {
        if (curAction != null) curAction.ActionFixedUpdate();
    }
    private void LateUpdate()
    {
        if (curAction != null) curAction.ActionLateUpdate();
    }
    private void SetCurAction(Action action)
    {
        int index = System.Array.IndexOf(actions, action);
        if (index < 0) return;

        curAction.ActionExit();
        preAction = curAction;
        actions[index].ActionEnter(owner);
        curAction = actions[index];
    }

    public void SetCurAction(int index)
    {
        //�ִ��� ���������� �Ǵ�
        int nextAction = -1;
        nextAction = System.Array.IndexOf(actions, actions[index]);
        if (nextAction < 0) return;

        if (curAction != null)
        {
            //if (curAction == actions[index]) return;
            curAction.ActionExit();
            preAction = curAction;
            preAction_e = (Enums.PlayerActions)System.Array.IndexOf(actions, curAction);
        }

        curAction = actions[nextAction];
        curAction_e = (Enums.PlayerActions)nextAction;

        curAction.ActionEnter(owner);
    }

    private void Mode_Change()
    {
        if (attType == Enums.PlayerAttType.Hard)
        {
            if (modeDurtaionTimer < hardModeDurationTime &&
                curAction != actions[(int)Enums.PlayerActions.Skill_3]) modeDurtaionTimer += Time.deltaTime;
            else
            {
                modeDurtaionTimer = 0.0f;
                ModeChange = true;
            }
        }
        else if (AttType == Enums.PlayerAttType.Normal)
        {
            if (Input.GetKeyDown(KeyCode.Alpha3)) ModeChange = true;
        }
    }
   
    public bool isCurrentAnimationOver(float time)
    {
        return owner.GetAniCtrl.GetCurrentAnimatorStateInfo(0).normalizedTime > time;
    }
    
    public void AttFinish() { isAttFinish = true; }
    public void EquiptFinish() { isEquptFinish = true; }
    public void DashFinish() { isDashFinish = true; }

}