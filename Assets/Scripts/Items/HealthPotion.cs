using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Health Potion", menuName = "Health Potion")]
public class HealthPotion : Item
{
    public float hpToHeal;
    private int usesLeft = 3;
    public int UsesLeft { get { return usesLeft; } private set { usesLeft = usesLeft; } }

    public void Use()
    {
        GameManager.instance.playerBehavior.Player.RegainHp(this.hpToHeal);
        usesLeft--;
        updateDesc();
        if (usesLeft == 0) 
        {
            InventoryManager.Instance.RemoveItemFromInv(this);
            InventoryManager.Instance.UpdateInventoryUI();
        }

    }
    public void updateDesc()
    {
        description = $"Heals: {hpToHeal}\nUses Left: {usesLeft}";
    }
}
