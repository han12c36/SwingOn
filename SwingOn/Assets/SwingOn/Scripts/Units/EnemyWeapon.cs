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
        //Debug.Log("어 트리거발동!");

        if (owner.GetComponent<Enemy>() != null)
        {
            if (owner.GetComponent<Enemy>().status.curHp > 0)
            {
                if (other.transform.root.GetComponent<Player>() != null)
                {
                    
                    hitObjs.Add(other.transform.root.gameObject);
                }
            }
        }
       
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }

}
