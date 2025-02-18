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

    [SerializeField] GameObject moveableUnit;

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
                Debug.Log("Moveing");
                break;

            //case ActionState.eat:
            //    selectedUnit.GetComponent<Human>().RestoreHunger(10);
            //    actionState = ActionState.none;
            //    break;
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

            //GameObject moveableUnit;

            //Debug.Log(selectedUnit.GetComponent<Unit>().GetMount() + " mount");

            //if (selectedUnit.GetComponent<Unit>().GetMount() != null)
            //{
            //    Debug.Log("Mount found");
            //    moveableUnit = selectedUnit.GetComponent<Unit>().GetMount().GameObject();
            //    Debug.Log(moveableUnit);
            //}
            //else
            //{
            //    //Debug.Log("Mount not found");
            //    moveableUnit = selectedUnit;
            //}

            TileMovePoint tileMovePoint = selectedHexTile.GetRandomAvailablePosition();
            selectedUnit.GetComponent<Unit>().SetTileMovePoint(tileMovePoint);

            Vector2 tileMovePosition = tileMovePoint.GetPointPosition();
            //Debug.Log(tileMovePosition);
            Vector3 tilePosition = LevelSystem.Instance.GetHexWorldPosition(selectedHexTile);

            Vector3 moveLocation = new Vector3((tileMovePosition.x + tilePosition.x), (tilePosition.y), (tileMovePosition.y + tilePosition.z));

            //check if unit can make it to tile
            if (selectedHexTile.GetCostToMoveToTile() != 0 && moveableUnit.GetComponent<Unit>().GetEnergyAmount() >= selectedHexTile.GetCostToMoveToTile())
            {
                SetUnitDestination(moveLocation);
                tileMovePoint.SetPointFree(false);
                //selectedUnit.GetComponent<MoveAction>().SetTarget(selectedHexTile);
                moveableUnit.GetComponent<Unit>().RemoveEnergy(selectedHexTile.GetCostToMoveToTile());
                UnitsOnMap.Instance.DeselectedAllUnitProfiles();
                selectedUnit = null;
                moveableUnit = null;
                MoveableLocations.Instance.ClearMoveableHexTileVisuals();
                UnitsOnMap.Instance.ReorderUnitList();
                TurnController.Instance.CheckTurnStat();
                PanelController.Instance.SetUntOnTilePanalActive();
                actionState = ActionState.none;
            }  
        }
        else
        {
            //sets up buttons when unit is clicked on 
            OnHexTileSelected?.Invoke(this, EventArgs.Empty);
        }
    }

    public void FindTilesUnitCanMoveTo()
    {
        List<MoveAbleHexTileLocation> unitMoveableLocations = MoveableLocations.Instance.FindNumberOfMoves(selectedUnit.GetComponent<Unit>().GetHexTile().GetGridPosition(), selectedUnit.GetComponent<Unit>().GetEnergyAmount(), selectedUnit.GetComponent<Unit>().GetEnergyNeededToMove());

        proceduralGraphMover.target = moveableUnit.transform; // Sets the unit as the unit that will move for the AI pathfinder //Update the graph center (optional, depending on your ProceduralGraphMover configuration)
        UpdateGridCenter(selectedUnit.transform.position);
    }

    public void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit.gameObject;

        PanelController.Instance.SetUnitActionPanelActive();
        selectedUnit.GetComponent<Unit>().CreateActions();

        UnitsOnMap.Instance.SetActiveUnit(selectedUnit);

    }

    public void UpdateUnitActions()
    {
        selectedUnit.GetComponent<Unit>().CreateActions();
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
        AIPath aiPath = moveableUnit.GetComponent<AIPath>();

        if (aiPath != null)
        {
            // Set the target position for the AIPath component
            aiPath.destination = targetPosition;
            aiPath.SearchPath(); // Request a path update
        }

    }

    //sets the unit that is active for path finding purposes 
    public void SetActionStateToMove()
    {
        actionState = ActionState.move;

        Human human = selectedUnit.GetComponent<Human>();

        //checks if selected unit is a human or something else 
        if (human != null)
        {
            //gets the mount unit is on 
            Mounts mount = selectedUnit.GetComponent<Human>().GetMount();
            if (mount != null)
            {
                //if there is a mount swaps it so that path finding works of mount 
                moveableUnit = mount.GameObject();
            }
            else
            {
                //if no mount sets it so that the unit is what the path finding is based off
                moveableUnit = selectedUnit;
            }
        }
        else
        {
            moveableUnit = selectedUnit;
        }

        FindTilesUnitCanMoveTo();
    }

    public GameObject GetSelectedUnit()
    {
        return selectedUnit;
    }
}
