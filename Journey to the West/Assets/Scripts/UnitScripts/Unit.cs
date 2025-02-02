using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Unit : MonoBehaviour
{

    [SerializeField] string name;
    [SerializeField] int energyMax;

    [SerializeField] public int energyAmount;

    [SerializeField] int hunger;
    [SerializeField] int maxHunger;

    [SerializeField] public int moral;

    [SerializeField] HexTile hexTileOn;

    [SerializeField] TileMovePoint tileMovePoint;

    [SerializeField] int energyNeededToMove;

    public List<ItemSo> inventory;

    [SerializeField] List<UnitActions> unitActions;

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



    /// <summary>
    /// /////////////
    /// </summary>
    /// <returns></returns>

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

    //public void ResetEnergy()
    //{
    //    int neededEnergy = energyMax - energyAmount;
    //    if(hunger >= neededEnergy / 2)
    //    {
    //        hunger -= neededEnergy / 2;
    //        energyAmount = energyMax;
    //    }
    //    else
    //    {
    //        energyAmount += hunger * 2;
    //        hunger = 0;
    //    }
    //}

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

    public int GetMoral()
    {
        return moral;
    }

    public void UpdateMoral(int moralChange)
    {
        moral += moralChange;
        UnitsOnMap.Instance.UpdateUnitProfileMoral();
    }

    public virtual void GetEnergyLevel()
    {
        Debug.Log("Not Checking settler energy");
    }
    //{
    //    if(moral >= 90)
    //    {
    //        energyAmount = 80;
    //    }
    //    else if(moral >= 70)
    //    {
    //        energyAmount = 75;
    //    }
    //    else if (moral >= 50)
    //    {
    //        energyAmount = 60;
    //    }
    //    else if (moral >= 30)
    //    {
    //        energyAmount = 40;
    //    }

    //}

    public void CreateActions()
    {
        PanelController.Instance.DestroyButtons();
        foreach (UnitActions actions in unitActions)
        {
            Debug.Log("Creating Action");
            actions.CanPreformAction();
            //PanelController.Instance.AddAction(actions);
        }
    }

}
