using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCollision : MonoBehaviour
{
    int detectionLayer;
    // Start is called before the first frame update
    void Start()
    {
        detectionLayer = LayerMask.NameToLayer("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnParticleCollision(GameObject other)
    {
        //Debug.Log("파티클 충돌");
    }

    private void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.layer == 8)
        //{
        //    Debug.Log("파티클 충돌");
        //    other.GetComponent<Enemy>().status.curHp--;
        //}
    }

}
