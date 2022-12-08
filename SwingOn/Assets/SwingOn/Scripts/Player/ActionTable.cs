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
    private int clickCount;
    [SerializeField]
    private float comboTime = 0.5f;
    [SerializeField]
    private float timer = 0.0f;

    public bool isAttFinish;

    public Vector3 dashstartPos;
    public Vector3 dashtargetPos;
    public float dashSpeed = 0.1f;      //대쉬 속도
    public float DashLength = 5.0f;     //대쉬 길이

    public float Timer { get { return timer; } set { timer = value; }  }
    public int ClickCount { get { return clickCount; } set { clickCount = value; } }
    public float GetComboTime { get { return comboTime; }}


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
        actions[(int)Enums.PlayerActions.Normal] = new NormalAtt();
        actions[(int)Enums.PlayerActions.Skill_1] = new Skill_1();
        actions[(int)Enums.PlayerActions.Skill_2] = new Skill_2();
        actions[(int)Enums.PlayerActions.Skill_3] = new Skill_3();


        SetCurAction((int)Enums.PlayerActions.None);
    }
    private void Start()
    {
    }
    private void Update()
    {
        GetInput();

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
        //있는지 없는지부터 판단
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

    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            owner.GetAniCtrl.SetTrigger("isSwing");

            //SetCurAction((int)Enums.PlayerActions.Normal);
        }
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetCurAction((int)Enums.PlayerActions.Skill_1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetCurAction((int)Enums.PlayerActions.Skill_2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetCurAction((int)Enums.PlayerActions.Skill_3);
        }
    }
    public bool isCurrentAnimationOver(float time)
    {
        return owner.GetAniCtrl.GetCurrentAnimatorStateInfo(0).normalizedTime > time;
    }

    public void ComboCheck(int Comboindex)
    {
        if (Comboindex == 1)
        {
            if (Comboindex == clickCount -1) owner.GetAniCtrl.SetTrigger("goCombo2");
        }
        else if (Comboindex == 2)
        {
            if (Comboindex == clickCount - 1) owner.GetAniCtrl.SetTrigger("goCombo3");
        }
        else if (Comboindex == 3)
        {
            if (Comboindex == clickCount - 1) owner.GetAniCtrl.SetTrigger("goCombo4");
        }
    }
    public void AttFinish() { isAttFinish = true; }
} 
