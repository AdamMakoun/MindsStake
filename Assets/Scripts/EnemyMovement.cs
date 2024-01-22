using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private GameObject player;
    private Enemy enemyScript;
    void Start()
    {
        player = GameManager.instance.playerGameObject;
    }

    
    void Update()
    {
        if(player != null)
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void FindPlayerInRange()
    {
        //todo find optimalized way to find player and target him
    }
}
