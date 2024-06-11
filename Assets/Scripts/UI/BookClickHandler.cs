using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class BookClickHandler : MonoBehaviour , IPointerClickHandler
{
    private Literature book;
    [SerializeField]
    public TMP_Text indexTitle;
    public void SetBook(Literature newBook)
    {
        book = newBook;
        SetIndexTitle(book);
    }

    private void SetIndexTitle(Literature book)
    {
        if (indexTitle != null)
            indexTitle.text = book.itemName;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        // Notify the InventoryManager when an item is clicked
        if (DiaryManager.Instance != null)
        {
            DiaryManager.Instance.OnIndexClick(book);
        }
    }
}
