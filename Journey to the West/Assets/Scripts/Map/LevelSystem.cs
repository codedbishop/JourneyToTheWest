using Unity.Mathematics;
using UnityEngine;



public class LevelSystem : MonoBehaviour
{
    public static LevelSystem Instance { get; private set; }

    private const float HEX_VERTICAL_OFFSET_MULTIPLIER = .75f;
    [SerializeField] int mapHight;
    [SerializeField] int mapWidth;
    [SerializeField] int cellSize;

    [SerializeField] Transform hexTilePrefab;

    HexGridSystem hexGridSystem;

    [SerializeField] Transform treePrefab;


    private void Awake()
    {
        Instance = this;    
    }

    void Start()
    {
        hexGridSystem = new HexGridSystem(mapHight, mapWidth, cellSize);//creates the object that holds the hex data for the map
        GenerateHexTiles();
        SpawnTrees();

    }

    public void GenerateHexTiles()
    {

        for (int x = 0; x < mapWidth; x++)
        {
            for(int z = 0; z < mapHight; z++)
            {
                HexTile hexTile = hexGridSystem.GetHexTile(new GridPosition(x, z));//gets the hexTileObject to attach visuals to
                GameObject hexTileVisual = Instantiate(hexTilePrefab, GetTileLocation(new int2(x, z), cellSize), Quaternion.identity, this.transform).gameObject;//creates visual for the hex
                hexTileVisual.transform.name = ("Tile " + x + ", " + z);
                hexTile.AttachGameObject(hexTileVisual);
                
            }
        }
    }

    //this is just a temp tree spawner tell a better one is made 
    public void SpawnTrees()
    {
       
        int totalNumber = 100;
        for (int x = 0; x < totalNumber; x++)
        {
            float xPosition = UnityEngine.Random.Range(0,((mapWidth-2) * 6));
            float zPosition = UnityEngine.Random.Range(0,(mapHight * 3.5f));
            Instantiate(treePrefab, new Vector3(xPosition, 0, zPosition), Quaternion.identity);
        }

    }
    

    public Vector3 GetTileLocation(int2 tileXY, float cellSize)
    {
        return
            new Vector3(tileXY.x, 0, 0) * cellSize +
            new Vector3(0, 0, tileXY.y) * cellSize * HEX_VERTICAL_OFFSET_MULTIPLIER +
            (((tileXY.y % 2) == 1) ? new Vector3(1, 0, 0) * cellSize * .5f : Vector3.zero);
    }

    public GridPosition GetGridPosition(Vector3 worldPosition) => hexGridSystem.GitGridPosition(worldPosition);

    public Vector3 GetHexPositionFromWorldPosition(Vector3 worldPosition) => hexGridSystem.GetHexPositionFromWorldPosition(worldPosition);

    public Vector3 GetHexWorldPosition(HexTile hexTile) => hexGridSystem.GetHexWorldPosition(hexTile);

    public Vector3 GetHexWorldPositionWithGridPosition(GridPosition gridPosition) => hexGridSystem.GetHexWorldPositionWithGridPosition(gridPosition);

    public HexTile GetHexTile(GridPosition gridPosition) => hexGridSystem.GetHexTile(gridPosition);

    public HexTile GetHexTileFromWorldPosition(Vector3 worldPosition) => hexGridSystem.GetHexTileFromWorldPosition(worldPosition);

    public bool IsValidGridPosition(GridPosition gridPosition) => hexGridSystem.IsValidGridPosition(gridPosition);

    public void SetUnitOnTile(Unit unit, HexTile oldTile, HexTile newTile)
    {
        if(oldTile != null)
        {
            oldTile.RemoveUnit(unit);
        }
        newTile.SetUnit(unit);
    }

    //public void FindNumberOfMoves(GridPosition startingHex, int maxMoves) => hexGridSystem.FindNumberOfMoves(startingHex, maxMoves);
}
