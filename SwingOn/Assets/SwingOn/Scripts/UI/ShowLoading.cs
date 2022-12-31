using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLoading : MonoBehaviour
{
    public void goLoad()
    {
        SceneController.Instance.LoadScene((int)SceneIndex.InGame);
    }
}
