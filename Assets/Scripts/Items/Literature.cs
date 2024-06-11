using UnityEngine;

[CreateAssetMenu(fileName = "New Literature", menuName = "Literature")]
public class Literature : Item
{
    [Multiline]
    public string text;
    public bool isRead = false;
    

}

