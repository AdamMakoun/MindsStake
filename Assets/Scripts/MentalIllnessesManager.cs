using UnityEngine;

public class MentalIllnessesManager : MonoBehaviour
{
    public static MentalIllnessesManager instance { get; private set; }
    
    [SerializeField] private float procTimer = 10;
    private float currentTimer = 10;
    [SerializeField] private float chanceToForget = 100;
    [SerializeField] private float chanceToProcDepression = 100;
    [SerializeField] private float dmgFromDepression = 5;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
   
    void Update()
    {
        reduceTimer();
        if(currentTimer < 0)
        {
            Forget();

        }
    }
    public void Forget() { 
        if (GameManager.instance.playerBehavior.Player.MentalIllnesses.Contains(MentalIllnesses.dementia) ) 
        { 
            if(Random.Range(0, 100) < chanceToForget)
            {
                MinimapBehavior.Instance.ForgetRandomRoom();
                Debug.Log("I Forgor :skull:");
            }
            currentTimer = procTimer;
        }
    }
    public void reduceTimer() { 
        
        currentTimer -= Time.deltaTime;
    }
    public void ProcDepression()
    {
        if(GameManager.instance.playerBehavior.Player.MentalIllnesses.Contains(MentalIllnesses.depression))
        {
            if(Random.Range(0, 100) < chanceToProcDepression)
            {
                GameManager.instance.playerBehavior.Player.GainStress(1);
                if(GameManager.instance.playerBehavior.Player.StressLevel >= GameManager.instance.playerBehavior.Player.MaxStressLevel/2)
                {
                    GameManager.instance.playerBehavior.Player.TakeDMG(dmgFromDepression);
                    Debug.Log(GameManager.instance.playerBehavior.Player.Hp);
                }
            }
        }
    }
}
