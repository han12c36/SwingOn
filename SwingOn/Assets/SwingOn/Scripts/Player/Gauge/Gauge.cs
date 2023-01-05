using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{
    public ParticleSystem particle;
    public Player player;

    public float startCurveValue = 6.0f;
    public float endCurveValue = 2.0f;

    public Vector3 startPos;
    public Vector3 controllPoint1;
    public Vector3 controllPoint2;
    public Vector3 endPos;

    private float timer;

    private void OnEnable()
    {
        controllPoint1 = new Vector3(startCurveValue * Random.Range(1.0f, -1.0f), startCurveValue * Random.Range(1.0f, 0.0f), 0.0f);
        controllPoint2 = new Vector3(endCurveValue * Random.Range(1.0f, -1.0f), endCurveValue * Random.Range(1.0f, 0.0f));
    }

    private void OnDisable()
    {
        startPos = Vector3.zero;
        endPos = Vector3.zero;
        controllPoint1 = Vector3.zero;
        controllPoint2 = Vector3.zero;
        timer = 0.0f;
    }
    void Start()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        particle.Play();
        player = InGameManager.Instance.GetPlayer;
        //startPos = transform.position;
        endPos = player.transform.position;
        
    }

    void Update()
    {
        timer += Time.deltaTime;
        startPos = transform.position;
        endPos = player.transform.position;
        transform.position = Bezier_3Curve(startPos, controllPoint1, controllPoint2, endPos, timer * timer);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.root.gameObject == InGameManager.Instance.GetPlayer.transform.root.gameObject)
        {
            InGameManager.Instance.inGameCanvas.GetComponentInChildren<Skill_3Animation>().Skill_3Bounce();
            if (player.hardGauge <= player.maxGauge)
            {
                player.hardGauge += 10;
                InGameManager.Instance.inGameCanvas.GetComponentInChildren<Skill_3Animation>().button.coolTimeImage.fillAmount = player.hardGauge / player.maxGauge;
            }
            PoolingManager.Instance.ReturnObj(gameObject);
        }
    }

    public Vector3 Bezier_3Curve(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3,float t)
    {
        float u = 1f - t;
        float t2 = t * t;
        float u2 = u * u;
        float u3 = u2 * u;
        float t3 = t2 * t;

        Vector3 result =(u3) * p0 + (3f * u2 * t) * p1 + (3f * u * t2) * p2 + (t3) * p3;
        return result;
    }
}