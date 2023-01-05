using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon<T> : MonoBehaviour
{
    [SerializeField]
    protected T owner;
    [SerializeField]
    protected Collider collider;

    public List<GameObject> hitObjs;

    public int detectionLayer;
    public int dmg;

    public int GetDetectionLayer { get { return detectionLayer; } }
    public T Owner { get { return owner; } set { owner = value; } }

    protected virtual void Awake()
    {
        if (Owner == null) Owner = GetComponentInParent<T>();
        if(Owner != null) collider = GetComponent<Collider>();
        if(collider == null)
        {
            collider = GetComponentInChildren<Collider>();
        }
        hitObjs = new List<GameObject>();
    }
    protected virtual void Start()
    {

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
        hitObjs.Clear();
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        Debug.Log("맞았!!!!!!!!!!!" + other.gameObject.name);
        if (hitObjs.Find(x => x == other.transform.root.gameObject))
        {
            return;
        }

        //맞는놈이 플레이어일때
        if (other.transform.root.gameObject.GetComponent<Player>() != null)
        {
            Player player = other.transform.root.gameObject.GetComponent<Player>();
            player.hitCount++;
            player.status.curHp -= dmg;
        }
        else if (other.transform.root.gameObject.GetComponent<Enemy>() != null)
        {
            Enemy enemy = other.transform.root.gameObject.GetComponent<Enemy>();
            enemy.hitCount++;
            enemy.status.curHp -= dmg;
            CreateGauge(enemy);
            PoolingManager.Instance.PlayEffect("Effect_Friction", transform);
            CameraEffect.instance.PlayShake("WeekShake");

        }
    }

    public void CreateGauge(Enemy enemy)
    {
        float value = enemy.transform.localScale.y * 0.5f;
        Vector3 vec = enemy.transform.position;
        vec.y += value;
        GameObject obj = PoolingManager.Instance.LentalObj("Gauge");
        obj.transform.position = vec;
        obj.GetComponent<Gauge>().startPos = vec;
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {

    }
}
