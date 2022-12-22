using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoEffect : MonoBehaviour
{
    private ParticleSystem effect;
    private float DestroyTime;

    private float timer;
    public float EffectColliderLifeTime;
    [SerializeField]
    private Collider collider;

    private void Awake()
    {
        collider = GetComponentInChildren<Collider>();
    }

    private void OnEnable()
    {
        effect = GetComponentInChildren<ParticleSystem>();
        DestroyTime = effect.main.duration;
        StartCoroutine(CheckIfAlive());
    }
    protected void OnDisable()
    {
        effect.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        StopCoroutine(CheckIfAlive());
        timer = 0.0f;
    }
    IEnumerator CheckIfAlive()
    {
        effect.Play();
        while (effect != null)
        {
            yield return new WaitForSeconds(DestroyTime);
            if (!effect.IsAlive(true))
            {
                effect.Stop();
                PoolingManager.Instance.ReturnObj(gameObject);
                break;
            }
        }
    }

    protected void Update()
    {
        if(collider != null && collider.enabled)
        {
            if (timer > EffectColliderLifeTime)
            {
                timer = 0.0f;
                collider.enabled = false;
            }
            else timer += Time.deltaTime;
        }
    }
}
