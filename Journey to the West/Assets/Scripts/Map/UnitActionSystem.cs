using System;
using Unity.VisualScripting;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    [SerializeField] GameObject mousPosition;

    [SerializeField] GameObject unit;

    [SerializeField] GameObject selectedUnit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        mousPosition.transform.position = LevelSystem.Instance.GetHexPositionFromWorldPosition(MouseWorld.GetPosition());

        if (Input.GetMouseButtonDown(0))
        {
            HandelSelectedAction();
        }

        if (Input.GetMouseButton(1))
        {
            GridPosition gridPosition = new GridPosition(0, 0);

            LevelSystem.Instance.GetHexTile(gridPosition).SetUnit(unit.GetComponent<Unit>());

        }
    }

    private void HandelSelectedAction()
    {
        GridPosition mouseGridPosition = LevelSystem.Instance.GetGridPosition(MouseWorld.GetPosition());
        HexTile hexTile = LevelSystem.Instance.GetHexTile(mouseGridPosition);
        if (selectedUnit != null)
        {
            selectedUnit.GetComponent<MoveAction>().SetTarget(hexTile);
            //hexTile.SetUnit(selectedUnit.GetComponent<Unit>());
            //Debug.Log(hexTile.GetUnit().GetName());
            selectedUnit = null;
        }
        else
        {

            if (hexTile.GetUnit() != null)
            {
                selectedUnit = hexTile.GetUnit().gameObject;
                Debug.Log(hexTile.GetUnit().GetName());
            }
        }

        Debug.Log(hexTile.GetHexTileObject().name);
    }

   
}
