using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class InventoryUi : MonoBehaviour
{
    [SerializeField] Image[] iconSprite;

    public void SetInventorySlots(List<ItemSo> itemsInInventory)
    {
        for (int i = 0; i < itemsInInventory.Count; i++)
        {
            iconSprite[i].GetComponent<Image>().sprite = itemsInInventory[i].GetIcon();
        }
    }

    public void ResetInventorySlots()
    {
        foreach(Image image in iconSprite)
        {
            image.sprite = null;
        }
    }
}
