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
        SetUpButtons(UnitActionSystem.Instance.GetSelectedHexTile().GetUnits());
    }
}
