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
    private PlayerActionTable actionTable;
    [Space(5f)]
    [SerializeField]
    private MoveCtrl moveCtrl;
    [SerializeField]
    private PlayerWeapon player_Weapon;
    [SerializeField]
    private int detectionLayer;

    public Transform backEffect_Pos;
    public Transform chargeEffect_Pos;
    public Transform GroundEffect_Pos;
    public Collider dashCollder;

    public MoveCtrl MoveCtrl { get { return moveCtrl; } set { moveCtrl = value; } }
    public PlayerWeapon PlayerWeapon { get { return player_Weapon; } set { player_Weapon = value; } }
    public PlayerActionTable ActionTable { get { return actionTable; } set { actionTable = value; } }
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
        detectionLayer = LayerMask.NameToLayer("EnemyWeapon");
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
        if(Input.GetKeyDown(KeyCode.O))
        {
            GameObject obj = PoolingManager.Instance.LentalObj("Enemy_1_N");
            obj.transform.position = new Vector3(0.0f, 2.0f, 0.0f);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameObject obj = PoolingManager.Instance.LentalObj("Enemy_1_H");
            obj.transform.position = new Vector3(0.0f, 2.0f, 0.0f);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            GameObject obj = PoolingManager.Instance.LentalObj("Enemy_1_Un");
            obj.transform.position = new Vector3(0.0f, 2.0f, 0.0f);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            GameObject obj = PoolingManager.Instance.LentalObj("Enemy_2");
            obj.transform.position = new Vector3(0.0f, 2.0f, 0.0f);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameObject obj = PoolingManager.Instance.LentalObj("Enemy_2Bullet");
            obj.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        }
    }
    private void FixedUpdate()
    {
        
    }

    private void UpdateAnimation()
    {
        components.aniCtrl.SetBool("isMove",moveCtrl.IsMove);
        components.aniCtrl.SetBool("isRun",moveCtrl.IsRun);
        components.aniCtrl.SetFloat("MoveValue", (moveCtrl.cur_Speed) / moveCtrl.max_Speed);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == detectionLayer)
        {

            EnemyWeapon enemyWeapon = other.GetComponent<EnemyWeapon>();
            status.curHp -= enemyWeapon.dmg;

            //status.curHp -= other.GetComponent<EnemyWeapon>().dmg;
        }
    }
}
