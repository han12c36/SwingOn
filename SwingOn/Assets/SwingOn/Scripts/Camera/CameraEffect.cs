using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShakeType
{
    Smooth,
    Rough
}

public class CameraEffect : MonoBehaviour
{
    public static string EffectDataPrfabFolderPath = "EffectDataPrefabs";

    public static CameraEffect instance = null;
    Vector3 originPos;
    Vector3 originRot;
    private bool curDataStop;
    public bool zoomStart;
    [SerializeField]
    private List<EffectData> List_EffectDatas = new List<EffectData>();
    private Dictionary<string, EffectData> Dic_EffectDatas = new Dictionary<string, EffectData>();
    public EffectData curData;
    public Zoom curZoom;

    public Vector3 OriginPos { get { return originPos; } }
    public Vector3 OriginRot { get { return originRot; } }
    public bool Stop { get { return curDataStop; } set { curDataStop = value; } }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(instance);
    }

    private void Start()
    {
        Load_Dic();

        originPos = transform.localPosition;
        originRot = transform.localEulerAngles;
        //originPos = transform.localPosition;
        //originRot = transform.localEulerAngles;

        //originPos = Camera.main.transform.localPosition;
        //originRot = Camera.main.transform.localEulerAngles;
    }
    private void Update()
    {
        if (curZoom != null)
        {
            if (!curZoom.isFinish)
            {
                if (!zoomStart) zoomStart = true;
                curZoom.Update();
            }
            else
            {
                zoomStart = false;
                curZoom.isFinish = false;
                curZoom = null;
            }
        }
    }
    private void LateUpdate()
    {
        if (curData) curData.Update();
    }

    private void Load_Dic()
    {
        EffectData[] Datas = Resources.LoadAll<EffectData>(EffectDataPrfabFolderPath);
        for (int i = 0; i < Datas.Length; i++)
        {
            EffectData data = new EffectData(Datas[i].duration, Datas[i].shakeSpeed, Datas[i].magnitude, Datas[i].shakePosition,
                                             Datas[i].shakeRotation, Datas[i].Curve, Datas[i].isSeedUpdate);
            Dic_EffectDatas.Add(Datas[i].name, Datas[i]);
        }
    }

    public void PlayShake(string dataName)
    {
        if (curData != null) return;

        if (Dic_EffectDatas.ContainsKey(dataName))
        {
            EffectData data = Dic_EffectDatas[dataName];

            Debug.Log(dataName);

            //List_EffectDatas.Add(data);

            //SelectCurData(data);
            if (curData != null)
            {
                if (curData.roop && data.roop)
                {
                    if (curData != data) curData = data;
                }
                else curData = data;
            }
            else curData = data;

            curData.Start = true;

            //루트다 아니다
            //
            //있을수도 없을수도
            //
        }
        else Debug.Log("해당 데이터는 딕셔너리에 존재하지 않습니다.");
    }

    public void PlayZoom(ZoomDir dir, float speed, float duration, Vector3 originPos)
    {
        curZoom = new Zoom(dir, speed, duration, originPos);
    }
    public void PlayZoom(ZoomDir dir, float speed, bool check)
    {
        curZoom = new Zoom(dir, speed, check);
    }
}

public enum ZoomDir
{
    Front,
    Back,
}
public class Zoom
{
    private bool check;
    public ZoomDir dir;
    public float speed;
    public float power;
    public float duration;
    public Vector3 originPos = new Vector3(0.0f, 0.0f, -2f);
    private float startTimer;

    public bool isFinish;
    public bool isFinishDir;
    public bool startBack;

    private Vector3 Dir;
    private Vector3 tempPos;

    public bool Check { get { return check; } set { check = value; } }

