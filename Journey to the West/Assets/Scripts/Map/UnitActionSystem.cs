using Pathfinding;
using System;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;


public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance;

    [SerializeField] GameObject mousPosition;

    [SerializeField] GameObject unit;

    [SerializeField] GameObject selectedUnit;

    HexTile selectedHexTile;

    public ProceduralGraphMover proceduralGraphMover;

    public event EventHandler OnHexTileSelected;//The UnitOnTilePanel listines to this event to update the panel of units on the selected tile 

    public enum ActionState { none, move, eat };
    public ActionState actionState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
    }


    // Update is called once per frame
    void Update()
    {

        Vector3? moucePosition = MouseWorld.GetPosition();
        if (moucePosition != null)
        {
            Vector3 actualMoucePosition = moucePosition.Value;
            mousPosition.transform.position = LevelSystem.Instance.GetHexPositionFromWorldPosition(actualMoucePosition);


            HexTile huveringHexTile = LevelSystem.Instance.GetHexTileFromWorldPosition(actualMoucePosition);

            if(huveringHexTile == null)
            {
                return;
            }

            if (huveringHexTile.GetCostToMoveToTile() > 0)
            {
                mousPosition.GetComponent<MoveStatUI>().SetTurnsNeeded(huveringHexTile.GetCostToMoveToTile(), selectedUnit.GetComponent<Unit>().GetEnergyAmount());
            }
            else
            {
                mousPosition.GetComponent<MoveStatUI>().SetTurnNeedsToInactive();
            }


            if (Input.GetMouseButtonDown(0))
            {
                PreformAction(actualMoucePosition);
                //HandelSelectedAction(actualMoucePosition);
            }

            if (Input.GetMouseButton(1))
            {
                //GridPosition gridPosition = new GridPosition(0, 0);

                //LevelSystem.Instance.GetHexTile(gridPosition).SetUnit(unit.GetComponent<Unit>());

            }
        }
    }

    public void CheckIfUnitWasClickedOn(Vector3 actualMoucePosition)
    {
        GridPosition mouseGridPosition = LevelSystem.Instance.GetGridPosition(actualMoucePosition);
        selectedHexTile = LevelSystem.Instance.GetHexTile(mouseGridPosition);
        if (selectedUnit != null)
        {
            PanelController.Instance.SetUnitActionPanelActive();
            //selectedUnit.GetComponent<Unit>().CreateActions();
        }
        else
        {
            OnHexTileSelected?.Invoke(this, EventArgs.Empty);
        }
    }

    public void PreformAction(Vector3 actualMoucePosition)
    {
        switch (actionState)
        {
            case ActionState.none:
                CheckIfUnitWasClickedOn(actualMoucePosition);

                break;
            case ActionState.move:
                HandelSelectedAction(actualMoucePosition);
                break;

            case ActionState.eat:
                selectedUnit.GetComponent<Unit>().RestoreHunger(10);
                actionState = ActionState.none;
                break;
        }
    }


    private void HandelSelectedAction(Vector3 actualMoucePosition)
    {
        GridPosition mouseGridPosition = LevelSystem.Instance.GetGridPosition(actualMoucePosition);
        selectedHexTile = LevelSystem.Instance.GetHexTile(mouseGridPosition);
        if (selectedUnit != null)
        {
            HexTile lastHexPosition = selectedUnit.GetComponent<Unit>().GetHexTile();
            if(lastHexPosition != null)
            {
                TileMovePoint lastTileMovePoint = selectedUnit.GetComponent<Unit>().GetTileMovePoint();
                lastHexPosition.MoveOffTilePosition(lastTileMovePoint);
            }

            TileMovePoint tileMovePoint = selectedHexTile.GetRandomAvailablePosition();
            selectedUnit.GetComponent<Unit>().SetTileMovePoint(tileMovePoint);

            Vector2 tileMovePosition = tileMovePoint.GetPointPosition();
            //Debug.Log(tileMovePosition);
            Vector3 tilePosition = LevelSystem.Instance.GetHexWorldPosition(selectedHexTile);

            Vector3 moveLocation = new Vector3((tileMovePosition.x + tilePosition.x), (tilePosition.y), (tileMovePosition.y + tilePosition.z));

            //check if unit can make it to tile
            if (selectedHexTile.GetCostToMoveToTile() != 0 && selectedUnit.GetComponent<Unit>().GetEnergyAmount() >= selectedHexTile.GetCostToMoveToTile())
            {
                SetUnitDestination(moveLocation);
                tileMovePoint.SetPointFree(false);
                //selectedUnit.GetComponent<MoveAction>().SetTarget(selectedHexTile);
                selectedUnit.GetComponent<Unit>().RemoveEnergy(selectedHexTile.GetCostToMoveToTile());
                UnitsOnMap.Instance.DeselectedAllUnitProfiles();
                selectedUnit = null;
                MoveableLocations.Instance.ClearMoveableHexTileVisuals();
                UnitsOnMap.Instance.ReorderUnitList();
                TurnController.Instance.CheckTurnStat();
                PanelController.Instance.SetUntOnTilePanalActive();
                actionState = ActionState.none;
            }  
        }
        else
        {
            OnHexTileSelected?.Invoke(this, EventArgs.Empty);
        }
    }

    public void FindTilesUnitCanMoveTo()
    {
        Debug.Log(selectedUnit.transform.name + " needs " + selectedUnit.GetComponent<Unit>().GetEnergyNeededToMove() + " energy needed to move");

        List<MoveAbleHexTileLocation> unitMoveableLocations = MoveableLocations.Instance.FindNumberOfMoves(selectedUnit.GetComponent<Unit>().GetHexTile().GetGridPosition(), selectedUnit.GetComponent<Unit>().GetEnergyAmount(), selectedUnit.GetComponent<Unit>().GetEnergyNeededToMove());

        proceduralGraphMover.target = selectedUnit.transform; // Update the graph center (optional, depending on your ProceduralGraphMover configuration)
        UpdateGridCenter(selectedUnit.transform.position);
    }

    public void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit.gameObject;

        PanelController.Instance.SetUnitActionPanelActive();
        selectedUnit.GetComponent<Unit>().CreateActions();

        UnitsOnMap.Instance.SetActiveUnit(selectedUnit);

    }

    public HexTile GetSelectedHexTile()
    {
        return selectedHexTile;
    }

    void UpdateGridCenter(Vector3 position)
    {
        Vector3 newCenter = position;
        GridGraph gridGraph = AstarPath.active.data.gridGraph;

        gridGraph.center = newCenter;
        AstarPath.active.Scan();
    }

    void SetUnitDestination(Vector3 targetPosition)
    {
        //Assuming the unit has an AIPath component for pathfinding and movement
        AIPath aiPath = selectedUnit.GetComponent<AIPath>();
        if (aiPath != null)
        {
            // Set the target position for the AIPath component
            aiPath.destination = targetPosition;
            aiPath.SearchPath(); // Request a path update
        }

    }

    public void SetActionStateToMove()
    {
        actionState = ActionState.move;
        FindTilesUnitCanMoveTo();
        Debug.Log("Move Action");
    }

    public GameObject GetSelectedUnit()
    {
        return selectedUnit;
    }
}
