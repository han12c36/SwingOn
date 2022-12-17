using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2Bullet : EnemyWeapon
{
    public float bulletSpeed = 0.05f;
    public Transform target;
    public float upTime = 0.3f;
    public float timer;
    public Rigidbody rigid;
    public Vector3 targetPos;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        timer = 0.0f;
    }

    protected override void Awake()
    {
        base.Awake();
        rigid = GetComponentInChildren<Rigidbody>();
    }
    protected override void Start()
    {
        base.Start();
        target = InGameManager.Instance.GetPlayer.transform;
        targetPos = new Vector3(target.position.x, target.position.y + 0.5f, target.position.z);
    }
    protected override void Update()
    {
        base.Update();
        //transform.position += targetPos.normalized * 0.02f;
        //if (timer < upTime)
        //{
        //    timer += Time.deltaTime;
        //    transform.position += Vector3.up * 0.02f;
        //}
        //else
        //{
        //    timer = 0.0f;
        //}
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        rigid.AddForce(targetPos * 0.3f, ForceMode.Impulse);
        if (timer < upTime)
        {
            timer += Time.deltaTime;
            rigid.AddForce(Vector3.up * 0.01f, ForceMode.Impulse);
        }
        else
        {
            timer = 0.0f;
        }

    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(collider);
        Debug.Log("부딪침!");
        //if (other.gameObject.layer == detectionLayer)
        //{
        //    PoolingManager.Instance.ReturnObj(gameObject);
        //}
    }

    public void SetVelocuty(Vector3 velocity)
    {
        rigid.velocity = velocity;
    }
    public void SetForce(Vector3 force)
    {
        rigid.AddForce(force, ForceMode.Impulse);
    }
    public Vector3 GetVelocity(Vector3 curPos,Vector3 targetPos,float initialAngle)
    {
        float gravity = Physics.gravity.magnitude;  //중력값 들고오기
        float angle = initialAngle * Mathf.Deg2Rad; //초기 각도를 라디안으로 바꾸기

        //평면 타겟위치
        Vector3 planarTargetPos = new Vector3(targetPos.x, 0.0f, targetPos.z);
        Vector3 planarCurPos = new Vector3(curPos.x, 0.0f, curPos.z);

        float distance = Vector3.Distance(planarTargetPos, planarCurPos);   //평면상의 현재위치에서 목표점까지의 거리
        float yOffset = curPos.y = targetPos.y;

        float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

        Vector3 velocity = new Vector3(0f, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));
        float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarTargetPos - planarCurPos) * (targetPos.x > curPos.x ? 1 : -1);
        Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

        return finalVelocity;
    }
}
