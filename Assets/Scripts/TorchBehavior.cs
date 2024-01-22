using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchBehavior : MonoBehaviour
{
    [SerializeField]
    private float regainingStressCd = 1f;

    private float remainingRegainingStressCd = 0f;

    [SerializeField]
    private float stressToRegain = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LowerCD();
        FindPlayer();
    }
    private void FindPlayer()
    {
        Collider2D[] physics2D = Physics2D.OverlapCircleAll(gameObject.transform.position, 5f);
        foreach(Collider2D collider in physics2D)
        {
            if(collider.TryGetComponent<PlayerBehavior>(out PlayerBehavior playerBeh))
            {
                Debug.Log("found Player");
                if(remainingRegainingStressCd <= 0)
                {
                    Debug.Log("regained players stress");
                    playerBeh.Player.regainStress(stressToRegain);
                    remainingRegainingStressCd = regainingStressCd;
                }
            }
        }
    }
    private void LowerCD()
    {
        if(remainingRegainingStressCd > 0)
        {
            remainingRegainingStressCd -= Time.deltaTime;
        }
    }
}
