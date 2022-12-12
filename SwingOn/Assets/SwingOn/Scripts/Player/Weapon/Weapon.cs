using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon<T> : MonoBehaviour
{
    [SerializeField]
    protected T Owner;
    [SerializeField]
    protected Collider collider;
    [SerializeField]
    protected int detectionLayer;

    public int dmg;

    public int GetDetectionLayer { get { return detectionLayer; } }

    protected virtual void Awake()
    {
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
