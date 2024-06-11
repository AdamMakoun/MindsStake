
using UnityEngine;

public class LighterBehavior : MonoBehaviour
{
    [SerializeField]
    private float timeToGoOut = 5f * 60;
    private float timeLeft = 0;
    [SerializeField]
    private float regainingStressCd = 1f;

    private float remainingRegainingStressCd = 0f;

    [SerializeField]
    private float stressToRegain = 2f;

    private void Start()
    {
        this.gameObject.SetActive(false);
    }
    private void Update()
    {
        putOut();
        findShroom();
    }
    private void OnEnable()
    {
        timeLeft = timeToGoOut;   
    }
    private void putOut()
    {
        if(timeLeft <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            regainStressToPlayer();
            timeLeft -= Time.deltaTime;
        }
    }
    private void regainStressToPlayer()
    {
        if(remainingRegainingStressCd > 0)
        {
            remainingRegainingStressCd -= Time.deltaTime;
        }
        else
        {
            GameManager.instance.playerBehavior.Player.regainStress(stressToRegain);
            remainingRegainingStressCd = regainingStressCd;
        }
    }
    private void findShroom()
    {
        Collider2D[] physics2D = Physics2D.OverlapCircleAll(gameObject.transform.position, 2f);
        foreach (Collider2D collider in physics2D)
        {
            if (collider.TryGetComponent<ShroomEnemy>(out ShroomEnemy shroom))
            {
                if(shroom != null)
                shroom.Burn();
            }
        }
    }
    
}
