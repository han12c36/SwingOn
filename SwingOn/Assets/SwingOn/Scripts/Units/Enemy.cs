using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy<T> : MonoBehaviour
{
    public Enums.EnemyType enemyType;
    public Structs.Status status;
    [Space(5f)]
    public Structs.UnitComponents components;
    [Space(5f)]
    [SerializeField]
    protected Player target;
    [SerializeField]
    protected float distToTarget;
    [Space(5f)]
    [SerializeField]
    protected ActionTable<T> actionTable;

    public ActionTable<T> ActionTable { get { return actionTable; } set { actionTable = value; } }
    protected abstract void Initialize();

    protected virtual void Awake()
    {
        components.rigid = GetComponent<Rigidbody>();
        components.collider = GetComponent<Collider>();
        components.aniCtrl = GetComponent<Animator>();
        Initialize();
    }
    protected virtual void OnEnable() { }
    protected virtual void OnDisable() { }

    protected virtual void Start() 
    {
        target = InGameManager.Instance.GetPlayer;
    }

    protected virtual void Update() 
    {
        if(target) distToTarget = Vector3.Distance(target.transform.position, transform.position);
    }
    protected virtual void FixedUpdate() { }
    protected virtual void LateUpdate() { }
}
