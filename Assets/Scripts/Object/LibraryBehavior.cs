using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryBehavior : MonoBehaviour
{
    [SerializeField]
    private List<Literature> literatureList = new List<Literature>();

    private bool isInRange = false;
    private bool isLooted = false;

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && isInRange)
        {
            if(isLooted == false)
            {
                DiaryManager.Instance.AddLiterature(literatureList[Random.Range(0, literatureList.Count)]);
                isLooted = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isInRange = false;
        }
    }
}
