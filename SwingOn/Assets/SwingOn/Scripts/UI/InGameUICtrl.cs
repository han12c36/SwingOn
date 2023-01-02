using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUICtrl : MonoBehaviour
{
    public Enums.PlayerAttType curPlayerAttType;
    public Enums.PlayerAttType prePlayerAttType;
    public Player player;

    public bool startSkill_1;
    public bool startSkill_2;
    public bool startSkill_3;

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
        //curPlayerAttType = player.ActionTable.AttType;
        //ChangeModeIcon(curPlayerAttType);
        //prePlayerAttType = curPlayerAttType;
    }

    public void ChangeModeIcon(Enums.PlayerAttType attType)
    {
        //if (prePlayerAttType == curPlayerAttType) return;
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
        if(!player.ActionTable.ModeChange && !startSkill_1)
        {
            player.ActionTable.isSkill_1Down = true;
            startSkill_1 = true;
        }
    }

    public void Button_Skill_2()
    {
        if (!player.ActionTable.ModeChange && !startSkill_2)
        {
            player.ActionTable.isSkill_2Down = true;
            startSkill_2 = true;
        }
    }
    public void Button_Skill_3()
    {
        player.ActionTable.isSkill_3Down = true;
        //startSkill_3 = true;
        //StartCoroutine(ShowCoolTimeImage(skill_3Btn, 1.0f));
    }
    public void Button_Att()
    {
        if (!player.ActionTable.ModeChange)
        {
            player.ActionTable.isAtt_Down = true;
        }
    }

    public IEnumerator ShowCoolTimeImage(Button btn,float coolTime)
    {
        ButtonCoolTime buttonImage = btn.GetComponent<ButtonCoolTime>();

        buttonImage.coolTimeImage.gameObject.SetActive(true);
        Debug.Log("ÄÚ·çÆ¾ µå¿È");
        float timer = 0.0f;
        while(timer < coolTime)
        {
            yield return null;
            timer += Time.deltaTime;
            buttonImage.coolTimeImage.fillAmount -= Time.deltaTime / coolTime;
            buttonImage.coolTimeText.text = ((int)(coolTime - timer)).ToString();
            if (timer >= coolTime || player.ActionTable.ModeChange)
            {
                buttonImage.coolTimeImage.gameObject.SetActive(false);
                buttonImage.coolTimeImage.fillAmount = 1.0f;
                if (btn == skill_1Btn) startSkill_1 = false;
                if (btn == skill_2Btn) startSkill_2 = false;
                //startSkill_3 = false;
                yield break;
            }
        }
    }
    //============================================================================
}
