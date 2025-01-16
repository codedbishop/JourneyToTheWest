using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Unit : MonoBehaviour
{

    [SerializeField] string name;
    [SerializeField] int energyMax;

    [SerializeField] int energyAmount;

    [SerializeField] int hunger;
    [SerializeField] int maxHunger;

    [SerializeField] HexTile hexTileOn;

    [SerializeField] TileMovePoint tileMovePoint;

    [SerializeField] int energyNeededToMove;

    public List<ItemSo> inventory; 

    private void Update()
    {
        HexTile newHexTile = LevelSystem.Instance.GetHexTileFromWorldPosition(transform.position);
        if (newHexTile != hexTileOn)
        {
            HexTile oldHexTile = hexTileOn;
            hexTileOn = newHexTile;

            LevelSystem.Instance.SetUnitOnTile(this, oldHexTile, hexTileOn);
        }
    }

    public string GetName()
    {
        return name;
    }

    public int GetEnergyAmount()
    {
        return energyAmount;
    }

    public void RemoveEnergy(int energyToRemove)
    {
        energyAmount -= energyToRemove;
        UnitsOnMap.Instance.UpdateUnitProfileEnergy();
    }

    public void SetTileMovePoint(TileMovePoint tileMovePoint)
    {
        this.tileMovePoint = tileMovePoint;
    }

    public TileMovePoint GetTileMovePoint()
    {
        return tileMovePoint;
    }

    public HexTile GetHexTile()
    {
        return hexTileOn;
    }

    public void ResetEnergy()
    {
        int neededEnergy = energyMax - energyAmount;
        if(hunger >= neededEnergy / 2)
        {
            hunger -= neededEnergy / 2;
            energyAmount = energyMax;
        }
        else
        {
            energyAmount += hunger * 2;
            hunger = 0;
        }
    }

    public int GetEnergyNeededToMove()
    {
        return energyNeededToMove;
    }

    public int GetHunger()
    {
        return hunger;
    }

    public void RestoreHunger(int hungerToRestore)
    {
        hunger += hungerToRestore;
        energyAmount -= 10;
    }

    public List<ItemSo> GetInventory()
    {
        return inventory;
    }

    public void AddItemToInventory(ItemSo itemToAdd)
    {
        if(inventory.Count < 7)
        {
            inventory.Add(itemToAdd);

        }
        else
        {

        }
    }

    public void RemoveFoodItemFromInventory()
    {
        for (int i = inventory.Count - 1; i >=0; i--)
        {
            if (inventory[i] is FoodSo)
            {
                inventory.RemoveAt(i);
                return;
            }
        }
    }

    public bool CheckForFood()
    {
        foreach (ItemSo inventoryItem in inventory)
        {
            if(inventoryItem is FoodSo)
            {
                return true;
            }
        }
        return false;
    }

    public bool CheckForInventory(ItemSo item)
    {
        foreach (ItemSo inventoryItem in inventory)
        {
            if(inventoryItem == item)
            {
                return true;
            }
        }
        return false;
    }

}
