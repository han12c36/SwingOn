using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCtrl : MonoBehaviour
{
    [Header("Ctrl_Owner")]
    private Player ctrl_Owner;

    [Header("Speed")]
    public float max_Speed;
    public float cur_Speed;

    [Space(10f)]
    [Header("Bool")]
    [SerializeField]
    private bool isMove;
    [SerializeField]
    private bool isRun;
    [SerializeField]
    private bool isCurFront;
    private bool isPreFront;

    private float init_Speed;
    private float x;
    private float z;
    private Vector3 preDir;
    public Vector3 curDir;
    private float AccelerationValue = 3.0f;
    private float RunPercent = 0.8f;

    public float X { get { return x; } }
    public float Z { get { return z; } }
    public bool IsMove { get { return isMove; } }
    public bool IsRun { get { return isRun; } }



    void Start()
    {
        ctrl_Owner = GetComponent<Player>();
        if (ctrl_Owner) ctrl_Owner.MoveController = this;
        init_Speed = cur_Speed;
    }

    void Update()
    {
        GetInput();
        CheckMovement();
        Move();
    }

    private void GetInput()
    {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical"); ;
    }

    private void CheckMovement()
    {
        if (x == 0.0f && z == 0.0f) isMove = false;
        else isMove = true;

        if(isMove)
        {
            if (cur_Speed >= max_Speed * RunPercent) isRun = true;
            else isRun = false;
        }
    }

    private void Move()
    {
        if (x == 0.0f && z == 0.0f)
        {
            cur_Speed = init_Speed;
            return;
        }

        preDir = curDir;

        curDir = new Vector3(x, 0, z);
        curDir = curDir.normalized;

        isPreFront = isCurFront;

        if (curDir.z == 0.0f) { }
        else if(curDir.z > 0.0f) isCurFront = true;
        else isCurFront = false;

        //앞 뒤로만 구분 할꺼고 
        if(isPreFront == isCurFront)
        {
            //가속
            if (cur_Speed < max_Speed) cur_Speed += Time.deltaTime * AccelerationValue;
            else cur_Speed = max_Speed;
        }
        else
        {
            //속도 초기화
            cur_Speed = init_Speed;
        }

        transform.position += curDir * cur_Speed * Time.deltaTime;
    }
}
