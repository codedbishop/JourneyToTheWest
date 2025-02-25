using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;
using static Unity.Cinemachine.CinemachineFreeLookModifier;

public class HexTile
{
    private HexGridSystem gridSystem;
    private GridPosition gridPosition;

    private GameObject hexTileObject;

    private List<Unit> units;

    private List<TileMovePoint> tileMovePoints;

    private int selectidUnitCostToMove;

    public GameObject huntableObject;

    public List<UnitActions> actionsOnTile;

    public void Start()
    {
        
    }

    public HexTile(HexGridSystem gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
        units = new List<Unit>();
        actionsOnTile = new List<UnitActions>();

        CreateListOfTileMovePoints();


    }

    private void CreateListOfTileMovePoints()
    {
        tileMovePoints = new List<TileMovePoint>
        {
            new TileMovePoint(new Vector2(0,0)),
            new TileMovePoint(new Vector2(-2,0)),
            new TileMovePoint(new Vector2(2,0)),
            new TileMovePoint(new Vector2(-1,-1)),
            new TileMovePoint(new Vector2(1,-1)),
            new TileMovePoint(new Vector2(0,-2)),
            new TileMovePoint(new Vector2(-1,1)),
            new TileMovePoint(new Vector2(1,1)),
            new TileMovePoint(new Vector2(0,2)),
        };
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

    //this will need to get all the free pints and then select one of those ones 
    public TileMovePoint GetRandomAvailablePosition()
    {
        List<TileMovePoint> freeTileMovePoints = new List<TileMovePoint>();
        foreach (TileMovePoint tileMovePoint in tileMovePoints)
        {
            if (tileMovePoint.GetPointFree())
            {
                freeTileMovePoints.Add(tileMovePoint);
            }
        }

        if (freeTileMovePoints.Count > 0) 
        {
            int randomIndex = Random.Range(0, freeTileMovePoints.Count);
            return freeTileMovePoints[randomIndex];
        }
        else
        {
            return tileMovePoints[0];
        }
    }

    public void MoveOffTilePosition(TileMovePoint tileMovePoint)
    {
       for (int i = 0; i < tileMovePoints.Count; i++)
        {
            if (tileMovePoints[i] == tileMovePoint)
            {
                tileMovePoints[i].SetPointFree(true);
                return;
            }
        }
    }

    public void SetCostToMoveTo(int unitCost)
    {
        selectidUnitCostToMove = unitCost;
    }

    public int GetCostToMoveToTile()
    {
        return selectidUnitCostToMove;
    }

    public GameObject GetTileModifire()
    {      
        if (huntableObject != null)
        {
            return huntableObject;
        }
        else
        {
            return null;
        }
        //return huntableObject.GetComponent<> tileModifiers;
    }

    public void AddTileModifier(GameObject addGameObject)
    {
        huntableObject = addGameObject;
        UnitActions unitActions = addGameObject.GetComponent<ITileModifire>().GetUnitAction();

        //AddActionToTile(unitActions, addGameObject);

        actionsOnTile.Add(unitActions);

        //tileModifiers = new HuntableSO();
    }

    public GameObject GetHuntableObject()
    {
        return huntableObject;
    }

    public void RemoveHuntableObject()
    {
        huntableObject = null;
    }

    public List<UnitActions> GetInteractableOnTileList()
    {
        return actionsOnTile;
    }

    public void AddActionToTile(UnitActions action)
    {
        actionsOnTile.Add(action);
    }

    public void RemoveActionFromTile(UnitActions removeAction)
    {
        foreach (UnitActions action in actionsOnTile)
        {
            if(removeAction == action)
            {
                actionsOnTile.Remove(action);
                return;
            }
        }
    }
}

public class TileMovePoint
{
    Vector2 pointPostition;
    bool pointFree;

    public TileMovePoint(Vector2 position)
    {
        pointPostition = position;
        pointFree = true;
    }

    public bool GetPointFree()
    {
        return pointFree;
    }

    public void SetPointFree(bool isFree)
    {
        pointFree = isFree;
    }

    public Vector2 GetPointPosition()
    {
        return pointPostition;
    }

   
}


