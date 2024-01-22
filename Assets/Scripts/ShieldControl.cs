using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldControl : MonoBehaviour
{
    //this is a script for the shiled used in combat to block attacks
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.eulerAngles = new Vector3(0, 0, 180);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.eulerAngles = new Vector3(0, 0, -90);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("attack")) {
            CombatManager.Instance.PlayerStressRegain();
            CombatManager.Instance.RemoveAttackFromSeq(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }

}
