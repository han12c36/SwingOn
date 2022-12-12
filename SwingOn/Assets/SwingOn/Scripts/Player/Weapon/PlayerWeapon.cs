using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : Weapon<Player>
{
    protected override void Awake()
    {
        base.Awake();
        if(Owner == null) Owner = GetComponentInParent<Player>();
        if (Owner != null)
        {
            Owner.PlayerWeapon = this;
            collider = GetComponent<Collider>();
            detectionLayer = LayerMask.NameToLayer("Enemy");
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
