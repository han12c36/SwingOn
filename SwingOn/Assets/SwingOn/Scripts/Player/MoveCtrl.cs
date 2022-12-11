using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCtrl : MonoBehaviour
{
    [SerializeField]
    private bool canMove = true;
    [Header("Ctrl_Owner")]
    private Player ctrl_Owner;

    [Header("Speed")]
    public float max_Speed;
    public float cur_Speed;
    public float max_BackSpeed;

    private bool isMove;
    private bool isRun;

    private float init_Speed;
    private float x;
    private float z;
    private float animation_X;
    private float animation_Z;

    private Vector3 preDir;
    private Vector3 curDir;
    private float AccelerationValue = 3.0f;
    private float RunPercent = 0.8f;

    public bool CanMove { get { return canMove; } set{ canMove = value; } }
    public float X { get { return animation_X; } }
    public float Z { get { return animation_Z; } }
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

    void Update()
    {
        if (!canMove) return;
        GetInput();
        UpdateAnimationBlendValue();
        CheckMovement();
        Move();
        Rotate();
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

        if (-preDir == curDir)
        {
            //감속
            cur_Speed = init_Speed;
        }
        else
        {
            //가속
            if (curDir.z >= 0.0f)
            {
                if (cur_Speed < max_Speed) cur_Speed += Time.deltaTime * AccelerationValue;
                else cur_Speed = max_Speed;
            }
            else
            {
                if (cur_Speed < max_BackSpeed) cur_Speed += Time.deltaTime * AccelerationValue;
                else cur_Speed = max_BackSpeed;
            }
        }

        transform.position += curDir * cur_Speed * Time.deltaTime;
    }

    private void Rotate()
    {
        //좌우 키를 누르면 회전과 이동이 같이 되게하자.
        //캐릭터 회전은 여기서
        //카메라 회전은 카메라쪽에서

    }


    private float ToMax(float value,float max)
    {
        if (value < max) value += Time.deltaTime;
        else value = max;

        return value;
    }
    private float ToMin(float value,float min)
    {
        if (value > min) value -= Time.deltaTime;
        else value = min;
        return value;
    }

    private void UpdateAnimationBlendValue()
    {
        if (x > 0.0f) animation_X = ToMax(animation_X, 1.0f);
        if (x < 0.0f) animation_X = ToMin(animation_X, -1.0f);
        if (z > 0.0f) animation_Z = ToMax(animation_Z, 1.0f);
        if (z < 0.0f) animation_Z = ToMin(animation_Z, -1.0f);
        if (x == 0.0f)
        {
            if (animation_X > 0.0f) animation_X = ToMin(animation_X, 0.0f);
            else if (animation_X < 0.0f) animation_X = ToMax(animation_X, 0.0f);
            else animation_X = 0.0f;
        }
        if (z == 0.0f)
        {
            if (animation_Z > 0.0f) animation_Z = ToMin(animation_Z, 0.0f);
            else if (animation_Z < 0.0f) animation_Z = ToMax(animation_Z, 0.0f);
            else animation_Z = 0.0f;
        }
    }
}
