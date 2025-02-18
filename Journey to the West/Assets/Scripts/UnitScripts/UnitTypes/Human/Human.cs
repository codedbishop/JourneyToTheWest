using UnityEngine;

public class Human : Unit
{
    [SerializeField] int hunger;
    [SerializeField] int maxHunger;

    [SerializeField] public int moral;

    [SerializeField] Mounts mount;

    public int GetHunger()
    {
        return hunger;
    }

    public void RestoreHunger(int hungerToRestore)
    {
        hunger += hungerToRestore;
        energyAmount -= 10;
    }

    public int GetMoral()
    {
        return moral;
    }

    public void UpdateMoral(int moralChange)
    {
        moral += moralChange;
        UnitsOnMap.Instance.UpdateUnitProfileMoral();
    }

    public void MountUnit(Mounts mount)
    {
        this.mount = mount;
    }

    public Mounts GetMount()
    {
        return mount;
    }
}
