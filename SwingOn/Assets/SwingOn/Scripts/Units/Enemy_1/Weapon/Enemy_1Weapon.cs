using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1Weapon : Weapon<Enemy_1>
{
    protected override void Awake()
    {
        base.Awake();
        if (Owner != null)
        {
            Owner.Enemy_1Weapon = this;
            detectionLayer = LayerMask.NameToLayer("Player");
        }
    }
    protected override void Start()
    {
        base.Start();
    }
    protected virtual void Update()
    {
        base.Update();
    }
    protected virtual void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
