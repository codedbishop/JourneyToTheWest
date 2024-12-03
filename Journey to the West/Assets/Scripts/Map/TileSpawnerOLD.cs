using Unity.Mathematics;
using UnityEngine;

public class TileSpawnerOLD : MonoBehaviour
{
    public static TileSpawnerOLD Instance { get; private set; }

    private const float HEX_VERTICAL_OFFSET_MULTIPLIER = .75f;
    [SerializeField] int mapHight;
    [SerializeField] int mapWidth;
    [SerializeField] int cellSize;

    [SerializeField] Transform hexTilePrefab;

    HexGridSystem hexGridSystem;

    private void Awake()
    {
        Instance = this;    
    }

    void Start()
    {
        hexGridSystem = new HexGridSystem(mapHight, mapWidth, cellSize);//creates the object that holds the hex data for the map
        GenerateHexTiles();
    }

    public void GenerateHexTiles()
    {

        for (int x = 0; x < mapWidth; x++)
        {
            for(int z = 0; z < mapHight; z++)
            {
                HexTile hexTile = hexGridSystem.GetHexTile(new GridPosition(x, z));//gets the hexTileObject to attach visuals to
                GameObject hexTileVisual = Instantiate(hexTilePrefab, GetTileLocation(new int2(x, z)), Quaternion.identity, this.transform).gameObject;//creates visual for the hex
                hexTileVisual.transform.name = ("Tile " + x + ", " + z);
                hexTile.AttachGameObject(hexTileVisual);
                
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

    public GridPosition GetGridPosition(Vector3 worldPosition) => hexGridSystem.GitGridPosition(worldPosition);

    public Vector3 GetHexPositionFromWorldPosition(Vector3 worldPosition) => hexGridSystem.GetHexPositionFromWorldPosition(worldPosition);

    public Vector3 GetHexWorldPosition(HexTile hexTile) => hexGridSystem.GetHexWorldPosition(hexTile);

    public HexTile GetHexTile(GridPosition gridPosition) => hexGridSystem.GetHexTile(gridPosition);

    public HexTile GetHexTileFromWorldPosition(Vector3 worldPosition) => hexGridSystem.GetHexTileFromWorldPosition(worldPosition);

    //public void DebugHex()
    //{
    //    Debug.Log()
    //}
}
