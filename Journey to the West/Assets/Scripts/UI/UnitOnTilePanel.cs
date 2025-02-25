using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class UnitOnTilePanel : MonoBehaviour
{
    [SerializeField] GameObject buttonPrefab;

    List<GameObject> allButtons;

    public void Start()
    {
        allButtons = new List<GameObject>();

        UnitActionSystem.Instance.OnHexTileSelected += UnitActionSystem_OnHexTileSelected;

    }

    public void SetUpButtons(List<Unit> units)
    {
        RemoveButtons();

        if (units.Count == 1)
        {
            Mounts unitMount = units[0].GetComponent<Human>().GetMount();
            if (unitMount != null)
            {
                UnitActionSystem.Instance.SetSelectedUnit(unitMount.GetComponent<Unit>());
            }
            else
            {
                UnitActionSystem.Instance.SetSelectedUnit(units[0]);

            }
            return;
        }
        if (units.Count > 0)
        {
            foreach (Unit unit in units)
            {
                GameObject newButton = Instantiate(buttonPrefab, this.transform);
                newButton.GetComponent<UnitOnTileButton>().SetUpButton(unit);
                allButtons.Add(newButton);
            }
        }
    }

    public void RemoveButtons()
    {
        foreach (GameObject button in allButtons)
        {
            Destroy(button);
        }
    }

    private void UnitActionSystem_OnHexTileSelected(object sender, System.EventArgs e)
    {
        List<Unit> humanUnit = new List<Unit>();
        foreach(Unit unit in UnitActionSystem.Instance.GetSelectedHexTile().GetUnits())
        {
            if(unit is Human)
            {
                humanUnit.Add(unit);
            }
        }
        SetUpButtons(humanUnit);
    }
}
