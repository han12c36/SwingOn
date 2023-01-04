using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{
    public ParticleSystem particle;
    public Vector3 randomDIr;
    public Vector3 target;

    public float initSpeed;
    public float curSpeed;
    public float timer;
    public float distance;

    void Start()
    {
        initSpeed = 10.0f;
        particle = GetComponentInChildren<ParticleSystem>();
        particle.Play();
    }

    void Update()
    {
        target = InGameManager.Instance.GetPlayer.transform.position;
        distance = Vector3.Distance(target, transform.position);
        Vector3 vec = new Vector3(target.x, target.y + 0.5f, target.z);
        timer += Time.deltaTime;
        curSpeed = initSpeed * (1.0f + timer);
        transform.position += (vec - transform.position).normalized * curSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.root.gameObject == InGameManager.Instance.GetPlayer.transform.root.gameObject)
        {
            PoolingManager.Instance.ReturnObj(gameObject);
        }
    }
}