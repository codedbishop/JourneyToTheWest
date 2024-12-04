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
        if(moucePosition.HasValue)
        {
            Vector3 actualMoucePosition = moucePosition.Value;
            mousPosition.transform.position = LevelSystem.Instance.GetHexPositionFromWorldPosition(actualMoucePosition);

            if (Input.GetMouseButtonDown(0))
            {
                HandelSelectedAction(actualMoucePosition);
            }

            if (Input.GetMouseButton(1))
            {
                GridPosition gridPosition = new GridPosition(0, 0);

                LevelSystem.Instance.GetHexTile(gridPosition).SetUnit(unit.GetComponent<Unit>());

            }
        } 
    }

    private void HandelSelectedAction(Vector3 actualMoucePosition)
    {
        GridPosition mouseGridPosition = LevelSystem.Instance.GetGridPosition(actualMoucePosition);
        selectedHexTile = LevelSystem.Instance.GetHexTile(mouseGridPosition);
        if (selectedUnit != null)
        {
            selectedUnit.GetComponent<MoveAction>().SetTarget(selectedHexTile);
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
    }

    public HexTile GetSelectedHexTile()
    {
        return selectedHexTile;
    }
   
}
