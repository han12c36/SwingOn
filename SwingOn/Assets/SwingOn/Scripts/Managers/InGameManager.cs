using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnEnemyType
{
    Enemy_1_N,  // ´Þ°¿ ÀÛÀº³ð       //50%
    Enemy_2,    // ¾¾¾Ñ             //50%
    //-> ³­ÀÌµµ »ó½Â±¸°£
    Enemy_3,    // ÀÚÆø             //40%
    Enemy_1_H,  // ´Þ°¿ Å«³ð        //25%
    //-> ³­ÀÌµµ »ó½Â±¸°£
    Enemy_4,    // ÀÚÆøÅ«³ð         //25%
    Enemy_5,    // ÀÚÆøÅ«³ðº¸´Ù Å«³ð //20%
    //-> ³­ÀÌµµ »ó½Â±¸°£
    Enemy_6,    // ¹ÚÁã             //10%
    Enemy_7,    // ´Á´ë             //10%
    End
}


public class InGameManager : Manager<InGameManager>
{
    private Player player;
    private GameObject ScorePanel;
    public InGameCanvas inGameCanvas;
    public ButtonCoolTime skill_3ButtonCoolTime;

    private Vector3 playerStartPos = new Vector3(0.0f, 0.0f, -9f);
    private Vector3 spawnerPos_1 = new Vector3(-16.0f,1.5f,-1.5f);
    private Vector3 spawnerPos_2 = new Vector3(16.0f, 1.5f, -15f);

    private float[] spawnPercent = new float[(int)SpawnEnemyType.End]
    {50.0f,50.0f,40.0f,25.0f,25.0f,20.0f,10.0f,10.0f};
    private string[] EnemyNameTable = new string[(int)SpawnEnemyType.End] 
    { "Enemy_1_N", "Enemy_2", "Enemy_3", "Enemy_1_H", "Enemy_4", "Enemy_5", "Enemy_6", "Enemy_7" };

    public float playerLifeTime;
    private float gameStartTime;
    public float totalDamage;

    [SerializeField]
    private float timer;
    private float enemySpawnTimer = 10.0f;
    private int enemySpawnIndex = 2;

    public Player GetPlayer { get { return player; } }

    private void Awake()
    {
        Player.instance.transform.position = playerStartPos;
        player = Player.instance;
        inGameCanvas = GameObject.Find("InGameCanvas").GetComponent<InGameCanvas>();
        skill_3ButtonCoolTime = inGameCanvas.GetComponentInChildren<ButtonCoolTime>();
    }
    public override void OnEnable()
    {
        base.OnEnable();
        gameStartTime = Time.time;
    }
    private void Start()
    {
    }

    private void Update()
    {

        if (player.status.curHp > 0)
        {
            playerLifeTime += Time.deltaTime;
            IncreasingDifficulty();
            //EnemySpawn(spawnerPos_1);
            //EnemySpawn(spawnerPos_2);
        }
        else
        {
            Structs.UserSaveDatas datas;

            if (PlayerPrefs.GetFloat("BestLifeTime") != 0.0f)
            {
                float saveTime = PlayerPrefs.GetFloat("BestLifeTime");
                float saveDamage = PlayerPrefs.GetFloat("BestDamage");
                if (saveTime >= playerLifeTime) datas.bestLifeTime = saveTime;
                else datas.bestLifeTime = playerLifeTime;
                if (saveDamage >= totalDamage) datas.bestDamage = saveDamage;
                else datas.bestDamage = totalDamage;

                datas.playableStageIndex = 0;
                GameManager.Instance.SaveData = datas;
            }
            else
            {
                datas.bestLifeTime = playerLifeTime;
                datas.bestDamage = totalDamage;
                datas.playableStageIndex = 0;
                GameManager.Instance.SaveData = datas;
            }
        }
    }
    public override void OnDisable()
    {
        base.OnDisable();
        gameStartTime = 0.0f;
        enemySpawnTimer = 0.0f;
        enemySpawnIndex = 2;
    }

    private void IncreasingDifficulty()
    {
        if (playerLifeTime > 120.0f)
        {
            enemySpawnTimer = 8.0f;
            enemySpawnIndex = 4;
        }
        else if (playerLifeTime > 240.0f)
        {
            enemySpawnTimer = 7.5f;
            enemySpawnIndex = 6;
        }
        else if (playerLifeTime > 480.0f)
        {
            enemySpawnTimer = 7.0f;
            enemySpawnIndex = 8;
        }
    }

    private void EnemySpawn(Vector3 spawnerPos)
    {
        if(timer >= enemySpawnTimer)
        {
            timer = 0.0f;
            GameObject obj = PoolingManager.Instance.LentalObj(EnemyNameTable[SelectEnemy(spawnPercent)]);
            obj.transform.position = spawnerPos;
            Debug.Log(obj.name);
        }
        else timer += Time.deltaTime;
    }

    public int SelectEnemy(float[] probs)
    {
        int Index = enemySpawnIndex - 1;
        for (int i = 0; i < probs.Length; i++)
        {
            if (i <= Index) continue;
            probs[i] = 0.0f;
        }

        float total = 0;
        foreach (float elem in probs) { total += elem; }
        float randomPoint = Random.value * total;
        for (int i = 0; i < probs.Length; i++)
        {
            if (randomPoint < probs[i]) return i;
            else randomPoint -= probs[i];
        }
        return probs.Length - 1;
    }
   
}
