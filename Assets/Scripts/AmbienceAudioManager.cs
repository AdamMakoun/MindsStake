
using UnityEngine;

public class AmbienceAudioManager : MonoBehaviour
{
    public static AmbienceAudioManager Instance { get; private set; }
    [SerializeField] private AudioSource creepySound;
    [SerializeField] private float creepySoundTimer = 1;
    private float currentCreepySoundTimer = 0;
    [SerializeField] private AudioSource depressionSound;
    [SerializeField] private AudioSource hitInCombatSound;
    [SerializeField] private AudioSource startCombatSound;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
   
    private void Update()
    {
        
        
        if (currentCreepySoundTimer <= 0)
        {
            if (GameManager.instance.playerBehavior.Player.StressLevel > 30)
            {
                if (Random.Range(0, 100) < 10)
                {
                    if(!creepySound.isPlaying)
                    PlayCreepySound();
                }
                currentCreepySoundTimer = creepySoundTimer;
            }
        }
        else
        {
            currentCreepySoundTimer -= Time.deltaTime;
        }
    }
    public void PlayCreepySound()
    {
        creepySound.Play();
        currentCreepySoundTimer = 0;
    }
    public void StartDepressionSoudLoop()
    {
        depressionSound.loop = true;

        depressionSound.Play();
    }
    public void PlayHitInCombatSound()
    {
        hitInCombatSound.Play();
    }
    public void PlayStartCombatSound()
    {
        startCombatSound.Play();
    }
}
