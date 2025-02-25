using System.Collections.Generic;
using UnityEngine;

public class UnitInventory : MonoBehaviour
{
    [SerializeField] int inventorySize;
    [SerializeField] List<ItemSo> itemsInInventory;
   

    public void AddItemToInventory(ItemSo item)
    {
        if (itemsInInventory.Count < inventorySize)
        {
            itemsInInventory.Add(item);

        }
        else
        {
            //**this should return a bool so it knows to not add item or destroy
        }
    }

    public void RemoveFoodItemFromInventory()
    {
        for (int i = itemsInInventory.Count - 1; i >= 0; i--)
        {
            if (itemsInInventory[i] is FoodSo)
            {
                itemsInInventory.RemoveAt(i);
                return;
            }
        }
    }

    public List<ItemSo> GetInventory()
    {
        return itemsInInventory;
    }

    public bool CheckForFood()
    {
        foreach (ItemSo inventoryItem in itemsInInventory)
        {
            if (inventoryItem is FoodSo)
            {
                return true;
            }
        }
        return false;
    }

    public bool CheckForInventory(ItemSo item)
    {
        foreach (ItemSo inventoryItem in itemsInInventory)
        {
            if (inventoryItem == item)
            {
                return true;
            }
        }
        return false;
    }
}
