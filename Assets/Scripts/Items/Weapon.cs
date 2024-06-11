using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : Item
{
    public float damage;
    public DamageType damageType;
    public bool isEquiped = false;
    public void Equip(Player player)
    {
        isEquiped=true;
        player.EquipWeapon(this);
        Debug.Log("Equiped a sword");

    }

}
