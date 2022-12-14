using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : Weapon<Enemy>
{
    protected override void Awake()
    {
        base.Awake();
        if (owner != null)
        {
            //owner = this;
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
