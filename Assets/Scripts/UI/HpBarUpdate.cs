using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HpBarUpdate : MonoBehaviour
{
    public TMP_Text hpText;
    public Image hpBar;
    
    public void UpdateHpBar()
    {
        hpBar.fillAmount = GameManager.instance.playerBehavior.Player.Hp / GameManager.instance.playerBehavior.Player.MaxHp;
        hpText.text = GameManager.instance.playerBehavior.Player.Hp + "/" + GameManager.instance.playerBehavior.Player.MaxHp;
    }
}
