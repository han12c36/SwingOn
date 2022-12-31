using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardTornado : PlayerWeapon
{
    public float existTime;
    public float timer;
    public Vector3 dir;
    public Vector3 initPos;
    public float speed;
    public float rotationSpeed;
    public float radius;
    public float initAngle;

    private void OnEnable()
    {
    }
    private void OnDisable()
    {
        dir = Vector3.zero;
        timer = 0.0f;
    }
    protected override void Awake()
    {
        base.Awake();
        speed = 3.0f;
        rotationSpeed = 80.0f;
        existTime = GetComponentInChildren<ParticleSystem>().main.duration;
        detectionLayer = LayerMask.NameToLayer("Enemy");
    }
    protected override void Start()
    {
        base.Start();
        dir = (transform.position - owner.transform.position).normalized;
        initPos = transform.position;
    }
    protected override void Update()
    {
        base.Update();
        if (timer < existTime) timer += Time.deltaTime;
        else
        {
            timer = 0.0f;
            PoolingManager.Instance.ReturnObj(gameObject.transform.root.gameObject);
        }
    }

    //어딘가 써먹자 이거 잘만든건데...
    //private void Rotation()
    //{
    //    float angle = initAngle + timer * rotationSpeed;
    //    angle *= Mathf.Deg2Rad;
    //    float x = (radius + timer * speed) * Mathf.Sin(angle);
    //    float z = (radius + timer * speed) * Mathf.Cos(angle);
    //    transform.position = Owner.transform.position + new Vector3(x, 0.1f, z);
    //}
}
