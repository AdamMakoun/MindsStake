using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public BodyPart head;
    public BodyPart body;
    public BodyPart leftLeg;
    public BodyPart rightLeg;
    public BodyPart leftArm;
    public BodyPart rightArm;
    public GameObject Object;
    protected List<BodyPart> parts = new List<BodyPart>();
    private EnemyMovement movementScript;
    protected List<Fobias> phobias = new List<Fobias>();

    BoxCollider2D collider;

    public List<Fobias> Phobias { get { return phobias; } }
    protected int baseAttacksToDo;
    public int BaseAttacksToDo { get { return baseAttacksToDo; } }
    public List<BodyPart> Parts { get { return parts; } }
    [SerializeField]
    protected float combatCD = 1f;
    protected float currCombatCD = 0;
    public bool isInCombat = false;
    protected virtual void Start()
    {
        movementScript = gameObject.GetComponent<EnemyMovement>();
        collider = gameObject.GetComponent<BoxCollider2D>();
    }
    protected virtual void Update()
    {
        LowerCombatCD();
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && currCombatCD <= 0)
        {
            Debug.Log("collided with player");
            currCombatCD = combatCD;

            isInCombat = true;
            movementScript.enabled = false;
            CombatManager.Instance.gameObject.SetActive(true);
            CombatManager.Instance.startCombat(collision.gameObject.GetComponent<PlayerBehavior>().Player, gameObject, this);
        }
    }
    protected void LowerCombatCD()
    {
        if (currCombatCD > 0 && isInCombat == false)
        {
            currCombatCD -= Time.deltaTime;
            collider.enabled = false;
        }
        else if (currCombatCD <= 0) { collider.enabled = true; }
    }
    virtual public int Attack()
    {
        Debug.Log("normal");
        return Random.Range(1, 5);
    }
    public void SelectBodyPartAndDamageIt(float dmg,string nameOfPart)
    {
        foreach (BodyPart part in Parts) { 
        if(nameOfPart == part.Name)
            {
                part.TakeDMG(dmg); break;
            }
        }
    }
    public void GetOutOfCombat()
    {
        if (isInCombat)
        {
            movementScript.enabled = true;
            currCombatCD = combatCD;
            isInCombat = false;
        }

    }
    public bool CheckForDeath()
    {
        foreach (BodyPart part in parts)
        {
            if (part.NeedForLiving == true)
            {
                if (part.CheckForPartDeath())
                {
                    return true;
                }
            }
        }
        return false;
    }
    public virtual void regenLimbs()
    {

    }
    public int GetAliveLimbs()
    {
        int aliveLimbs = 0;
        foreach (BodyPart part in Parts)
        {
            if(part.Hp > 0)
                aliveLimbs++;
        }
        return aliveLimbs;
    }
    public int GetAvaibleAttacks()
    {
        Debug.Log($"{baseAttacksToDo + GetAliveLimbs()}");
        return baseAttacksToDo + GetAliveLimbs();
    }
}
