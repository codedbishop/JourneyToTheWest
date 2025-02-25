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


    public void HandelSelectedAction(Vector3 actualMoucePosition)
    {
        GridPosition mouseGridPosition = LevelSystem.Instance.GetGridPosition(actualMoucePosition);//Get Grid position from mouse position
        selectedHexTile = LevelSystem.Instance.GetHexTile(mouseGridPosition); // set hex tile that was selected
        
        //check that there is a selected unit 
        if (moveableUnit != null)
        {
            HexTile lastHexPosition = selectedUnit.GetComponent<Unit>().GetHexTile();//hex postion unit was last one
                                                                                     
            //moves off the last tile move point and sets as free
            if(lastHexPosition != null)
            {
                TileMovePoint lastTileMovePoint = selectedUnit.GetComponent<Unit>().GetTileMovePoint();
                lastHexPosition.MoveOffTilePosition(lastTileMovePoint);
            }

            //gets new tile move point for unit to move to 
            TileMovePoint tileMovePoint = selectedHexTile.GetRandomAvailablePosition();

            //sets move point to moving unit
            moveableUnit.GetComponent<Unit>().SetTileMovePoint(tileMovePoint);
            
            //gets the vector of the tile move point
            Vector2 tileMovePosition = tileMovePoint.GetPointPosition();
           
            //gets the vector of the tile position
            Vector3 tilePosition = LevelSystem.Instance.GetHexWorldPosition(selectedHexTile);

            //offsets the move point from the vector of the tile 
            Vector3 moveLocation = new Vector3((tileMovePosition.x + tilePosition.x), (tilePosition.y), (tileMovePosition.y + tilePosition.z));

            //check if unit can make it to tile
            if (selectedHexTile.GetCostToMoveToTile() != 0 && moveableUnit.GetComponent<Unit>().GetEnergyAmount() >= selectedHexTile.GetCostToMoveToTile())
            {
                //sets the target destination for the unit 
                //SetUnitDestination(moveLocation);//***********************
                moveableUnit.GetComponent<Move>().SetTarget(moveLocation);

                //sets the move point to not free 
                tileMovePoint.SetPointFree(false);

                //removes energy to move
                moveableUnit.GetComponent<Unit>().RemoveEnergy(selectedHexTile.GetCostToMoveToTile());
                
                //deselcts the unit profile that moved
                UnitsOnMap.Instance.DeselectedAllUnitProfiles();
                //sets units to null
                selectedUnit = null;
                moveableUnit = null;

                //removes moveable visuals
                MoveableLocations.Instance.ClearMoveableHexTileVisuals();

                //reorder the unit profile list
                UnitsOnMap.Instance.ReorderUnitList();

                //ckech if there are still turns left
                TurnController.Instance.CheckTurnStat();

                //turns unit ui to active 
                PanelController.Instance.SetUntOnTilePanalActive();

                //sets it back to be able to click on unit 
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
        List<MoveAbleHexTileLocation> unitMoveableLocations = MoveableLocations.Instance.FindNumberOfMoves(moveableUnit.GetComponent<Unit>().GetHexTile().GetGridPosition(), moveableUnit.GetComponent<Unit>().GetEnergyAmount(), moveableUnit.GetComponent<Unit>().GetEnergyNeededToMove());

        proceduralGraphMover.target = moveableUnit.transform; // Sets the unit as the unit that will move for the AI pathfinder //Update the graph center (optional, depending on your ProceduralGraphMover configuration)
        UpdateGridCenter(moveableUnit.transform.position);
    }

    public void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit.gameObject;
        moveableUnit = unit.gameObject;

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
        moveableUnit.GetComponent<AIPath>().enabled = true; //turns pathfinding on 

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

        SetMoveableUnit();
        FindTilesUnitCanMoveTo();
    }

    public void SetMoveableUnit()
    {
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

    }

    public GameObject GetSelectedUnit()
    {
        return selectedUnit;
    }
}