    public Zoom(ZoomDir _dir, float _speed, bool _check)
    {
        dir = _dir;
        speed = _speed;
        check = _check;
        if (dir == ZoomDir.Front)
        {
            //Vector3 vec = Player.instance.transform.position - Camera.main.transform.localPosition;
            Vector3 vec = Player.instance.transform.position - CameraEffect.instance.transform.localPosition;
            Dir = vec.normalized;
        }
        else
        {
            //Vector3 vec = Camera.main.transform.localPosition - Player.instance.transform.position;
            Vector3 vec = CameraEffect.instance.transform.localPosition - Player.instance.transform.position;
            Dir = vec.normalized;
        }
    }
    public Zoom(ZoomDir _dir, float _speed, float _duration, Vector3 _originPos)
    {
        dir = _dir;
        speed = _speed;
        duration = _duration;
        originPos = _originPos;
        if (dir == ZoomDir.Front)
        {
            //Vector3 vec = Player.instance.transform.position - Camera.main.transform.localPosition;
            Vector3 vec = Player.instance.transform.position - CameraEffect.instance.transform.localPosition;
            Dir = vec.normalized;
        }
        else
        {
            //Vector3 vec = Camera.main.transform.localPosition - Player.instance.transform.position;
            Vector3 vec = CameraEffect.instance.transform.localPosition - Player.instance.transform.position;
            Dir = vec.normalized;
        }
    }
    public void Update()
    {
        if (!check) Play();
        else CheckPlay();
    }
    private void Play()
    {
        if (startTimer < duration)
        {
            startTimer += Time.deltaTime;
            if (!isFinishDir)
            {
                if (startTimer <= duration * 0.5f)
                {
                    Vector3 vec = new Vector3(0.0f, 0.0f, originPos.z + -Dir.z * speed * startTimer);
                    if (vec.z > -1f) vec.z = -1f;
                    else if (vec.z < -13f) vec.z = -13f;
                    //Camera.main.transform.localPosition = vec;
                    CameraEffect.instance.transform.localPosition = vec;
                    Debug.Log(vec);
                }
                else
                {
                    //tempPos = Camera.main.transform.localPosition;
                    tempPos = CameraEffect.instance.transform.localPosition;
                    isFinishDir = true;
                    startTimer = 0.0f;
                }
            }
            else
            {
                startTimer += Time.deltaTime;
                Vector3 vec = new Vector3(0.0f, 0.0f, tempPos.z + Dir.z * speed * startTimer);
                if (vec.z > -1f) vec.z = -1f;
                else if (vec.z < -13f) vec.z = -13f;
                //Camera.main.transform.localPosition = vec;
                CameraEffect.instance.transform.localPosition = vec;
                Debug.Log(vec);
            }
        }
        else
        {
            //Camera.main.transform.localPosition = originPos;
            isFinish = true;
            isFinishDir = false;
            startTimer = 0.0f;
        }
    }

    public void CheckPlay()
    {
        if (!isFinishDir)
        {
            //진행
            startTimer += Time.deltaTime;
            Vector3 vec = new Vector3(0.0f, 0.0f, originPos.z + -Dir.z * speed * startTimer);
            if (vec.z > -1f) vec.z = -1f;
            else if (vec.z < -13f) vec.z = -13f;
            //Camera.main.transform.localPosition = vec;
            CameraEffect.instance.transform.localPosition = vec;
        }
        else
        {
            //외부에서 강제로 꺼
            //tempPos = Camera.main.transform.localPosition;
            tempPos = CameraEffect.instance.transform.localPosition;
            startTimer = 0.0f;
            startBack = true;
        }

        if (startBack)
        {
            startTimer += Time.deltaTime;
            Vector3 vec = new Vector3(0.0f, 0.0f, tempPos.z + Dir.z * speed * startTimer);
            if (vec.z > -1f) vec.z = -1f;
            else if (vec.z < -13f) vec.z = -13f;
            //Camera.main.transform.localPosition = vec;]
            CameraEffect.instance.transform.localPosition = vec;
            if (originPos == CameraEffect.instance.transform.localPosition)
            {
                startBack = false;
                isFinish = true;
            }
        }
    }
}
