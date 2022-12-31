using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : PlayerWeapon
{
    public float speed;
    public float existTime;
    public float timer;
    private Vector3 dir;

    private void OnEnable()
    {
        dir = (Owner.transform.forward).normalized;
    }
    private void OnDisable()
    {
        dir = Vector3.zero;
        timer = 0.0f;
    }
    protected override void Awake()
    {
        base.Awake();
        speed = 8.0f;
        existTime = GetComponentInChildren<ParticleSystem>().main.duration;
        detectionLayer = LayerMask.NameToLayer("Enemy");
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        if (timer < existTime) timer += Time.deltaTime;
        else
        {
            timer = 0.0f;
            PoolingManager.Instance.ReturnObj(gameObject);
        }
        MoveForward();
    }
    private void MoveForward()
    {
        transform.position += dir * speed * Time.deltaTime;
    }
}
