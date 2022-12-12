using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy<T> : MonoBehaviour
{
    public Enums.EnemyType enemyType;
    [Space(5f)]
    public Structs.Status status;
    [Space(5f)]
    public Structs.UnitComponents components;
    [SerializeField]
    protected float distToTarget;
    [SerializeField]
    protected Player target;
    [SerializeField]
    protected ActionTable<T> actionTable;
    [SerializeField]
    protected NavMeshAgent navAgent;
    [SerializeField]
    private int detectionLayer;
    [SerializeField]
    private EnemyWeapon<T> enemyWeapon;


    public ActionTable<T> ActionTable { get { return actionTable; } set { actionTable = value; } }
    public Player GetTarget { get { return target; } }
    public Animator GetAniCtrl { get { return components.aniCtrl; } }
    public EnemyWeapon<T> EnemyWeapon { get { return enemyWeapon; } set { enemyWeapon = value; } }

    public float GetDistToTarget { get { return distToTarget; } }

    protected abstract void Initialize();

    protected virtual void Awake()
    {
        components.rigid = GetComponent<Rigidbody>();
        components.collider = GetComponent<Collider>();
        components.aniCtrl = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        detectionLayer = LayerMask.NameToLayer("PlayerWeapon");
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
        status.preHp = status.curHp;
    }
    protected virtual void FixedUpdate() { }
    protected virtual void LateUpdate() { }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == detectionLayer)
        {
            status.curHp -= target.PlayerWeapon.dmg;
        }
    }

    public void MoveOrder(Transform targetPos,float MoveSpeed)
    {
        if (navAgent == null) return;
        if (navAgent.isStopped) navAgent.isStopped = false;
        navAgent.speed = MoveSpeed;
        navAgent.SetDestination(targetPos.position);
    }

    public void MoveStop()
    {
        if (navAgent == null) return;
        if (!navAgent.isStopped) navAgent.isStopped = true;
        navAgent.SetDestination(transform.position);
    }
    public bool isCurrentAnimationOver(float time)
    {
        return GetAniCtrl.GetCurrentAnimatorStateInfo(0).normalizedTime > time;
    }

    public IEnumerator ChangeMaterial(Material OriginMaterial, Material DamagedMaterial, float durtaionTime)
    {
        GetComponentInChildren<SkinnedMeshRenderer>().material = DamagedMaterial;
        yield return new WaitForSeconds(durtaionTime);
        GetComponentInChildren<SkinnedMeshRenderer>().material = OriginMaterial;

    }
}
