using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1Un : MonoBehaviour
{
    public Structs.Status status;
    public Structs.UnitComponents component;
    public float awakeTime = 3.0f;
    public float timer;
    public GameObject enemy_1_N;

    private void Awake()
    {
        component.aniCtrl = GetComponent<Animator>();
        component.collider = GetComponent<Collider>();
    }

    private void Start()
    {
        
    }

    private void Update()
    { 
        if(timer < awakeTime) timer += Time.deltaTime;
        else
        {
            timer = 0.0f;
            gameObject.SetActive(false);
        }
    }
    private void OnDisable()
    {
        //GameObject enemy_1_n = Instantiate(enemy_1_N);
        //enemy_1_n.transform.position = transform.position;
    }
}
