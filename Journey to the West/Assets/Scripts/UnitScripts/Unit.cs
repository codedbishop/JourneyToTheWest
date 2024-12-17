using UnityEngine;

public class Unit : MonoBehaviour
{

    [SerializeField] string name;
    [SerializeField] HexTile hexTileOn;

    [SerializeField] TileMovePoint tileMovePoint;

    private void Update()
    {
        HexTile newHexTile = LevelSystem.Instance.GetHexTileFromWorldPosition(transform.position);
        if (newHexTile != hexTileOn)
        {
            HexTile oldHexTile = hexTileOn;
            hexTileOn = newHexTile;

            LevelSystem.Instance.SetUnitOnTile(this, oldHexTile, hexTileOn);
        }
    }

    public string GetName()
    {
        return name;
    }

    public void SetTileMovePoint(TileMovePoint tileMovePoint)
    {
        this.tileMovePoint = tileMovePoint;
    }

    public TileMovePoint GetTileMovePoint()
    {
        return tileMovePoint;
    }

    public HexTile GetHexTile()
    {
        return hexTileOn;
    }

}
