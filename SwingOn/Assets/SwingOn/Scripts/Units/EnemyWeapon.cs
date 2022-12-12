using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon<T> : Weapon<Enemy<T>>
{
    protected override void Awake()
    {
        base.Awake();
        if (Owner != null)
        {
            Owner.EnemyWeapon = this;
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
