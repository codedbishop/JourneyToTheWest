using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HexTile
{
    private HexGridSystem gridSystem;
    private GridPosition gridPosition;

    private GameObject hexTileObject;

    private List<Unit> units;

    

    public HexTile(HexGridSystem gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
        units = new List<Unit>();

    }

    //attaches the visualGameObject to this object
    public void AttachGameObject(GameObject hexObject)
    {
        this.hexTileObject = hexObject;
    }

    public GameObject GetHexTileObject()
    {
        return this.hexTileObject;
    }

    public void SetUnit(Unit unit)
    {
        units.Add(unit);
        //this.unit = unit;
    }

    public void RemoveUnit(Unit unit)
    {
        
        units.Remove(unit);
        
    }

    public List<Unit> GetUnits()
    {
        return units;
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }
}
