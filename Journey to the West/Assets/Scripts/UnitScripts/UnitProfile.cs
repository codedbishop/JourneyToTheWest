using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UnitProfile : MonoBehaviour
{
    [SerializeField] private Button selectButton;
    [SerializeField] private TMP_Text unitNameText;
    [SerializeField] private TMP_Text unitEnergyText;
    [SerializeField] private TMP_Text unitHungerText;
    [SerializeField] private TMP_Text unitMoral;
    [SerializeField] Transform selectedUnit;
    [SerializeField] Transform OutOfMovesBorder;
    [SerializeField] Transform HasMovesBorder;


    Unit unitOnThisButton;


    public void SetUpUnit(Unit unit)
    {
        unitOnThisButton = unit;
        unitNameText.text = unit.GetName();
        unitEnergyText.text = unit.GetEnergyAmount().ToString();
        unitHungerText.text = unit.GetHunger().ToString();
        unitMoral.text = unit.GetMoral().ToString();
        SetHasEnergyBorder();
    }

    public void SelectThisUnit()
    {
        UnitActionSystem.Instance.SetSelectedUnit(unitOnThisButton);
    }

    public void SetUnitToActive()
    {
        UnitsOnMap.Instance.DeselectedAllUnitProfiles();
        selectedUnit.gameObject.SetActive(true);
        HasMovesBorder.gameObject.SetActive(false);
        OutOfMovesBorder.gameObject.SetActive(false);
    }

    public void DeselectedProfile()
    {
        //selectedUnit.gameObject.SetActive(false);
        SetHasEnergyBorder();
    }

    public void UpdateEnergy()
    {
        unitEnergyText.text = unitOnThisButton.GetEnergyAmount().ToString();

    }

    public void UpdateMoral()
    {
        unitMoral.text = unitOnThisButton.GetMoral().ToString();
    }

    public void UpdateHunger()
    {
        unitHungerText.text = unitOnThisButton.GetHunger().ToString();
    }

    public void SetHasEnergyBorder()
    {
        if (unitOnThisButton.GetComponent<Unit>().GetEnergyAmount() > 0)
        {
            HasMovesBorder.gameObject.SetActive(true);
            OutOfMovesBorder.gameObject.SetActive(false);
            selectedUnit.gameObject.SetActive(false);
        }
        else
        {
            HasMovesBorder.gameObject.SetActive(false);
            OutOfMovesBorder.gameObject.SetActive(true);
            selectedUnit.gameObject.SetActive(false);
        }
    }
}
