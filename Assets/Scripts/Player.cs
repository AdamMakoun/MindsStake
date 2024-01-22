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
    public float DefStat { get { return defStat; } }

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
    }

    public void TakeDMG(float dmg)
    {
        if(dmg > 0)
        {
            hp -= dmg;
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
    }
    public void gainRandomMentalIllness()
    {
        int randomIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(MentalIllnesses)).Cast<int>().Max());
        mentalIllnesses.Add((MentalIllnesses)Enum.GetValues(typeof(MentalIllnesses)).GetValue(randomIndex));
        Debug.Log(mentalIllnesses[0].ToString());
    }
}
