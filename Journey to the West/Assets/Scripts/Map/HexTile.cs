using UnityEngine;

public class HexTile
{
    private HexGridSystem gridSystem;
    private GridPosition gridPosition;

    private GameObject hexTileObject;

    public HexTile(HexGridSystem gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
    }

    //attaches the visualGameObject to this object
    public void AttachGameObject(GameObject hexObject)
    {
        this.hexTileObject = hexObject;
    }

    public GameObject GetHexTileObject()
    {
        return this.hexTileObject;
    }
}
