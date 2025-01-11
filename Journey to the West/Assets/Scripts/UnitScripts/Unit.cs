using UnityEngine;

public class Unit : MonoBehaviour
{

    [SerializeField] string name;
    [SerializeField] int energyMax;

    [SerializeField] int energyAmount;

    [SerializeField] int hunger;
    [SerializeField] int maxHunger;

    [SerializeField] HexTile hexTileOn;

    [SerializeField] TileMovePoint tileMovePoint;

    [SerializeField] int energyNeededToMove;



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

    public int GetEnergyAmount()
    {
        return energyAmount;
    }

    public void RemoveEnergy(int energyToRemove)
    {
        energyAmount -= energyToRemove;
        UnitsOnMap.Instance.UpdateUnitProfileEnergy();
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

    public void ResetEnergy()
    {
        int neededEnergy = energyMax - energyAmount;
        if(hunger >= neededEnergy / 2)
        {
            hunger -= neededEnergy / 2;
            energyAmount = energyMax;
        }
        else
        {
            energyAmount += hunger * 2;
            hunger = 0;
        }
    }

    public int GetEnergyNeededToMove()
    {
        return energyNeededToMove;
    }

    public int GetHunger()
    {
        return hunger;
    }
}
