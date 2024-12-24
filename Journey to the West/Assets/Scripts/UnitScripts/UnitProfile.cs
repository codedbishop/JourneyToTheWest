using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UnitProfile : MonoBehaviour
{
    [SerializeField] private Button selectButton;
    [SerializeField] private TMP_Text unitNameText;
    [SerializeField] Transform selectedUnit;

    Unit unitOnThisButton;


    public void SetUpUnit(Unit unit)
    {
        unitOnThisButton = unit;
        unitNameText.text = unit.GetName();
    }

    public void SelectThisUnit()
    {
        UnitActionSystem.Instance.SetSelectedUnit(unitOnThisButton);
    }

    public void SetUnitToActive()
    {
        UnitsOnMap.Instance.DeselectedAllUnitProfiles();
        selectedUnit.gameObject.SetActive(true);
    }

    public void DeselectedProfile()
    {
        selectedUnit.gameObject.SetActive(false);
    }
}
