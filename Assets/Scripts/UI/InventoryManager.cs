using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager inventoryManager;
    public static InventoryManager Instance { get { return inventoryManager; } }
    public GameObject itemPrefab;
    public Transform gridTransform;
    private Item selectedItem;
    public Image selectedItemImage;
    public TMP_Text selectedItemDescription;
    public TMP_Text selectedItemName;
    public Item testItem;
    [SerializeField]
    private GameObject inventory;
    
    
    private bool isOpened = false;

    private List<Item> inventoryItems = new List<Item>();
    public List<Item> InventoryItems { get { return inventoryItems; } }
    private void Awake()
    {
        if (inventoryManager == null)
        {
            inventoryManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void itemUse()
    {
        if(selectedItem is Weapon)
        {
            Weapon weapon = (Weapon)selectedItem;
            if(GameManager.instance.playerBehavior.Player.EquipedWeapon != null)
            {
                GameManager.instance.playerBehavior.Player.EquipedWeapon.isEquiped = false;
            }
            weapon.Equip(GameManager.instance.playerBehavior.Player);
            selectedItemDescription.text = $"{weapon.description}\nDamage: {weapon.damage}\nDamage Type: {weapon.damageType.ToString()}\nEquiped";
        }
        else if(selectedItem is HealthPotion)
        {
            HealthPotion healthPotion = (HealthPotion)selectedItem;
            healthPotion.Use();
            selectedItemDescription.text = selectedItem.description;

            if (healthPotion.UsesLeft == 0)
            {
                selectedItem = null;
                selectedItemDescription.text = "Descrition of Item";
                selectedItemImage = null;
                selectedItemName.text = "Name";
            }

        }
        else if(selectedItem is Lighter)
        {
            Lighter lighter = (Lighter)selectedItem;
            lighter.Use();
            selectedItemDescription.text = selectedItem.description;

            if (lighter.UsesLeft == 0)
            {
                selectedItem = null;
                selectedItemDescription.text = "Descrition of Item";
                selectedItemImage = null;
                selectedItemName.text = "Name";
            }
        }
        else if(selectedItem != null)
        {
            //todo
        }
    }
    private void Start()
    { 
        inventory.SetActive(false);
        

    }
    private void Update()
    {
        CloseInv();
    }
    private void CloseInv()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            inventory.SetActive(false);
            isOpened = false;
        }
    }

    public void OpenCloseInv()
    {
        isOpened = !isOpened;
        inventory.SetActive(isOpened);
        UpdateInventoryUI();
    }
    private void AddItemToInv(Item item)
    {
        inventoryItems.Add(item);
        if(isOpened)
        UpdateInventoryUI();
    }
    public void OnItemClick(Item item)
    {
        selectedItem = item;
        selectedItemDescription.text = selectedItem.description;
        selectedItemImage.sprite = selectedItem.icon;
        selectedItemName.text = selectedItem.name;
        if(item is Weapon)
        {
            Weapon weapon = (Weapon)item;
            selectedItemDescription.text = $"\nDamage: {weapon.damage}\nDamage Type: {weapon.damageType.ToString()}\n";
            if (weapon.isEquiped)
            {
                selectedItemDescription.text += "Equiped";
            }
            else
            {
                selectedItemDescription.text += "Unequiped";
            }
        }
    }
    public void RemoveItemFromInv(Item item)
    {
        inventoryItems.Remove(item);
    }
    public void UpdateInventoryUI()
    {
        foreach (Transform child in gridTransform)
        {
            Destroy(child.gameObject);
        }

        foreach (Item item in inventoryItems)
        {
            GameObject itemObject = Instantiate(itemPrefab, gridTransform);
            ItemClickHandler clickHandler = itemObject.GetComponent<ItemClickHandler>();
            clickHandler.SetItem(item);
        }
    }
    public void AddItemToInventory(Item item)
    {
        inventoryItems.Add(item);
        UpdateInventoryUI();
    }
    
}
