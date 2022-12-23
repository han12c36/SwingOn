using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_4Bullet : EnemyWeapon
{
    public Vector3 startPos;
    public Vector3 endPos;
    public Vector3 dir;
    public int environmentLayer;
    public float timer;

    private float g;
    private float v0;
    [SerializeField]
    private float R;
    private float theta;
    private float time = 0.0f;
    private float totalTime;
    private float HorizontalV0;

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        owner = null;
    }

    protected override void Awake()
    {
        base.Awake();
        environmentLayer = LayerMask.NameToLayer("Environment");
        detectionLayer = LayerMask.NameToLayer("Player");
    }
    protected override void Start()
    {
        base.Start();
        if (!collider.enabled) OnOffWeaponCollider(true);
        //startPos = owner.GetComponent<Enemy_4>().makeBulletEffectPos.transform.position;
        dir = (endPos - startPos).normalized;
        g = Physics.gravity.magnitude;
        R = Vector3.Distance(endPos, startPos);
        v0 = Mathf.Sqrt(g * R + 0.1f);
        float thetaValue = Random.Range(1, 10) / 100f;
        theta = Mathf.Asin(((g * R) / Mathf.Pow(v0, 2))) / (1 + thetaValue);
        totalTime = v0 * Mathf.Cos(theta) / g;
        float rand = Random.Range(5, 9);
        rand /= 10.0f;
        HorizontalV0 = v0 * rand;
    }
    protected override void Update()
    {
        base.Update();
        timer += Time.deltaTime;
        Vector3 moveVec = startPos + dir * 5f * timer;
        
        //transform.position = moveVec;
        float yValue = (v0 * Mathf.Sin(theta) * time) - (0.5f * g * time * time);
        Vector3 finalVec = new Vector3(moveVec.x, moveVec.y + yValue, moveVec.z);
        
        transform.position = finalVec;
        transform.eulerAngles = finalVec;

        //float xValue = (endPos - startPos).normalized.x * HorizontalV0 * Time.deltaTime;
        //float yValue = (v0 * Mathf.Sin(theta) * time) - (0.5f * g * time * time);
        //float zValue = (endPos - startPos).normalized.z * HorizontalV0 * Time.deltaTime;
        //Vector3 vec = new Vector3(xValue, 0.0f, zValue);
        //transform.position += vec;
        //Vector3 finalVec = new Vector3(transform.position.x, yValue, transform.position.z);
        //transform.position = finalVec;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(collider);
        Debug.Log(other.gameObject.name);
        if (other.transform.root.gameObject.layer == detectionLayer || other.gameObject.layer == environmentLayer)
        {
            //PoolingManager.Instance.ReturnObj(gameObject);
        }
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }

}
