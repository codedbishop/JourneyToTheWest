using UnityEngine;

public class Unit : MonoBehaviour
{

    [SerializeField] string name;
    [SerializeField] HexTile hexTileOn;

    public string GetName()
    {
        return name;
    }

    public void MoveUnit(HexTile targetTile)
    {
        gameObject.transform.position = TileSpawnerOLD.Instance.GetHexWorldPosition(targetTile);
        OnUnitMove();
    }

    public void OnUnitMove()
    {
        HexTile previusHexTile = hexTileOn;
        HexTile hexTile = TileSpawnerOLD.Instance.GetHexTileFromWorldPosition(transform.position);
        hexTileOn = hexTile;
        hexTileOn.SetUnit(this);
        if (previusHexTile != null)
        {
            previusHexTile.SetUnit(null);
            previusHexTile = null;
        }
    }
}
