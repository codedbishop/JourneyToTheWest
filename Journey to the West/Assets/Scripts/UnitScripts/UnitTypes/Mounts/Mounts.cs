using UnityEngine;

public class Mounts : Unit
{
    Unit mountaidUnit;

    [SerializeField]Transform mountPosition;

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
}
