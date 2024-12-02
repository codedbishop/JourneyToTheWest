using System;
using Unity.VisualScripting;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    [SerializeField] GameObject mousPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousPosition.transform.position = TileSpawnerOLD.Instance.GetHexPositionFromWorldPosition(MouseWorld.GetPosition());

        if (Input.GetMouseButtonDown(0))
        {
            HandelSelectedAction();
        }
    }

    private void HandelSelectedAction()
    {
        GridPosition mouseGridPosition = TileSpawnerOLD.Instance.GetGridPosition(MouseWorld.GetPosition());
        HexTile hexTile = TileSpawnerOLD.Instance.GetHexTile(mouseGridPosition);


        Debug.Log(hexTile.GetHexTileObject().name);
    }

}
