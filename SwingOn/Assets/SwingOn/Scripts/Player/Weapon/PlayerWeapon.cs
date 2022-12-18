using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : Weapon<Player>
{
    protected override void Awake()
    {
        base.Awake();
        if (Owner != null)
        {
            Owner.PlayerWeapon = this;
            detectionLayer = LayerMask.NameToLayer("Enemy");
        }
    }
    protected override void Start()
    {
        base.Start();
        if (collider.enabled) OnOffWeaponCollider(false);
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
