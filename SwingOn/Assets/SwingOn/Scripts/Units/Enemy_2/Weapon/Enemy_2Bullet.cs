using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2Bullet : EnemyWeapon
{
    public float bulletSpeed = 0.01f;
    public Player target;

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        target = InGameManager.Instance.GetPlayer;
    }
    protected virtual void Update()
    {
        base.Update();
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, bulletSpeed);
        //transform.position += Vector3.up * 0.5f;
    }
    protected virtual void FixedUpdate()
    {
        base.FixedUpdate();
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == detectionLayer)
        {
            PoolingManager.Instance.ReturnObj(gameObject);
        }
    }
}
