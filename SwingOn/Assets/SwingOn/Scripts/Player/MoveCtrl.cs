using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCtrl : MonoBehaviour
{
    [SerializeField]
    private bool canMove = true;
    [Header("Ctrl_Owner")]
    [SerializeField]
    private Player ctrl_Owner;

    [Header("Speed")]
    public float max_Speed;
    public float cur_Speed = 0.1f;
    public float max_BackSpeed;

    [Header("Rotation")]
    public float RotationSpeed = 0.1f;
    public float RotationSize;


    private bool isMove;
    private bool isRun;

    private float init_Speed;
    private float x;
    private float z;
    private float rotateSpeed = 12.0f;

    private Vector3 preDir;
    private Vector3 curDir;
    private float AccelerationValue = 3.0f;
    private float RunPercent = 0.8f;

    public bool CanMove { get { return canMove; } set{ canMove = value; } }
    public bool IsMove { get { return isMove; } }
    public bool IsRun { get { return isRun; } }

    private void Awake()
    {
        ctrl_Owner = GetComponent<Player>();
        if (ctrl_Owner) ctrl_Owner.MoveCtrl = this;
    }
    void Start()
    {
        init_Speed = cur_Speed;
    }

    private void Update()
    {
        if (!canMove)
        {
            cur_Speed = init_Speed;
            return;
        }
        GetInput();
        CheckMovement();
    }
    private void FixedUpdate()
    {
        RigidMove();
    }

    private void GetInput()
    {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");
    }

    private void CheckMovement()
    {
        if (x == 0.0f && z == 0.0f) isMove = false;
        else isMove = true;

        if (isMove)
        {
            if (cur_Speed >= max_Speed * RunPercent || cur_Speed >= max_BackSpeed * RunPercent) isRun = true;
            else isRun = false;
        }
        else isRun = false;
    }


    private void Move()
    {
        if (x == 0.0f && z == 0.0f)
        {
            cur_Speed = init_Speed;
            curDir = Vector3.zero;
            return;
        }

        preDir = curDir;
        curDir = new Vector3(x, 0, z);
        curDir = curDir.normalized;

        if (-preDir == curDir) cur_Speed = init_Speed;
        else
        {
            if (cur_Speed < max_Speed) cur_Speed += Time.deltaTime * AccelerationValue;
            else cur_Speed = max_Speed;
        }

        transform.position += curDir * cur_Speed * Time.deltaTime;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(curDir), Time.deltaTime * rotateSpeed);
    }
    private void RigidMove()
    {
        if (x == 0.0f && z == 0.0f)
        {
            cur_Speed = init_Speed;
            curDir = Vector3.zero;
            return;
        }

        preDir = curDir;
        curDir = new Vector3(x, 0, z);
        curDir = curDir.normalized;

        if (-preDir == curDir) cur_Speed = init_Speed;
        else
        {
            if (cur_Speed < max_Speed) cur_Speed += Time.fixedDeltaTime * AccelerationValue;
            else cur_Speed = max_Speed;
        }
        ctrl_Owner.components.rigid.MovePosition(ctrl_Owner.components.rigid.position 
            + curDir * cur_Speed * Time.fixedDeltaTime);
        ctrl_Owner.components.rigid.MoveRotation(Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(curDir), Time.fixedDeltaTime * rotateSpeed));
    }
}
