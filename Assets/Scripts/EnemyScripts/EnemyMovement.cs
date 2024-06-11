using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private GameObject player;
    private Enemy enemyScript;
    Animator animator;
    void Start()
    {
        player = GameManager.instance.playerGameObject;

        animator = gameObject.GetComponent<Animator>();
    }


    void Update()
    {
        if(player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            if(animator != null)
            {
                animator.SetBool("isMoving", true);
            }
        }
        else
        {
           if(animator != null)
           {
                    animator.SetBool("isMoving", false);
           }
        }
    }
    public void StopMovement()
    {
        speed = 0;
    }
    public void FindPlayer(GameObject player)
    {
        this.player = player;
    }
    public void LosePlayer()
    {
        player = null;
    }
}
