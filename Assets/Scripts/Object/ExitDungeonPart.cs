
using UnityEngine;

public class ExitDungeonPart : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.instance.turnOnWinPanel();
            Destroy(collision.gameObject);
        }
    }
}
