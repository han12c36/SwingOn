using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUICtrl : MonoBehaviour
{
    public Enums.PlayerAttType curPlayerAttType;
    public Enums.PlayerAttType prePlayerAttType;
    public Player player;
    
    [Header("Buttons")]
    public Button skill_1Btn;
    public Button skill_2Btn;
    public Button skill_3Btn;
    public Button att_Btn;
    [Space(10.0f)]
    [Header("NormalSkillSprite")]
    public Sprite dashIcon;
    public Sprite tornadoIcon;
    [Space(10.0f)]
    [Header("NormalSkillSprite")]
    public Sprite blitzIcon;
    public Sprite powerShotIcon;
    [Space(10.0f)]
    [Header("NormalSkillSprite")]
    public Sprite groundBreakIcon;
    public Sprite hardTornadoIcon;

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
                    skill_1Btn.image.sprite = dashIcon;
                    skill_2Btn.image.sprite = tornadoIcon;
                }
                break;
            case Enums.PlayerAttType.Speed:
                {
                    skill_1Btn.image.sprite = blitzIcon;
                    skill_2Btn.image.sprite = powerShotIcon;
                }
                break;
            case Enums.PlayerAttType.Hard:
                {
                    skill_1Btn.image.sprite = groundBreakIcon;
                    skill_2Btn.image.sprite = hardTornadoIcon;
                }
                break;
            case Enums.PlayerAttType.End:
                break;
            default:
                break;
        }
    }

    //============================================================================
    //button
    public void Button_Skill_1()
    {
        if(!player.ActionTable.ModeChange)
        {
            player.ActionTable.isSkill_1Down = true;
        }
    }
    public void Button_Skill_2()
    {
        if (!player.ActionTable.ModeChange)
        {
            player.ActionTable.isSkill_2Down = true;
        }
    }
    public void Button_Skill_3()
    {
        if (!player.ActionTable.ModeChange)
        {
            player.ActionTable.isSkill_3Down = true;
        }
    }
    public void Button_Att()
    {
        if (!player.ActionTable.ModeChange)
        {
            player.ActionTable.isAtt_Down = true;
        }
    }
    //============================================================================
}
