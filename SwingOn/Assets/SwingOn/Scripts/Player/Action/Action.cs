using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action<T>
{
    protected T me;
    //protected ActionTable actionTable;
    public virtual void ActionEnter(T script)
    {
        if(me == null) me = script;
        //if (me != null) actionTable = me.GetActionTable;
    }
    public abstract void ActionUpdate();
    public virtual void ActionFixedUpdate() { }
    public virtual void ActionLateUpdate() { }
    public abstract void ActionExit();

}
