using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BodyPart
{
    private float hp;
    private float maxHp;
    public float MaxHp { get { return maxHp; } }
    public float Hp { get { return hp; } }
    private string partName;
    public string PartName {  get { return partName; } }
    private bool needForLiving;
    public bool NeedForLiving {  get { return needForLiving; } }
    private GameObject part;
    public GameObject Part { get { return part; } }
    private int turnsToRegen = 4;
    private int turnsleftForRegen = 4;
    private string name;
    public string Name { get { return name; } }
    public BodyPart(string name, float maxHp,bool needForLiving,GameObject part)
    {
        this.maxHp = maxHp;
        this.name = name;
        hp = maxHp;
        this.needForLiving = needForLiving;
        this.part = part;
    }

    public void TakeDMG(float dmg)
    {
        this.hp -= dmg;
        Debug.Log($"{name} HP: {hp}");
        CheckForPartDeath();
    }
    public bool CheckForPartDeath()
    {
        if (this.hp <= 0)
        {
            DestroyThis();
            return true;
        }
        return false;
    }
    
    public void DestroyThis()
    {
        if (hp <= 0)
        {
            part.SetActive(false);
            turnsleftForRegen = turnsToRegen;
        }
        
    }
    //this is used only on some enemies
    public void RegenPart()
    {
        if(turnsleftForRegen == 0)
        {
            part.SetActive(true);
            hp = maxHp;
            turnsleftForRegen = turnsToRegen;
        }
        else
            turnsleftForRegen--;

    }
    public void outOfCombatRegen()
    {
        part.SetActive(true);
        hp = maxHp;
        turnsleftForRegen = turnsToRegen;
    }
}
