using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon<T> : MonoBehaviour
{
    [SerializeField]
    protected T owner;
    [SerializeField]
    protected Collider collider;
    
    public int detectionLayer;
    public int dmg;

    public int GetDetectionLayer { get { return detectionLayer; } }
    public T Owner { get { return owner; } set { owner = value; } }

    protected virtual void Awake()
    {
        if (Owner == null) Owner = GetComponentInParent<T>();
        if(Owner != null) collider = GetComponent<Collider>();
    }
    protected virtual void Start()
    {
        if (collider.enabled) OnOffWeaponCollider(false);
    }
    protected virtual void Update()
    {

    }
    protected virtual void FixedUpdate()
    {

    }

    public void OnOffWeaponCollider(bool value)
    {
        collider.enabled = value;
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
    }
}
