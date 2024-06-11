using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragUI : MonoBehaviour, IDragHandler
{
    public Canvas canvas;

    private RectTransform rectTransform;
    private Vector3 normalPosition;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        normalPosition = new Vector3(0,0,0);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        if (rectTransform.anchoredPosition.x > 1000)
        {
            rectTransform.anchoredPosition = new Vector2(1000, rectTransform.anchoredPosition.y);
        }
        if (rectTransform.anchoredPosition.x < -1000)
        {
            rectTransform.anchoredPosition = new Vector2(-1000, rectTransform.anchoredPosition.y);
        }
        if (rectTransform.anchoredPosition.y > 500)
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 500);
        }
        if (rectTransform.anchoredPosition.y < -500)
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, -500);
        }
    }
    private void OnEnable()
    {
        gameObject.transform.localPosition = normalPosition;
    }
}
