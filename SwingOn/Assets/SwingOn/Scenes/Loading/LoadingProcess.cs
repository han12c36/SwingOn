using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingProcess : MonoBehaviour
{
    void Start()
    {
        //GameManager.Instance.SceneCtrl.LoadScene((int)SceneIndex.InGame);
    }

    private void Update()
    {
        Debug.Log("업데이트");
    }
}
