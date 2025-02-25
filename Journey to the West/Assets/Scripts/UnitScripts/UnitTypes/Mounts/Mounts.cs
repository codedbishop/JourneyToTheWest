using UnityEngine;

public class Mounts : Unit
{
    Unit mountaidUnit;

    [SerializeField]Transform mountPosition;


    public override void Update()
    {
        //base.Update();
        HexTile newHexTile = LevelSystem.Instance.GetHexTileFromWorldPosition(transform.position);
        if (newHexTile != hexTileOn)
        {
            HexTile oldHexTile = hexTileOn;
            hexTileOn = newHexTile;

            LevelSystem.Instance.SetUnitOnTile(this, oldHexTile, hexTileOn);
            hexTileOn.AddActionToTile(gameObject.GetComponent<UnitActions>());
            if(oldHexTile != null)
            {
                oldHexTile.RemoveActionFromTile(gameObject.GetComponent<UnitActions>());
            }
        }
    } 

    public void AddMountaidUnit(Unit unit)
    {
        mountaidUnit = unit;
    }

    public Unit GetMountaidUnit()
    {
        return mountaidUnit;
    }

    public Transform GetMountPosition()
    {
        return mountPosition;
    }

    public override void RemoveEnergy(int energyToRemove)
    {
        energyAmount -= energyToRemove;
        UnitsOnMap.Instance.UpdateUnitProfileEnergy();

        mountaidUnit.RemoveEnergy(energyToRemove / 2);//removes energy from mounted unit as well
    }
}
