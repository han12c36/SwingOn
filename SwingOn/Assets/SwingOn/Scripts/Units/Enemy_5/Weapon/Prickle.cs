using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prickle : MonoBehaviour
{

    public float lifeTime = 5.0f;
    private float timer;

    void Start()
    {
        
    }

    void Update()
    {
        if (timer < lifeTime)
        {
            timer += Time.deltaTime;
            if (transform.position.y > -1.0f)
            {
                Vector3 vec = new Vector3(transform.position.x, transform.position.y - (1.0f * Time.deltaTime), transform.position.z);
                transform.position = vec;
            }
            else
            {
                Vector3 vec = new Vector3(transform.position.x, transform.position.y + (2.0f * Time.deltaTime), transform.position.z);
                transform.position = vec;
            }
        }
        else
        {
            timer = 0.0f;
            PoolingManager.Instance.ReturnObj(gameObject);
        }
    }
}
