using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public Item item;
    [HideInInspector] public int count = 1;
    [Header("UI")]
    public Image image;
    public TextMeshProUGUI countText;

    public Transform initialParent;
    public Vector3 initialPosition;


    public void InitialiseItem(Item newItem)
    {
        item = newItem;
        image.sprite = newItem.image;
        RefreshCount();
    }

    public void RefreshCount()
    {
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        countText.raycastTarget = false;
        initialParent = transform.parent;
        initialPosition = transform.position;

        transform.SetParent(transform.root, true); // Set the worldPositionStays parameter to true to maintain the position
    }

    public void OnDrag(PointerEventData eventData)
    {
    RectTransformUtility.ScreenPointToLocalPointInRectangle(
        transform.parent.GetComponent<RectTransform>(),
        eventData.position,
        eventData.pressEventCamera,
        out Vector2 localPos
    );
    
    transform.localPosition = localPos;
    }

    public void OnEndDrag(PointerEventData eventData)
{
    image.raycastTarget = true;
    countText.raycastTarget = true;
    
    // Check if the pointer is hovering over any valid inventory slot
    InventorySlot inventorySlot = eventData.pointerEnter?.GetComponent<InventorySlot>();
    if (inventorySlot != null)
    {
        // Set the new parent as the inventory slot
        transform.SetParent(inventorySlot.transform, false);

        // Reset local position and anchoring to snap within the slot
        RectTransform rectTransform = transform.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector2.zero;
        rectTransform.localScale = Vector3.one;
    }
    else
    {
        // If not dropped onto a valid slot, return to the initial parent and position
        transform.SetParent(initialParent, false);
        transform.position = initialPosition;
    }
}
}
