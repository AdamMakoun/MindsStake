using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChestBehavior : MonoBehaviour
{
    bool canCollectItem = false;
    [SerializeField]
    List<Sprite> potionSprites = new List<Sprite>();
    [SerializeField]
    List<Sprite> weaponSprites = new List<Sprite>();
    List<Sprite> bookSprites = new List<Sprite>();
    [SerializeField]
    Sprite lighterSprite;

    private bool isLooted = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canCollectItem)
        {
            if (!isLooted)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    switch (UnityEngine.Random.Range(0, 100)){
                        case int n when (n <= 10):
                            InventoryManager.Instance.AddItemToInventory(CreateWeapon());
                            isLooted = true;
                            break;
                        case int n when (n > 10 && n <= 30):
                            InventoryManager.Instance.AddItemToInventory(CreateHealthPotion());
                            isLooted = true;
                            break;
                        default: Lighter lighter = ScriptableObject.CreateInstance<Lighter>();
                            lighter.id = GetUniqueID();
                            lighter.itemName = "Lighter";
                            lighter.description = "This shall provide light for some time.";
                            lighter.icon = lighterSprite;
                            InventoryManager.Instance.AddItemToInventory(lighter);
                            isLooted = true;
                            break;
                    }
                }
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canCollectItem = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canCollectItem = false;
        }
    }
    private Weapon CreateWeapon()
    {
        Weapon weapon = ScriptableObject.CreateInstance<Weapon>();
        weapon.id = GetUniqueID();
        weapon.itemName = "Weapon";
        weapon.description = "";
        weapon.icon = weaponSprites[UnityEngine.Random.Range(0, weaponSprites.Count)];
        weapon.damage = UnityEngine.Random.Range(10,20);
        int randomIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(DamageType)).Cast<int>().Max());
        weapon.damageType = (DamageType)Enum.GetValues(typeof(DamageType)).GetValue(randomIndex);
        weapon.isEquiped = false;
        Debug.Log("Made item");
        return weapon;
    }
    private HealthPotion CreateHealthPotion()
    {
        HealthPotion healthPotion = ScriptableObject.CreateInstance<HealthPotion>();
        healthPotion.id = GetUniqueID();
        healthPotion.name = "Weird Vial";
        healthPotion.icon = potionSprites[UnityEngine.Random.Range(0, potionSprites.Count)];
        healthPotion.hpToHeal = UnityEngine.Random.Range(10,20);
        healthPotion.updateDesc();
        return healthPotion;
    }
    private int GetUniqueID()
    {
        return Mathf.Abs(System.Guid.NewGuid().GetHashCode());
    }
}
