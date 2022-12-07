using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static Player instance;

    [Header("Status")]
    public Structs.Status status;

    [Header("Component")]
    [SerializeField]
    private Rigidbody rigid;
    [SerializeField]
    private Collider collider;
    [SerializeField]
    private Animator aniCtrl;

    [Header("MoveCtrl")]
    [SerializeField]
    private MoveCtrl moveCtrl;

    public MoveCtrl MoveController { set { moveCtrl = value; } }

    private void Initialize()
    {
        status.unitName = Enums.UnitNameTable.Player;
        status.maxHp = 100;
        status.curHp = status.maxHp;
        rigid = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        aniCtrl = GetComponent<Animator>();
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
        aniCtrl.SetBool("isMove",moveCtrl.IsMove);
        aniCtrl.SetBool("isRun",moveCtrl.IsRun);
        aniCtrl.SetFloat("X", moveCtrl.X);
        aniCtrl.SetFloat("Z", moveCtrl.Z);
    }
}
