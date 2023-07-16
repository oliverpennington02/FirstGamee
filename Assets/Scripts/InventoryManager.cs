using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int maxStackItems = 4;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public CameraMovement movement;

    private int selectedSlot = 0; // Start at the first slot (index 0)

private void Start()
{
    ChangeSelectedSlot(selectedSlot); // Call the ChangeSelectedSlot() method directly
}

void Update()
{
    float scrollDelta = Input.mouseScrollDelta.y;

    if (scrollDelta > 0f && selectedSlot != 3)
    {
        ChangeSelectedSlot(selectedSlot + 1);
        Debug.Log("Scroll wheel went up!");
    }
    else if (scrollDelta < 0f)
    {
        ChangeSelectedSlot(selectedSlot - 1);
        Debug.Log("Scroll wheel went down!");
    }
}

void ChangeSelectedSlot(int newValue)
{
    // Ensure the newValue is within valid range
    newValue = Mathf.Clamp(newValue, 0, inventorySlots.Length - 1);

    if (selectedSlot >= 0 && selectedSlot < inventorySlots.Length)
    {
        inventorySlots[selectedSlot].Deselect();
    }

    inventorySlots[newValue].Select();
    selectedSlot = newValue;
}
    public bool AddItem(Item item)
    {

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxStackItems && itemInSlot.item.stackable == true)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }

        return false;
    }

    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    public Item GetSelectedItem(bool use)
{
    InventorySlot slot = inventorySlots[selectedSlot];
    InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
    if (itemInSlot != null)
    {
        Item item = itemInSlot.item;
        if (use == true)
        {
            itemInSlot.count--;
            if (itemInSlot.count <= 0)
            {
                movement.canPlace = false;
                Destroy(itemInSlot.gameObject);
            }
            else
            {
                movement.canPlace = true;
                itemInSlot.RefreshCount();
            }
        }

        return item; // Return the selected item when one is found
    }

    return null; // Return null when no item is found
}
}
