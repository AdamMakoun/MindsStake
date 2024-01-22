using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject playerGameObject;
    PlayerBehavior playerBehavior;
    public Image stressBar;

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
        playerBehavior = playerGameObject.GetComponent<PlayerBehavior>();
    }
    private void Update()
    {
        UpdateStressBar();
    }
    public void UpdateStressBar()
    {
        stressBar.fillAmount = playerBehavior.Player.StressLevel / playerBehavior.Player.MaxStressLevel;
    }
}
