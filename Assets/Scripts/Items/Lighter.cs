using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Lighter", menuName = "Lighter")]
public class Lighter : Item
{
    private int usesLeft = 3;
    public int UsesLeft { get { return usesLeft; } private set { usesLeft = usesLeft; } }

    public void Use()
    {
        if(GameManager.instance.playerBehavior.Lighter.activeSelf == false)
        {
            GameManager.instance.playerBehavior.Lighter.SetActive(true);
            usesLeft--;
            updateDesc();
            if (usesLeft == 0)
            {
                InventoryManager.Instance.RemoveItemFromInv(this);
                InventoryManager.Instance.UpdateInventoryUI();
            }
        }
        
    }
    public void updateDesc()
    {
        description = $"Uses Left: {usesLeft}";
    }
}
