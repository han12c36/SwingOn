using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2Bullet : EnemyWeapon
{
    //public Vector3 targetvec;
    public bool isReady;
    public bool isStart;

    public Vector3 startPos;
    public Vector3 endPos;
    private float g;
    private float v0;
    private float R;
    private float theta;
    private float time = 0.0f;

    public int environmentLayer;

    public Enemy_2Bullet(Vector3 startPos, Vector3 endPos)
    {
        this.startPos = startPos;
        this.endPos = endPos;
    }

    private void OnEnable()
    {
        //startPos = Owner.GetComponent<Enemy_2>().makeBulletPos.position;
        Debug.Log("활성화");

    }

    private void OnDisable()
    {
        time = 0.0f;
        owner = null;
        isStart = false;
    }

    protected override void Awake()
    {
        base.Awake();
        Debug.Log("어웨이크");
        environmentLayer = LayerMask.NameToLayer("Environment");
    }
    protected override void Start()
    {
        base.Start();
        Debug.Log("스타트");
        if (!collider.enabled) OnOffWeaponCollider(true);
        startPos = owner.GetComponent<Enemy_2>().makeBulletPos.position;
        g = Physics.gravity.magnitude;
        R = Vector3.Distance(endPos, startPos);
        v0 = Mathf.Sqrt(g * R + 0.1f);
        float thetaValue = Random.Range(1, 10) / 100f;
        theta = Mathf.Asin(((g * R) / Mathf.Pow(v0, 2))) / (1 + thetaValue);
    }
    protected override void Update()
    {
        base.Update();
        Debug.Log("업데이트");
        time += Time.deltaTime;
        float xValue = (endPos - startPos).normalized.x * (v0 * 0.5f) * Time.deltaTime;
        float yValue = (v0 * Mathf.Sin(theta) * time) - (0.5f * g * time * time);
        float zValue = (endPos - startPos).normalized.z * (v0 * 0.5f) * Time.deltaTime;
        Vector3 vec = new Vector3(xValue, 0.0f, zValue);
        transform.position += vec;
        Vector3 finalVec = new Vector3(transform.position.x, yValue, transform.position.z);
        transform.position = finalVec;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    new protected void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(collider);
        if (other.gameObject.layer == detectionLayer || other.gameObject.layer == environmentLayer)
        {
            PoolingManager.Instance.ReturnObj(gameObject);
        }
    }

}
