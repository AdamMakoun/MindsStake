using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Player
{
    private float hp;
    public float Hp { get { return hp; } }
    private float maxHp;
    public float MaxHp { get { return maxHp; } }
    private float atkStat;
    public float AtkStat { get { return atkStat; } }
    private float defStat;
    public float finalAtkStat { get { return atkStat + (equipedWeapon != null ? equipedWeapon.damage : 0); } }
    public float DefStat { get { return defStat; } }

    private Weapon equipedWeapon = null;
    public Weapon EquipedWeapon {  get { return equipedWeapon; } private set { equipedWeapon = equipedWeapon; } }

    private List<Fobias> phobias = new List<Fobias>();
    public List<Fobias> Phobias { get { return phobias; } }
    private List<MentalIllnesses> mentalIllnesses = new List<MentalIllnesses>();
    public List<MentalIllnesses> MentalIllnesses { get { return mentalIllnesses; } }

    private float maxStressLevel;
    public float MaxStressLevel { get { return maxStressLevel; } }

    private float stressLevel = 0;
    public float StressLevel { get { return stressLevel; } }

    
    public Player(float maxHp, float atkStat, float defStat, float maxStressLevel)
    {
        this.maxHp = maxHp;
        this.maxStressLevel = maxStressLevel;
        hp = maxHp;
        this.atkStat = atkStat;
        this.defStat = defStat;
        
        Debug.Log(this.hp.ToString());
        gainRandomFobia();
        gainRandomMentalIllness();
        if(mentalIllnesses.Contains(global::MentalIllnesses.depression))
        {
            AmbienceAudioManager.Instance.StartDepressionSoudLoop();
        }
    }

    public void TakeDMG(float dmg)
    {
        if(dmg > 0)
        {
            hp -= dmg;
            if(GameManager.instance.hpBarUpdate != null && GameManager.instance.hpBarUpdate.gameObject.activeSelf)
            GameManager.instance.hpBarUpdate.UpdateHpBar();
        }
    }
    public bool CheckForDeath()
    {
        if(hp <= 0)
        {
            return true;
        }
        return false;
    }
    public void GainStress(float stressToGain)
    {
        if (stressLevel < maxStressLevel)
        {
            stressLevel += stressToGain;
        }
        else stressLevel = maxStressLevel;
        
        //StressManager.Instance.ChangeStressTier();
    }
    public void gainRandomFobia()
    {
        int randomIndex = UnityEngine.Random.Range(0,Enum.GetValues(typeof(Fobias)).Cast<int>().Max());
        phobias.Add((Fobias)Enum.GetValues(typeof(Fobias)).GetValue(randomIndex));
        Debug.Log(phobias[0].ToString());
    }
    public void regainStress(float stressToRegain)
    {
        stressLevel -= stressToRegain;
        if (stressLevel < 0) stressLevel = 0;
        //StressManager.Instance.ChangeStressTier();
    }

    public void RegainHp(float Hp)
    {
        hp += Hp;
        GameManager.instance.hpBarUpdate.UpdateHpBar();
        if (hp > maxHp) hp = maxHp;
    }
    public void gainRandomMentalIllness()
    {
        int randomIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(MentalIllnesses)).Cast<int>().Max());
        mentalIllnesses.Add((MentalIllnesses)Enum.GetValues(typeof(MentalIllnesses)).GetValue(randomIndex));
        Debug.Log(mentalIllnesses[0].ToString());
    }
    public void EquipWeapon(Weapon weapon)
    {
        equipedWeapon = weapon;
    }
    public void ResetStress()
    {
        stressLevel = 0;
    }
}
