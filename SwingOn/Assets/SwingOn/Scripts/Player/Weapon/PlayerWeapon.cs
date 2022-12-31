using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerWeaponType
{
    Melee,
    Effect,
    End
}


public class PlayerWeapon : Weapon<Player>
{
    public PlayerWeaponType weaponType;

    protected override void Awake()
    {
        base.Awake();
        if (Owner != null)
        {
            Owner.PlayerWeapon = this;
            detectionLayer = LayerMask.NameToLayer("Enemy");
        }
        if (Owner == null) Owner = InGameManager.Instance.GetPlayer;
    }
    protected override void Start()
    {
        base.Start();
        if(weaponType == PlayerWeaponType.Melee)
        {
            if (collider.enabled) OnOffWeaponCollider(false);
        }
        else
        {
            if (!collider.enabled) OnOffWeaponCollider(true);
        }
    }

    protected override void Update()
    {
        base.Update();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
