using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemClickHandler : MonoBehaviour, IPointerClickHandler
{
    private Item item;
    [SerializeField]
    private Image itemIcon;
    public void SetItem(Item newItem)
    {
        item = newItem;
        SetItemIcon(newItem);
    }

    private void SetItemIcon(Item newItem) 
    { 
        if(itemIcon != null)
        itemIcon.sprite = newItem.icon;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        // Notify the InventoryManager when an item is clicked
        InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
        if (inventoryManager != null)
        {
            inventoryManager.OnItemClick(item);
        }
    }
}
