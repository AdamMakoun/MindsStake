
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    //this is the longest code as of right now
    Player player;
    Enemy enemy;
    [SerializeField]
    private GameObject NormalGame;
    [SerializeField]
    private GameObject enemyPosition;

    private GameObject enemyObject;

    private Vector3 originalEnemyPosition;

    CombatState state = CombatState.playerTurn;

    [SerializeField]
    private Light2D globalLight;
    [SerializeField]
    private GameObject attackOptions;
    [SerializeField]
    private GameObject regularOptions;
    [SerializeField]
    private GameObject attackHeadButton, attackLeftArmButton, attackRightArmButton, attackLeftLegButton, attackRightLegButton, attackBodyButton;
    [SerializeField]
    private Image hpBar;
    [SerializeField]
    private TextMeshProUGUI hpText;
    [SerializeField]
    private Image stressBar;
    [SerializeField]
    private GameObject attackSequencePart;
    [SerializeField]
    private Transform[] attackSpawnPositions;
    [SerializeField]
    private GameObject[] attacks;
    [SerializeField]
    private GameObject target;
    public GameObject Target { get { return target; } }

    private static CombatManager instance;
    public static CombatManager Instance { get { return instance; } }
    private int attacksLeft = 0;

    [SerializeField]
    private float attackSpawnCD = 2f;
    private float currAttackCD;

    List<GameObject> attacksSeq = new List<GameObject>();


    [SerializeField]
    private GameObject attackZone;

    private CombatManager()
    {

    }
    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (state == CombatState.enemyTurn)
        {
            SpawnAttacks();
        }
    }
    public void startCombat(Player player, GameObject enemyObject, Enemy enemyScript)
    {
        gameObject.SetActive(true);
        this.player = player;
        enemy = enemyScript;
        this.enemyObject = enemyObject;
        originalEnemyPosition = enemyObject.transform.position;
        if (enemy != null) { Debug.Log("found enemy"); }
        this.enemyObject.transform.SetParent(enemyPosition.transform);
        this.enemyObject.transform.SetLocalPositionAndRotation(new Vector3(0, 0, 0), Quaternion.identity);

        globalLight.intensity = 1f;
        NormalGame.SetActive(false);
        attackOptions.SetActive(false);
        regularOptions.SetActive(true);
        attackSequencePart.SetActive(false);
        updateBars();
    }
    public void Run()
    {
        RunFromCombat();
    }
    public void attackEnemy()
    {
        attackOptions.SetActive(true);
        DisableAttackOptions();
        regularOptions.SetActive(false);
    }
    public void returnToCombatOptions()
    {
        regularOptions.SetActive(true);
        attackOptions.SetActive(false);
    }
    public void attackEnemyHead()
    {
        enemy.SelectBodyPartAndDamageIt(player.AtkStat * (1 - player.StressLevel / player.MaxStressLevel), "Head");
        if (enemy.CheckForDeath()) StopCombat();
        else BeginAttackSequence();
    }
    public void attackEnemyRightArm()
    {
        enemy.SelectBodyPartAndDamageIt(player.AtkStat * (1 - player.StressLevel / player.MaxStressLevel), "Right Arm");
        if (enemy.CheckForDeath()) StopCombat();
        else BeginAttackSequence();
    }
    public void attackEnemyLeftArm()
    {
        enemy.SelectBodyPartAndDamageIt(player.AtkStat * (1 - player.StressLevel / player.MaxStressLevel), "Left Arm");
        if (enemy.CheckForDeath()) StopCombat();
        else BeginAttackSequence();
    }
    public void attackEnemyRightLeg()
    {
        enemy.SelectBodyPartAndDamageIt(player.AtkStat * (1 - player.StressLevel / player.MaxStressLevel), "Right Leg");
        if (enemy.CheckForDeath()) StopCombat();
        else BeginAttackSequence();
    }
    public void attackEnemyLeftLeg()
    {
        enemy.SelectBodyPartAndDamageIt(player.AtkStat * (1 - player.StressLevel / player.MaxStressLevel), "Left Leg");
        if (enemy.CheckForDeath()) StopCombat();
        else BeginAttackSequence();
    }
    public void attackEnemyBody()
    {
        enemy.SelectBodyPartAndDamageIt(player.AtkStat * (1 - player.StressLevel / player.MaxStressLevel), "Body");
        if (enemy.CheckForDeath()) StopCombat();
        else BeginAttackSequence();
    }
    public void attackPlayer()
    {
        //called each time the attack hits
        if (!enemy.CheckForDeath()) {
            player.GainStress(5);
            player.TakeDMG(enemy.Attack());
        }

        updateBars();
        if (player.CheckForDeath())
        {
            StopCombat();
        }

    }

    public void RunFromCombat()
    {
        globalLight.intensity = 0.1f;
        Debug.Log("Stopping combat...");
        if (!enemy.CheckForDeath())
        {
            enemyObject.transform.SetParent(NormalGame.transform);
            enemyObject.transform.position = originalEnemyPosition;
            enemy.GetOutOfCombat();
            player.GainStress(5f);
        }
        NormalGame.SetActive(true);
        gameObject.SetActive(false);

    }
    public void updateBars()
    {
        hpBar.fillAmount = player.Hp / player.MaxHp;
        hpText.text = $"{player.Hp} / {player.MaxHp}";
        stressBar.fillAmount = player.StressLevel / player.MaxStressLevel;
    }

    public void BeginAttackSequence()
    {
        attackOptions.SetActive(false);
        NormalGame.SetActive(false);
        attackSequencePart.SetActive(true);
        attacksLeft = enemy.GetAvaibleAttacks();
        state = CombatState.enemyTurn;

    }
    private void DisableAttackOptions()
    {
        if(enemy.head == null) attackHeadButton.SetActive(false);
        else if (enemy.head.Hp > 0 || enemy.head != null) attackHeadButton.SetActive(true);
        else attackHeadButton.SetActive(false);

        if (enemy.body == null) attackBodyButton.SetActive(false);
        else if (enemy.body.Hp > 0 || enemy.body != null) attackBodyButton.SetActive(true); 
        else attackBodyButton.SetActive(false);

        if (enemy.rightArm == null) attackRightArmButton.SetActive(false);
        else if (enemy.rightArm.Hp > 0 || enemy.rightArm != null) attackRightArmButton.SetActive(true);
        else attackRightArmButton.SetActive(false);

        if(enemy.leftArm == null) attackLeftArmButton.SetActive(false);
        else if (enemy.leftArm.Hp > 0 || enemy.leftArm != null) attackLeftArmButton.SetActive(true);
        else attackLeftArmButton.SetActive(false);

        if(enemy.rightLeg == null) attackRightLegButton.SetActive(false);
        else if (enemy.rightLeg.Hp > 0 || enemy.rightLeg != null) attackRightLegButton.SetActive(true);
        else attackRightLegButton.SetActive(false);

        if(enemy.leftLeg == null) attackLeftLegButton.SetActive(false);
        else if(enemy.leftLeg.Hp > 0 || enemy.leftLeg != null) attackLeftLegButton.SetActive(true);
        else attackLeftLegButton.SetActive(false);
    }
    enum CombatState
    {
        playerTurn,
        enemyTurn,
    }
    private void SpawnAttacks()
    {
        Transform spawn;
        Vector3 targetDirection;
        GameObject selectedAttack;
        if (attacksLeft > 0)
        {
            if (currAttackCD <= 0)
            {
                spawn = attackSpawnPositions[Random.Range(0, attackSpawnPositions.Length)];
                selectedAttack = Instantiate(attacks[Random.Range(0, attacks.Length)], spawn.position, spawn.rotation);
                selectedAttack.transform.SetParent(gameObject.transform);
                attacksSeq.Add(selectedAttack);

                currAttackCD = attackSpawnCD;
                attacksLeft--;
            }
            else if (currAttackCD > 0)
                currAttackCD -= Time.deltaTime;
        }
    }
    void CheckForSeqChange()
    {
        if (attacksSeq.Count <= 0 && attacksLeft <= 0)
        {
            state = CombatState.playerTurn;
            enemy.regenLimbs();
            attackOptions.SetActive(false);
            regularOptions.SetActive(true);
            attackSequencePart.SetActive(false);
        }
    }
    public void RemoveAttackFromSeq(GameObject attack)
    {
        attacksSeq.Remove(attack);
        Debug.Log($"Removed item with count of {attacksSeq.Count}");
        CheckForSeqChange();
    }
    public void PlayerStressRegain()
    {
        player.regainStress(0.5f);
        updateBars();
    }
    public void StopCombat()
    {
        Destroy(enemy.gameObject);
        globalLight.intensity = 0.1f;
        NormalGame.SetActive(true);
        gameObject.SetActive(false);
    }
}
