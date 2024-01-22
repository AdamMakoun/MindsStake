using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBehavior : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("heart collided");
        if (collision.gameObject.CompareTag("attack"))
        {
            CombatManager.Instance.attackPlayer();

            Debug.Log("heart collided with attack");

            CombatManager.Instance.RemoveAttackFromSeq(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
