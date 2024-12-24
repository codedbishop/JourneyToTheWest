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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
    }


    // Update is called once per frame
    void Update()
    {

        Vector3? moucePosition = MouseWorld.GetPosition();
        if (moucePosition.HasValue)
        {
            Vector3 actualMoucePosition = moucePosition.Value;
            mousPosition.transform.position = LevelSystem.Instance.GetHexPositionFromWorldPosition(actualMoucePosition);

            if (Input.GetMouseButtonDown(0))
            {
                HandelSelectedAction(actualMoucePosition);
            }

            if (Input.GetMouseButton(1))
            {
                //GridPosition gridPosition = new GridPosition(0, 0);

                //LevelSystem.Instance.GetHexTile(gridPosition).SetUnit(unit.GetComponent<Unit>());

            }
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
            //Vector3 moveLocation = LevelSystem.Instance.GetHexWorldPosition(selectedHexTile);
            SetUnitDestination(moveLocation);
            tileMovePoint.SetPointFree(false);
            //selectedUnit.GetComponent<MoveAction>().SetTarget(selectedHexTile);
            UnitsOnMap.Instance.DeselectedAllUnitProfiles();
            selectedUnit = null;
        }
        else
        {
            OnHexTileSelected?.Invoke(this, EventArgs.Empty);
        }
    }

    public void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit.gameObject;

        proceduralGraphMover.target = selectedUnit.transform; // Update the graph center (optional, depending on your ProceduralGraphMover configuration)
        UpdateGridCenter(selectedUnit.transform.position);
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
}
