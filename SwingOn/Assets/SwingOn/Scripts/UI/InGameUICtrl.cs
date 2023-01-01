using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUICtrl : MonoBehaviour
{
    public Enums.PlayerAttType curPlayerAttType;
    public Enums.PlayerAttType prePlayerAttType;
    public Player player;

    public Image normalSkillSet;
    public Image speedSkillSet;
    public Image hardSkillSet;

    void Start()
    {
        player = InGameManager.Instance.GetPlayer;
    }

    void Update()
    {
        curPlayerAttType = player.ActionTable.AttType;
        ChangeModeIcon(curPlayerAttType);
        prePlayerAttType = curPlayerAttType;
    }

    private void ChangeModeIcon(Enums.PlayerAttType attType)
    {
        if (prePlayerAttType == curPlayerAttType) return;
        switch (attType)
        {
            case Enums.PlayerAttType.Normal:
                {
                    normalSkillSet.gameObject.SetActive(true);
                    speedSkillSet.gameObject.SetActive(false);
                    hardSkillSet.gameObject.SetActive(false);
                }
                break;
            case Enums.PlayerAttType.Speed:
                {
                    normalSkillSet.gameObject.SetActive(false);
                    speedSkillSet.gameObject.SetActive(true);
                    hardSkillSet.gameObject.SetActive(false);
                }
                break;
            case Enums.PlayerAttType.Hard:
                {
                    normalSkillSet.gameObject.SetActive(false);
                    speedSkillSet.gameObject.SetActive(false);
                    hardSkillSet.gameObject.SetActive(true);
                }
                break;
            case Enums.PlayerAttType.End:
                break;
            default:
                break;
        }
    }

}
