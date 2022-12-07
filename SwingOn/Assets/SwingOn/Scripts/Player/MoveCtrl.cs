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

    private float init_Speed;
    private float x;
    private float z;
    private float animation_X;
    private float animation_Z;
    private float animationBlendSpeed = 0.1f;

    private Vector3 preDir;
    public Vector3 curDir;
    private float AccelerationValue = 3.0f;
    private float RunPercent = 0.8f;

    public float X { get { return animation_X; } }
    public float Z { get { return animation_Z; } }
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
        z = Input.GetAxisRaw("Vertical");

        if (x > 0.0f)
        {
            if (animation_X < 1.0f) animation_X += Time.deltaTime;
            else animation_X = 1.0f;
        }
        if(x < 0.0f)
        {
            if (animation_X > -1.0f) animation_X -= Time.deltaTime;
            else animation_X = -1.0f;
        }
        
        if (z > 0.0f)
        {
            if (animation_Z < 1.0f) animation_Z += Time.deltaTime;
            else animation_Z = 1.0f;
        }
        if(z < 0.0f)
        {
            if (animation_Z > -1.0f) animation_Z -= Time.deltaTime;
            else animation_Z = -1.0f;
        }

        if(x == 0.0f)
        {
            if(animation_X > 0.0f)
            {
                animation_X -= Time.deltaTime;
                if (animation_X <= 0.0f) animation_X = 0.0f;
            }
            else if(animation_X < 0.0f)
            {
                animation_X += Time.deltaTime;
                if (animation_X >= 0.0f) animation_X = 0.0f;
            }
            else
            {
                animation_X = 0.0f;
            }
        }
        if(z == 0.0f)
        {
            if (animation_Z > 0.0f)
            {
                animation_Z -= Time.deltaTime;
                if (animation_Z <= 0.0f) animation_Z = 0.0f;
            }
            else if (animation_Z < 0.0f)
            {
                animation_Z += Time.deltaTime;
                if (animation_Z >= 0.0f) animation_Z = 0.0f;
            }
            else
            {
                animation_Z = 0.0f;
            }
        }
    }

    private void CheckMovement()
    {
        if (x == 0.0f && z == 0.0f) isMove = false;
        else isMove = true;

        if (isMove)
        {
            if (cur_Speed >= max_Speed * RunPercent) isRun = true;
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

        //animation_X = (Vector3.Lerp(preDir, curDir, Time.deltaTime * animationBlendSpeed)).normalized.x;
        //animation_Z = (Vector3.Lerp(preDir, curDir, Time.deltaTime * animationBlendSpeed)).normalized.z;

        if (-preDir == curDir)
        {
            //����
            cur_Speed = init_Speed;
        }
        else
        {
            //����
            if (cur_Speed < max_Speed) cur_Speed += Time.deltaTime * AccelerationValue;
            else cur_Speed = max_Speed;
        }

        transform.position += curDir * cur_Speed * Time.deltaTime;
    }
}
