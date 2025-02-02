using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class UnitsOnMap : MonoBehaviour
{
    public static UnitsOnMap Instance;

    [SerializeField] List<Unit> unitsOnMap = new List<Unit>();
    [SerializeField] List<UnitProfile> unitsProfiles = new List<UnitProfile>();
    [SerializeField] Transform unitIconPrefab;
    [SerializeField] Transform UnitsProfileHolder;

    private void Awake()
    {
        Instance = this;   
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateUnitProfiles();
    }

    public void CreateUnitProfiles()
    {
        foreach (Unit unitOnMap in unitsOnMap)
        {
            //Transform unit = 
            //.GetComponent<UnitProfile>().SetUpUnit(unitOnMap);
            UnitProfile profile = Instantiate(unitIconPrefab, UnitsProfileHolder).GetComponent<UnitProfile>();
            profile.SetUpUnit(unitOnMap);
            unitsProfiles.Add(profile);
        }
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateUnitProfileEnergy()
    {
        foreach (UnitProfile profile in unitsProfiles)
        {
            profile.UpdateEnergy();
        }
    }

    public void UpdateUnitProfileMoral()
    {
        foreach (UnitProfile profile in unitsProfiles)
        {
            profile.UpdateMoral();
        }
    }
    

    public void SetActiveUnit(GameObject selectedUnit)
    {
        DeselectedAllUnitProfiles();
        for (int i = 0; i < unitsProfiles.Count; i++)
        {
            if (unitsOnMap[i].gameObject == selectedUnit)
            {
                unitsProfiles[i].SetUnitToActive();
                //UnitActionSystem.Instance.SetSelectedUnit(selectedUnit.GetComponent<Unit>());
                return;
            }
        }
    }

    public void DeselectedAllUnitProfiles()
    {
        foreach(UnitProfile profile in unitsProfiles)
        {
            profile.DeselectedProfile();
        }
    }

    public void ReorderUnitList()
    {
        Debug.Log("Reording");
        List<Unit> orderUnitList = new List<Unit>();
        int highistEnergy = 0;
        int unitHighistEnergyIndex = 0;
        while (unitsOnMap.Count > 0)
        {
            highistEnergy = unitsOnMap[0].GetEnergyAmount();
            unitHighistEnergyIndex = 0;
            for (int i = 1; i < unitsOnMap.Count; i++)
            {
                if (unitsOnMap[i].GetEnergyAmount() > highistEnergy)
                {
                    highistEnergy = unitsOnMap[i].GetEnergyAmount();
                    unitHighistEnergyIndex = i;
                }
            }
            orderUnitList.Add(unitsOnMap[unitHighistEnergyIndex]);
            unitsOnMap.Remove(unitsOnMap[unitHighistEnergyIndex]);
        }
        
        foreach (UnitProfile profile in unitsProfiles)
        {
            Destroy(profile.gameObject);
        }
        unitsProfiles.Clear();
        unitsOnMap.AddRange(orderUnitList);
        CreateUnitProfiles();
    }

    public bool CheckIfUnitsHaveEnergy()
    {
        foreach (Unit unitOnMap in unitsOnMap)
        {
            //this will need to change to something that player can set
            if(unitOnMap.GetEnergyAmount() >= unitOnMap.GetEnergyNeededToMove())
            {
                return true;
            }
        }
        return false;
    }

    public void ResetUnitsEnergy()
    {
        foreach (Unit unitOnMap in unitsOnMap)
        {
            unitOnMap.GetEnergyLevel();
        }
        ReorderUnitList();
    }

    public void UpdateUnitProfileStats()
    {
        foreach (UnitProfile unitProfile in unitsProfiles)
        {
            unitProfile.UpdateEnergy();
            unitProfile.UpdateHunger();
        }
    }
}
