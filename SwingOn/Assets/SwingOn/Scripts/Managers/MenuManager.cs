using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : Manager<MenuManager>
{
    public Text bestTime;
    public Text bestDamage;

    void Start()
    {
        bestTime = GameObject.Find("BestLifeText").GetComponent<Text>();
        bestDamage = GameObject.Find("BestDamageText").GetComponent<Text>();

        if(bestTime != null && bestDamage != null)
        {
            bestTime.text = GetScore("BestLifeTime").ToString();
            bestDamage.text = GetScore("BestDamage").ToString();
        }
    }
    private float GetScore(string scoreName)
    {
        float saveScore = PlayerPrefs.GetFloat(scoreName);
        if (saveScore != 0.0f)
        {
            if (GameManager.Instance.SaveData.bestLifeTime <= saveScore) return saveScore;
            else
            {
                if (scoreName == "BestLifeTime") return GameManager.Instance.SaveData.bestLifeTime;
                else return GameManager.Instance.SaveData.bestDamage;
            }
        }
        else
        {
            if (scoreName == "BestLifeTime") return GameManager.Instance.SaveData.bestLifeTime;
            else return GameManager.Instance.SaveData.bestDamage;
        }
    }
}
