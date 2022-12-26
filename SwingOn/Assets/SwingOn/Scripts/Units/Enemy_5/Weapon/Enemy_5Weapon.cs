using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_5Weapon : EnemyWeapon
{
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        if (collider.enabled) OnOffWeaponCollider(false);
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
