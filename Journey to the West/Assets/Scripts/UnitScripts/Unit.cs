using UnityEngine;

public class Unit : MonoBehaviour
{

    [SerializeField] string name;
    [SerializeField] HexTile hexTileOn;

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

}
