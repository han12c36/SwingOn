using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionTable<T> : MonoBehaviour
{
    [SerializeField]
    protected T owner;
    protected Action<T>[] actions;
    [SerializeField]
    protected Action<T> preAction;
    [SerializeField]
    protected Action<T> curAction;

    [SerializeField]
    private Enums.PlayerActions preAction_e;
    [SerializeField]
    private Enums.PlayerActions curAction_e;


    protected abstract void Initialize();

    protected virtual void Awake()
    {
        Initialize();
    }
    protected virtual void Start() { }
    protected virtual void Update()
    {
        if(curAction != null) curAction.ActionUpdate();
    }
    protected virtual void FixedUpdate()
    {
        if (curAction != null) curAction.ActionFixedUpdate();
    }
    protected virtual void LateUpdate()
    {
        if (curAction != null) curAction.ActionLateUpdate();
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

    
   
}
