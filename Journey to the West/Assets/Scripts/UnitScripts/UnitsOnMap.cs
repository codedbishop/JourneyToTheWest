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
}
