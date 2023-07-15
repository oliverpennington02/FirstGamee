using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int rows = 5;
    public int columns = 5;
    public float spacing = 10f; // Space between each inventory slot
    private string[,] inventory;

    // Reference to the UI panel where the inventory will be displayed
    public RectTransform inventoryPanel;

    // Reference to the UI element prefab to represent an inventory slot
    public GameObject inventorySlotPrefab;

    void Start()
    {
        inventory = new string[rows, columns];
        UpdateInventoryUI();
    }

    // Function to add an item to the inventory
    public bool AddItem(string item)
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                if (inventory[row, col] == null)
                {
                    inventory[row, col] = item;
                    UpdateInventoryUI();
                    return true;  // Item added successfully
                }
            }
        }
        return false;  // Inventory is full, item not added
    }

    // Function to move an item within the inventory
    public void MoveItem(int sourceRow, int sourceCol, int targetRow, int targetCol)
    {
        string sourceItem = inventory[sourceRow, sourceCol];
        string targetItem = inventory[targetRow, targetCol];
        inventory[sourceRow, sourceCol] = targetItem;
        inventory[targetRow, targetCol] = sourceItem;
        UpdateInventoryUI();
    }

    // Function to retrieve an item from the inventory
    public string GetItem(int row, int col)
    {
        return inventory[row, col];
    }

    // Function to remove an item from the inventory
    public void RemoveItem(int row, int col)
    {
        inventory[row, col] = null;
        UpdateInventoryUI();
    }

    // Function to update the UI with the current inventory state
    void UpdateInventoryUI()
    {
        // Clear previous UI elements
        foreach (Transform child in inventoryPanel)
        {
            Destroy(child.gameObject);
        }

        // Calculate the size of each inventory slot based on the panel's dimensions and spacing
        float slotWidth = (inventoryPanel.rect.width - (spacing * (columns - 1))) / columns;
        float slotHeight = (inventoryPanel.rect.height - (spacing * (rows - 1))) / rows;

        float startX = -(inventoryPanel.rect.width / 2) + (slotWidth / 2);
        float startY = inventoryPanel.rect.height / 2 - (slotHeight / 2);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                string item = inventory[row, col];

                // Instantiate UI element for the inventory slot
                GameObject slot = Instantiate(inventorySlotPrefab, inventoryPanel);

                // Set the position and size of the UI element based on the grid layout and spacing
                RectTransform slotTransform = slot.GetComponent<RectTransform>();
                slotTransform.sizeDelta = new Vector2(slotWidth, slotHeight);
                float posX = startX + (slotWidth + spacing) * col;
                float posY = startY - (slotHeight + spacing) * row;
                slotTransform.anchoredPosition = new Vector2(posX, posY);

                // Set the sprite, text, or other properties of the UI element based on the item in the inventory slot
                // You can customize this part to fit your specific UI design and item representation
                // For example, you could use an Image component and assign a sprite for each item.

                // Add any other necessary UI components and handle interactions with the inventory slots
            }
        }
    }

}
