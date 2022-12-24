using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_4Bullet : EnemyWeapon
{
    public Vector3 startPos;
    public Vector3 endPos;
    public int environmentLayer;

    float x;
    float y;
    float z;
    float g;
    float endTime;
    float maxH;
    float H;
    float endH;
    float time;
    float MaxTime; //최대높이까지 걸리는 시간 

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

        maxH = Random.Range(3.0f,6.0f);
        float rand = Random.Range(0.0f, 2.0f);
        MaxTime = 1 + (rand * 0.1f);
        endH = endPos.y - startPos.y;
        H = maxH - startPos.y;
        g = 2 * H / (MaxTime * MaxTime);
        y = Mathf.Sqrt(2 * g * H);
        float a = g;
        float b = -2 * y;
        float c = endH;
        endTime = (-b + Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);
        x = (endPos.x - startPos.x) / endTime;
        z = (endPos.z - startPos.z) / endTime;
    }
    protected override void Update()
    {
        base.Update();
        time += Time.deltaTime;
        float xValue = startPos.x + x * time;
        float yValue = startPos.y + (y * time) - (0.5f * g * time * time);
        float zValue = startPos.z + z * time;
        Vector3 vec = new Vector3(xValue, yValue, zValue);
        transform.position = vec;
        transform.LookAt(new Vector3(0.0f, (y * time), 0.0f));
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
