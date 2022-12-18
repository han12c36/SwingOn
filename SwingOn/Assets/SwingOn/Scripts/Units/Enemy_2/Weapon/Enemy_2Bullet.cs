using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2Bullet : EnemyWeapon
{
    public Transform target;
    public Rigidbody rigid;
    public Vector3 targetPos;

    public Vector3 startPos;
    public Vector3 endPos;
    public float gravity;
    public float moveReach;
    public float initialAngle;
    public float MaxHeight;
    public float initialVelocity;
    public Vector3 HorizontalDir;
    
    public float timer = 0.0f;


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

        //initialVelocity = 1.0f;
        startPos = transform.position;
        endPos = target.transform.position;
        //initialAngle = Mathf.Asin(moveReach * gravity / initialVelocity * initialVelocity) / 2;
        //initialVelocity = initialVelocity * Mathf.Sin(initialAngle * Mathf.Deg2Rad);


        //float InitialSpeed = initialVelocity * Mathf.Sin(initialAngle);

        gravity = Physics.gravity.magnitude;
        initialVelocity = 0.1f;
        HorizontalDir = endPos - startPos;
        MaxHeight =  Mathf.Pow(initialVelocity * Mathf.Sin(initialAngle),2) / (2 * gravity);
        float maxtime = initialVelocity * Mathf.Sin(initialAngle) / gravity;
        moveReach = Vector3.Distance(endPos, startPos);

        initialAngle =  Mathf.Asin((moveReach * gravity) / Mathf.Pow(initialVelocity, 2)) / 2;
        initialAngle = initialAngle * Mathf.Rad2Deg;
        Debug.Log("초기 각도 : " + initialAngle);
        //Debug.Log("최고 높이까지 걸리는 시간 : " + maxtime);
    }
    protected override void Update()
    {
        base.Update();
        if (transform.position.y < MaxHeight)
        {
            timer += Time.deltaTime;
            float y = (initialVelocity * Mathf.Sin(initialAngle) * timer) - (0.5f * gravity * Mathf.Pow(timer, 2));
            //float x = HorizontalDir.x * initialVelocity * timer;
            //float z = HorizontalDir.z * initialVelocity * timer;
            Vector3 vec = new Vector3(0.0f, y, 0.0f);
            transform.position = vec;
        }
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
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
}
