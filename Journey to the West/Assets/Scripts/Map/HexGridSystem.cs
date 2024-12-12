using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HexGridSystem
{
    private const float HEX_VERTICAL_OFFSET_MULTIPLIER = 0.75f;


    int mapHight;
    int mapWidth;
    float cellSize;

    private HexTile[,] hexTileArray;

    public HexGridSystem(int mapHight, int mapWidth, float cellSize)
    {
        this.mapHight = mapHight;
        this.mapWidth = mapWidth;
        this.cellSize = cellSize;

        hexTileArray = new HexTile[mapWidth, mapHight];

        for (int x = 0; x < mapWidth; x++)
        {
            for (int z = 0; z < mapHight; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);//creates and sets the xz points for this object 
                hexTileArray[x, z] = new HexTile(this, gridPosition);//sets the points to the hexTile
            }
        }
    }

    public HexTile GetHexTile(GridPosition gridPosition)
    {
        return hexTileArray[gridPosition.x, gridPosition.z];
    }

    public Vector3 GetHexPositionFromWorldPosition(Vector3 worldPosition)
    {
        return GetWorldPosition(GitGridPosition(worldPosition));
    }

    public HexTile GetHexTileFromWorldPosition(Vector3 worldPosition)
    {
        return GetHexTile(GitGridPosition(worldPosition));
    }

    public Vector3 GetHexWorldPosition(HexTile hexTile)
    {
        return GetWorldPosition(hexTile.GetGridPosition());
    }

    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return
            new Vector3(gridPosition.x, 0, 0) * cellSize +
            new Vector3(0, 0, gridPosition.z) * cellSize * HEX_VERTICAL_OFFSET_MULTIPLIER +
            (((gridPosition.z % 2) == 1) ? new Vector3(1, 0, 0) * cellSize * .5f : Vector3.zero);
    }

    public GridPosition GitGridPosition(Vector3 worldPosition)
    {
        GridPosition roughXZ = new GridPosition(
            Mathf.RoundToInt(worldPosition.x / cellSize),
            Mathf.RoundToInt(worldPosition.z / cellSize / HEX_VERTICAL_OFFSET_MULTIPLIER)
        );

        bool oddRow = roughXZ.z % 2 == 1;

        List<GridPosition> neightbourGridPositionList = new List<GridPosition>
        {
            roughXZ + new GridPosition(-1, 0),
            roughXZ + new GridPosition(+1, 0),

            roughXZ + new GridPosition(0, +1),
            roughXZ + new GridPosition(0, -1),

            roughXZ + new GridPosition(oddRow ? +1 : -1, +1),
            roughXZ + new GridPosition(oddRow ? +1 : -1, -1),
        };

        GridPosition closestGridPosition = roughXZ;
        foreach (GridPosition neightbourGridPosition in neightbourGridPositionList)
        {
            if (Vector3.Distance(worldPosition, GetWorldPosition(neightbourGridPosition)) < Vector3.Distance(worldPosition, GetWorldPosition(closestGridPosition)))
            {
                //closer then the closest 
                closestGridPosition = neightbourGridPosition;
            }
        }

        return closestGridPosition;
    }



}
