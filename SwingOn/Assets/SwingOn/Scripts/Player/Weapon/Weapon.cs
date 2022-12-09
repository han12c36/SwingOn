using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private Player Owner;
    [SerializeField]
    private Collider collider;
    [SerializeField]
    private int detectionLayer;

    private void Awake()
    {
        if(Owner == null) Owner = GetComponentInParent<Player>();
        if (Owner != null)
        {
            Owner.PlayerWeapon = this;
            collider = GetComponent<Collider>();
            detectionLayer = LayerMask.NameToLayer("Enemy");
        }
        if (collider.enabled) OnOffWeaponCollider(false);
    }
    private void Start()
    {

    }
    private void Update()
    {

    }

    public void OnOffWeaponCollider(bool value)
    {
        collider.enabled = value;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == detectionLayer)
        {
            Debug.Log("적이 한대 맞음!!");

        }
    }
}
