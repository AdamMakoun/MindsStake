using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    private Player player;
    public Player Player { get { return player; } }
    [SerializeField]
    float maxHp = 100f;
    [SerializeField]
    float atkStat = 10f;
    [SerializeField]
    float defStat = 5f;
    [SerializeField]
    float maxStressLevel = 100;

    private float stressMulti = 1;

    private Schizophrenia schizophreniaScript;

    [SerializeField]
    private float timeToGainStress;

    private float currentTimeToGainStress;
    void Start()
    {
        player = new Player(maxHp, atkStat, defStat, maxStressLevel);

        schizophreniaScript = gameObject.GetComponent<Schizophrenia>();

        currentTimeToGainStress = timeToGainStress;
        schizophreniaScript.enabled = false;
        if(player.MentalIllnesses.Contains(MentalIllnesses.schizopschizofrenia)) { schizophreniaScript.enabled = true; }
    }

    // Update is called once per frame
    void Update()
    {
        LowerCDforPassiveStressGain();
        Die();
        CheckForNearbyPhobias();
    }

    public void Die()
    {
        if (player.CheckForDeath())
        {
            Destroy(gameObject); return;
        }
    }
    public void LowerCDforPassiveStressGain()
    {
        if (currentTimeToGainStress > 0)
            currentTimeToGainStress -= Time.deltaTime;
        else
        {
            player.GainStress(0.5f * stressMulti);
            currentTimeToGainStress = timeToGainStress;
        }
    }

    private void CheckForNearbyPhobias()
    {
        Collider2D[] colliderArray = Physics2D.OverlapCircleAll(gameObject.transform.position, 10f);
        stressMulti = 1f;
        foreach (Collider2D coll in colliderArray)
        {
            
            if(coll.TryGetComponent<Enemy>(out Enemy enemy))
            {
                foreach (Fobias phobia in player.Phobias)
                {
                    if (enemy.Phobias.Contains(phobia))
                    {
                        stressMulti += 0.5f;
                        if (stressMulti > 2.5f) stressMulti = 2.5f;
                    }

                }
            }
        }
        Debug.Log($"Stress Multi: {stressMulti}");
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Enemy"))
    //    {
    //        foreach (Fobias fobia in player.Phobias)
    //        {
    //            if (collision.gameObject.GetComponent<Enemy>().Phobias.Contains(fobia))
    //                stressMulti += 0.5f;

    //            if (stressMulti > 2.5f) stressMulti = 2.5f;
    //        }
    //    }
    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Enemy"))
    //    {
    //        stressMulti -= 0.5f;
    //        if (stressMulti < 1) stressMulti = 1;
    //    }
    //}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //the collision with hallucinations is solved here
        if (collision.gameObject.CompareTag("Hallucination"))
        {
            player.GainStress(10*stressMulti);
            Destroy(collision.gameObject);
        }
    }

}
