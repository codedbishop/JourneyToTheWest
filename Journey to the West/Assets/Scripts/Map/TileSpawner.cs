using Unity.Mathematics;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    private const float HEX_VERTICAL_OFFSET_MULTIPLIER = .75f;
    [SerializeField] int mapHight;
    [SerializeField] int mapWidth;
    [SerializeField] int cellSize;

    [SerializeField] Transform hexTilePrefab;

    [SerializeField]

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateHexTiles();
    }

    public void GenerateHexTiles()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for(int z = 0; z < mapHight; z++)
            {
                Instantiate(hexTilePrefab, GetTileLocation(new int2(x, z)), Quaternion.identity, this.transform);
            }
        }
    }

    public Vector3 GetTileLocation(int2 tileXY)
    {
        return
            new Vector3(tileXY.x, 0, 0) * cellSize +
            new Vector3(0, 0, tileXY.y) * cellSize * HEX_VERTICAL_OFFSET_MULTIPLIER +
            (((tileXY.y % 2) == 1) ? new Vector3(1, 0, 0) * cellSize * .5f : Vector3.zero);
    }
}
