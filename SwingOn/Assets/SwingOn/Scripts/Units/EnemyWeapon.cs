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
            detectionLayer = LayerMask.NameToLayer("Player");
        }
    }

    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

    }
}
