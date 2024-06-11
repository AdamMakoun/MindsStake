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
    [SerializeField] private AudioSource[] sounds;
    protected List<BodyPart> parts = new List<BodyPart>();
    protected EnemyMovement movementScript;
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

    [SerializeField] private float makeSoundCD = 5;
    private float currMakeSoundCD = 0;
    [SerializeField] private float loseInterestTime = 3;
    private float currLoseInterestTime = 0;
    private bool isPlayerInSight = false;
    protected virtual void Start()
    {
        movementScript = gameObject.GetComponent<EnemyMovement>();
        collider = gameObject.GetComponent<BoxCollider2D>();
    }
    protected virtual void Update()
    {
        LowerCombatCD();
        LosePlayer();
        LowerMakeSoundCD();
        
    }
    
    private void LosePlayer()
    {
        if(!isPlayerInSight)
        {
            if (currLoseInterestTime < loseInterestTime)
            {
                currLoseInterestTime += Time.deltaTime;
            }
            else
            {
                movementScript.LosePlayer();
            }
        }
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayerInSight = false;
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayerInSight = true;
            currLoseInterestTime = 0;
            movementScript.FindPlayer(collision.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            makeSound();
        }
    }
    public void makeSound()
    {
        if(sounds.Length > 0)
        {
            if (currMakeSoundCD <= 0)
            {
                sounds[Random.Range(0, sounds.Length)].Play();
                currMakeSoundCD = makeSoundCD;
            }
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
                part.TakeDMG(dmg);
                makeSound();
                break;
            }
        }
    }
    private void LowerMakeSoundCD()
    {
        if(currMakeSoundCD > 0)
            currMakeSoundCD -= Time.deltaTime;
    }
    public void GetOutOfCombat()
    {
        if (isInCombat)
        {
            movementScript.enabled = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
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
