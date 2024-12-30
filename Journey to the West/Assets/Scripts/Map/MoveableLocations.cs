using System.Collections.Generic;
using UnityEngine;

public class MoveableLocations : MonoBehaviour
{
    public static MoveableLocations Instance;

    [SerializeField] Transform moveableHexPrefab;
    public List<GameObject> moveableHexVisuals = new List<GameObject>();

    public void Awake()
    {
        Instance = this;
    }

    public void DrawMoveableHexis(List<MoveAbleHexTileLocation> moveAbleHexTileLocations)
    {
        int hexTileVisualsLangth = moveAbleHexTileLocations.Count;
        for (int i = 0; hexTileVisualsLangth > i; i++)
        {
            if (moveableHexVisuals.Count >= i + 1)
            {
                moveableHexVisuals[i].transform.position = LevelSystem.Instance.GetHexWorldPositionWithGridPosition(moveAbleHexTileLocations[i].GetGridPosition());
                moveableHexVisuals[i].gameObject.SetActive(true);
            }
            else
            {
                Transform hexVisual = Instantiate(moveableHexPrefab, this.transform);
                hexVisual.transform.position = LevelSystem.Instance.GetHexWorldPositionWithGridPosition(moveAbleHexTileLocations[i].GetGridPosition());
                moveableHexVisuals.Add(hexVisual.gameObject);
            }
        }
    }

    public void ClearMoveableHexTiles()
    {
        foreach(GameObject moveableHexVisual in moveableHexVisuals)
        {
            moveableHexVisual.SetActive(false);
        }
    }

    public void FindNumberOfMoves(GridPosition startingHex, int maxMoves)
    {
        List<MoveAbleHexTileLocation> movableHexs = new List<MoveAbleHexTileLocation>();

        List<GridPosition> checkNaghborTiles = GetHexNahbors(startingHex);

        List<GridPosition> checkNextNaghbors = new List<GridPosition>();

        foreach (GridPosition naghporTile in checkNaghborTiles)
        {
            if (IsValidGridPosition(naghporTile))
            {
                movableHexs.Add(new MoveAbleHexTileLocation(naghporTile, 1));
                checkNextNaghbors.Add(naghporTile);
            }
        }

        checkNaghborTiles.Clear();
        checkNaghborTiles.AddRange(checkNextNaghbors);
        checkNextNaghbors.Clear();

        Debug.Log("Next numver to count " + checkNaghborTiles.Count);
        //movableHexs.Clear();
        //checkNaghborTiles.Clear();

        for (int i = 2; maxMoves >= i; i++)
        {
            foreach (GridPosition naghporTile in checkNaghborTiles)
            {
                List<GridPosition> checkTheseNaghbors = GetHexNahbors(naghporTile);

                foreach (GridPosition check in checkTheseNaghbors)
                {
                    if (IsValidGridPosition(check))
                    {
                        bool matchFound = false;
                        foreach (MoveAbleHexTileLocation movable2 in movableHexs)
                        {
                            if (check.Equals(movable2.GetGridPosition()))
                            {
                                Debug.Log("tileFoundIn Hex List");
                                matchFound = true;
                                break;
                            }
                            else
                            {
                                Debug.Log("tile not FoundIn naghbor list " + movable2.GetGridPosition().GetPosition() + " : " + check.GetPosition());
                            }
                        }


                        if (!matchFound)
                        {
                            bool matchFoundOnMovable = false;
                            foreach (GridPosition movable in checkNextNaghbors)
                            {
                                if (check.Equals(movable))
                                {
                                    Debug.Log("tileFoundIn naghbor list " + movable.GetPosition() + " : " + check.GetPosition());
                                    matchFoundOnMovable = true;
                                    break;
                                }
                            }
                            if (!matchFoundOnMovable)
                            {
                                movableHexs.Add(new MoveAbleHexTileLocation(check, 1));
                                checkNextNaghbors.Add(check);
                            }
                        }
                    }
                }
            }
            checkNaghborTiles.Clear();
            checkNaghborTiles.AddRange(checkNextNaghbors);
            checkNextNaghbors.Clear();
        }


        DrawMoveableHexis(movableHexs);

        //Debug.Log(movableHexs.Count);

        //foreach (MoveAbleHexTileLocation movable in movableHexs)
        //{
        //    Debug.Log(movable.GetGridPosition().GetPosition());
        //}

    }

    public List<GridPosition> GetHexNahbors(GridPosition startingHex)
    {
        bool oddRow = startingHex.z % 2 == 1;

        List<GridPosition> neightbourGridPositionList = new List<GridPosition>
        {
            startingHex + new GridPosition(-1, 0),
            startingHex + new GridPosition(+1, 0),

            startingHex + new GridPosition(0, +1),
            startingHex + new GridPosition(0, -1),

            startingHex + new GridPosition(oddRow ? +1 : -1, +1),
            startingHex + new GridPosition(oddRow ? +1 : -1, -1),
        };
        return neightbourGridPositionList;
    }

    public bool IsValidGridPosition(GridPosition gridPosition)
    {
        return LevelSystem.Instance.IsValidGridPosition(gridPosition);
        
        //return gridPosition.x >= 0 && gridPosition.z >= 0 && gridPosition.x < mapWidth && gridPosition.z < mapHight;
    }
}

public class MoveAbleHexTileLocation
{
    int movesNeeded;
    GridPosition hexPosition;

    public MoveAbleHexTileLocation(GridPosition hexPosition, int movesNeeded)
    {
        this.movesNeeded = movesNeeded;
        this.hexPosition = hexPosition;
    }

    public GridPosition GetGridPosition()
    {
        return hexPosition;
    }
}
