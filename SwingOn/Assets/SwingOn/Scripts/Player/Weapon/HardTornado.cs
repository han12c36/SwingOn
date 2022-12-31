using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardTornado : PlayerWeapon
{
    public float existTime;
    public float timer;
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
    }
}
