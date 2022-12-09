using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance = null;

    public Structs.Status status;
    [Space(5f)]
    public Structs.UnitComponents components;
    [Space(5f)]
    [SerializeField]
    private ActionTable actionTable;
    [Space(5f)]
    [SerializeField]
    private MoveCtrl moveCtrl;

    public MoveCtrl MoveCtrl { get { return moveCtrl; } set { moveCtrl = value; } }
    public ActionTable SetActionTable { set { actionTable = value; } }
    public ActionTable GetActionTable { get { return actionTable; } }
    public Animator GetAniCtrl { get { return components.aniCtrl; } }
    public Rigidbody GetRigid { get { return components.rigid; } }


    private void Initialize()
    {
        status.unitName = Enums.UnitNameTable.Player;
        status.maxHp = 100;
        status.curHp = status.maxHp;
        components.rigid = GetComponent<Rigidbody>();
        components.collider = GetComponent<Collider>();
        components.aniCtrl = GetComponent<Animator>();
        moveCtrl = GetComponent<MoveCtrl>();
    }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);

        if(instance != null) Initialize();
        if (status.unitName == Enums.UnitNameTable.None) Debug.LogError("UnitName is None");
    }

    private void Start()
    {
        
    }
    private void Update()
    {
        UpdateAnimation();
    }
    private void FixedUpdate()
    {
        
    }

    private void UpdateAnimation()
    {
        components.aniCtrl.SetBool("isMove",moveCtrl.IsMove);
        components.aniCtrl.SetBool("isRun",moveCtrl.IsRun);
        components.aniCtrl.SetFloat("X", moveCtrl.X);
        components.aniCtrl.SetFloat("Z", moveCtrl.Z);
    }

    private void OnDrawGizmosSelected()
    {
        //Color col = Color.black;
        //Debug.DrawRay(transform.position, transform.forward, col, 10f);
    }
}
