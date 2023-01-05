using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : Manager<SoundManager>
{
    public Dictionary<string, AudioClip> audioClips;
    string audioFolderPath = "AudioClips";

    [SerializeField] Transform tempAusBox;

    public AudioSource bgmAus;
    public Queue<AudioSource> tempAusQueue;

    float volumeValue = 1f;
    void Awake()
    {
        SearchAllAudClips();
        CreateTempAudioSource(50);
        CreateBgmAudioSource();
    }

    public override void InstantiateManagerForNextScene(Scene scene, LoadSceneMode mode)
    {
        base.InstantiateManagerForNextScene(scene, mode);
        StopAllSound();
    }

    void SearchAllAudClips()
    {
        AudioClip[] clips = Resources.LoadAll<AudioClip>(audioFolderPath);

        audioClips = new Dictionary<string, AudioClip>();
        foreach (AudioClip clip in clips)
        {
            string clipName = clip.name;
            audioClips.Add(clipName, clip);
        }
    }


    //��� -> ���⼭
    //�����Ʈ -> �� �� �����Ű���� ����
    //�Ͻ��� ��ü�� -> Pooling�صΰ� ����
    //�ش� ������Ʈ���� �� �Ҹ� -> transform���� ���� ����� �ҽ� ã�Ƽ� �ְ� ������ �Ͻ��� ��ü������ ����
    public IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }
        audioSource.Stop();
    }

    public IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        audioSource.Play();
        volumeValue = 0f;
        while (volumeValue < 1)
        {
            volumeValue += Time.deltaTime / FadeTime;
            bgmAus.volume = volumeValue;
            yield return null;
        }
    }

    public void PlayBgm(string clipName, float volume = 1f)
    { //�̰� �ѹ��ݺ�
        if (!bgmAus)
        {
            CreateBgmAudioSource();
        }
        bgmAus.clip = GetAuidoClip(clipName);
        bgmAus.volume = volume;
        bgmAus.Play();
    }

    public void PlayBgmFade(string clipName)
    {
        if (!bgmAus)
        {
            CreateBgmAudioSource();
        }
        bgmAus.clip = GetAuidoClip(clipName);
        StartCoroutine(FadeIn(bgmAus, 5f));
    }

    public void StopBgm()
    {
        if (bgmAus)
        {
            bgmAus.Stop();
        }

    }

    public void StopAllSE()
    {
        AudioSource[] allAS = FindObjectsOfType<AudioSource>();

        foreach (AudioSource audio in allAS)
        {
            if (audio == bgmAus)
            {
                continue;
            }
            audio.Stop();
        }
    }
    public void StopAllSound()
    {
        AudioSource[] allAS = FindObjectsOfType<AudioSource>();

        foreach (AudioSource audio in allAS)
        {
            audio.Stop();
        }
    }

    public void PauseAllSound()
    {
        AudioSource[] allAS = FindObjectsOfType<AudioSource>();

        foreach (AudioSource audio in allAS)
        {
            audio.Pause();
        }
    }

    public void ResumeAllSound()
    {
        AudioSource[] allAS = FindObjectsOfType<AudioSource>();

        foreach (AudioSource audio in allAS)
        {
            audio.Play();
        }
    }


    public void PlayTempSound(string clipName, Vector3 pos, float volume = 1f)
    {
        //Ư�� ��ġ�� ��� Ʋ�����ų� �ȿ����϶� ��.
        AudioClip clip = GetAuidoClip(clipName);

        if (clip != null)
        {
            AudioSource temp = tempAusQueue.Dequeue();

            if (!temp.isPlaying)
            {
                temp.gameObject.SetActive(true);
                //temp.volume = volume * GameManager.Instance.SeOffset;
                temp.transform.position = pos;
                temp.PlayOneShot(clip);
                tempAusQueue.Enqueue(temp);
            }
            else
            {
                tempAusQueue.Enqueue(temp);

                GameObject newObj = new GameObject();
                newObj.name = "tempAus_" + tempAusQueue.Count.ToString();
                newObj.transform.parent = tempAusBox.transform;
                AudioSource newAus = newObj.AddComponent<AudioSource>();
                newAus.spatialBlend = 1f;

                //newObj.AddComponent<TempAudioSource>();

                //newAus.volume = volume * GameManager.Instance.SeOffset;
                newAus.transform.position = pos;
                newAus.PlayOneShot(clip);
                tempAusQueue.Enqueue(newAus);
            }
        }
    }

    public void PlaySound(string clipName, GameObject obj, float volume = 1f)
    {
        //�����̴� ������Ʈ�鿡 ���. (�Ҹ��� ���� �������� �Ҷ�)
        //�� ������Ʈ �ȿ� AuidoSource ������Ʈ ã�ƺ��� ������ PlayTempSound�� �������� 
        AudioClip clip = GetAuidoClip(clipName);

        if (clip)
        {
            AudioSource aus = obj.transform.root.GetComponent<AudioSource>();

            if (aus)
            {
                //aus.volume = volume * GameManager.Instance.SeOffset;
                aus.PlayOneShot(clip);
            }
            else
            { PlayTempSound(clipName, obj.transform.position, volume); }
        }

    }




    void CreateTempAudioSource(int count)
    {
        if (tempAusBox != null)
        {
            string boxName = "TempAusBox";
            Transform tr = transform.Find(boxName);
            if (tr == null)
            {
                GameObject newObj = new GameObject(boxName);
                newObj.transform.SetParent(gameObject.transform);
                tr = newObj.transform;
            }
            tempAusBox = tr;
        }

        if (tempAusQueue == null)
        {
            tempAusQueue = new Queue<AudioSource>();
        }

        for (int i = 0; i < count; ++i)
        {
            GameObject newObj = new GameObject();
            newObj.name = $"tempAus_{i}";
            newObj.transform.parent = tempAusBox.transform;

            AudioSource aus = newObj.AddComponent<AudioSource>();
            aus.spatialBlend = 1f;

            //newObj.AddComponent<TempAudioSource>();

            newObj.SetActive(false);
            tempAusQueue.Enqueue(aus);
        }
    }

    void CreateBgmAudioSource()
    {
        if (bgmAus == null)
        {
            string bgmAusName = "BgmAus";
            Transform tr = gameObject.transform.Find(bgmAusName);

            if (tr == null)
            {
                GameObject newObj = new GameObject(bgmAusName);
                newObj.transform.SetParent(transform);
                tr = newObj.transform;
            }

            AudioSource aus = tr.GetComponent<AudioSource>();

            if (aus == null)
            {
                aus = tr.gameObject.AddComponent<AudioSource>();

            }

            aus.spatialBlend = 0f;
            //3d sound ���ϱ� ���ؼ�
            aus.loop = true;
            bgmAus = aus;
        }
    }

    AudioClip GetAuidoClip(string clipName)
    {
        AudioClip audioClip = null;
        if (!audioClips.TryGetValue(clipName, out audioClip))
        {
            Debug.LogError($"{clipName} is empty");
        }
        return audioClip;
    }


}
