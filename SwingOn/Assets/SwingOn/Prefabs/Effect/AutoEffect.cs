using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoEffect : MonoBehaviour
{
    private ParticleSystem effect;
    private float DestroyTime;

    private void Awake()
    {

    }

    private void OnEnable()
    {
        effect = GetComponentInChildren<ParticleSystem>();
        DestroyTime = effect.main.duration;
        StartCoroutine(CheckIfAlive());
    }
    protected virtual void OnDisable()
    {
        effect.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
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
}
