using System.Collections.Generic;
using UnityEngine;

public class MoveableLocations : MonoBehaviour
{
    public static MoveableLocations Instance;

    [SerializeField] Transform moveableHexPrefab;
    public List<GameObject> moveableHexVisuals = new List<GameObject>();
    List<MoveAbleHexTileLocation> movableHexs = new List<MoveAbleHexTileLocation>();

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

    public void ClearMoveableHexTileVisuals()
    {
        foreach(GameObject moveableHexVisual in moveableHexVisuals)
        {
            moveableHexVisual.SetActive(false);
        }
        ClearTileMoveCosts();
    }

    public void ClearTileMoveCosts()
    {
        if(movableHexs.Count > 0)
        {
            foreach (MoveAbleHexTileLocation moveableHex in movableHexs)
            {
                LevelSystem.Instance.GetHexTile(moveableHex.GetGridPosition()).SetCostToMoveTo(0);
            }
        }
        movableHexs.Clear();
    }

    public List<MoveAbleHexTileLocation> FindNumberOfMoves(GridPosition startingHex, int unitsAvailableEnergy, int energyNeededToMove)
    {

        ClearMoveableHexTileVisuals();
        movableHexs.Add(new MoveAbleHexTileLocation(startingHex, 0));
        List<GridPosition> checkNaghborTiles = GetHexNahbors(startingHex);

        List<GridPosition> checkNextNaghbors = new List<GridPosition>();

        //foreach (GridPosition naghporTile in checkNaghborTiles)
        //{
        //    if (IsValidGridPosition(naghporTile))
        //    {
        //        movableHexs.Add(new MoveAbleHexTileLocation(naghporTile, 1));
        //        checkNextNaghbors.Add(naghporTile);
        //    }
        //}

        //checkNaghborTiles.Clear();
        //checkNaghborTiles.AddRange(checkNextNaghbors);
        //checkNextNaghbors.Clear();

        //movableHexs.Clear();
        //checkNaghborTiles.Clear();
        int maxTilesCanMove = 0;

        if (unitsAvailableEnergy >= energyNeededToMove)
        {
            maxTilesCanMove = unitsAvailableEnergy / energyNeededToMove;


            foreach (GridPosition naghporTile in checkNaghborTiles)
            {
                if (IsValidGridPosition(naghporTile))
                {
                    movableHexs.Add(new MoveAbleHexTileLocation(naghporTile, energyNeededToMove));
                    checkNextNaghbors.Add(naghporTile);
                }
            }

            checkNaghborTiles.Clear();
            checkNaghborTiles.AddRange(checkNextNaghbors);
            checkNextNaghbors.Clear();

            for (int i = 2; maxTilesCanMove >= i; i++)
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
                                    matchFound = true;
                                    break;
                                }
                                else
                                {
                                }
                            }


                            if (!matchFound)
                            {
                                bool matchFoundOnMovable = false;
                                foreach (GridPosition movable in checkNextNaghbors)
                                {
                                    if (check.Equals(movable))
                                    {
                                        matchFoundOnMovable = true;
                                        break;
                                    }
                                }
                                if (!matchFoundOnMovable)
                                {
                                    movableHexs.Add(new MoveAbleHexTileLocation(check, i * energyNeededToMove));
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

            foreach (MoveAbleHexTileLocation gridPosition in movableHexs)
            {
                LevelSystem.Instance.GetHexTile(gridPosition.GetGridPosition()).SetCostToMoveTo(gridPosition.GetMovesNeeded());
            }

            DrawMoveableHexis(movableHexs);
        }
        return movableHexs;
       
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
    int energyNeeded;
    GridPosition hexPosition;

    public MoveAbleHexTileLocation(GridPosition hexPosition, int energyNeeded)
    {
        this.energyNeeded = energyNeeded;
        this.hexPosition = hexPosition;
    }

    public GridPosition GetGridPosition()
    {
        return hexPosition;
    }

    public int GetMovesNeeded()
    {
        return energyNeeded;
    }
}
