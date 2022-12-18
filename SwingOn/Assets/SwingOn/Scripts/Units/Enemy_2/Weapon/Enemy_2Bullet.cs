using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2Bullet : EnemyWeapon
{
    //public Transform targetPos;
    public Vector3 targetvec;

    public Vector3 startPos;
    public Vector3 endPos;
    public float g;
    public float v0;
    public float R;
    public float t;
    public float theta;
    public float MaxHeight;
    public float time = 0.0f;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        time = 0.0f;
    }

    protected override void Awake()
    {
        base.Awake();
        detectionLayer = 1 << LayerMask.NameToLayer("Environment");
    }
    protected override void Start()
    {
        base.Start();
        if (!collider.enabled) OnOffWeaponCollider(true);

        startPos = transform.position;
        endPos = targetvec;
        //endPos = targetPos.transform.position;
        g = Physics.gravity.magnitude;
        R = Vector3.Distance(endPos, startPos);
        v0 = Mathf.Sqrt(g * R + 0.1f);
        float thetaValue = Random.Range(1, 10) / 100f;
        theta = Mathf.Asin(((g * R) / Mathf.Pow(v0,2))) / (1 + thetaValue);
    }
    protected override void Update()
    {
        base.Update();
        time += Time.deltaTime;
        float xValue = (endPos - startPos).normalized.x * (v0 * 0.5f) * time;
        float yValue = (v0 * Mathf.Sin(theta) * time) - (0.5f * g * time * time);
        float zValue = (endPos - startPos).normalized.z * (v0 * 0.5f) * time;
        Vector3 vec = new Vector3(xValue, yValue, zValue);
        transform.position = vec;
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(collider);
        Debug.Log("ºÎµúÄ§!");
        if (other.gameObject.layer == detectionLayer)
        {
            PoolingManager.Instance.ReturnObj(gameObject);
        }
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        Debug.Log("ºÎµúÄ§!");
        if (collision.gameObject.layer == detectionLayer)
        {
            PoolingManager.Instance.ReturnObj(gameObject);
        }
    }
}
