using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressManager : MonoBehaviour
{
    private static StressManager instance;
    public static StressManager Instance {  get { return instance; } }

    Player player;
    private int tier = 1;
    public int Tier { get { return tier; } }
    // Start is called before the first frame update
    private void Awake()
    {
        if(instance == null)
            instance = this;
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeStressTier()
    {
        if (player == null) player = GameManager.instance.playerGameObject.GetComponent<PlayerBehavior>().Player;
        if (player.StressLevel >= player.MaxStressLevel)
        {
            if(tier < 3)
            {
                player.ResetStress();
                tier++;
            }
        }
        if(player.StressLevel <= 0)
        {
            if(tier > 1)
            {
                player.ResetStress();
                tier--;
            }

        }
    }

}
