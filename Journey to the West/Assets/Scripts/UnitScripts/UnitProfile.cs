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
    [SerializeField] Transform mountBorder;



    Unit unitOnThisButton;


    public void SetUpUnit(Unit unit)
    {
        unitOnThisButton = unit;
        unitNameText.text = unit.GetName();
        unitEnergyText.text = unit.GetEnergyAmount().ToString();

        if (unit is Human)
        {
            Human human = (Human)unit;
            unitHungerText.text = human.GetHunger().ToString();
            unitMoral.text = human.GetMoral().ToString();
        }
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
        mountBorder.gameObject.SetActive(false);
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
        if (unitOnThisButton is Human)
        {
            Human human = (Human)unitOnThisButton;
            unitMoral.text = human.GetMoral().ToString();
        }
           
    }

    public void UpdateHunger()
    {
        if (unitOnThisButton is Human)
        {
            Human human = (Human)unitOnThisButton;
            unitHungerText.text = human.GetHunger().ToString();
        }
    }

    public void SetHasEnergyBorder()
    {
        if (unitOnThisButton.GetComponent<Unit>().GetEnergyAmount() > 0)
        {
            HasMovesBorder.gameObject.SetActive(true);
            OutOfMovesBorder.gameObject.SetActive(false);
            selectedUnit.gameObject.SetActive(false);
            //mountBorder.gameObject.SetActive(false);
        }
        else
        {
            HasMovesBorder.gameObject.SetActive(false);
            OutOfMovesBorder.gameObject.SetActive(true);
            selectedUnit.gameObject.SetActive(false);
            //mountBorder.gameObject.SetActive(false);
        }
    }

    public void SetIsMountBorder()
    {
        Debug.Log("Mount Border Set");
        mountBorder.gameObject.SetActive(true);
        HasMovesBorder.gameObject.SetActive(false);
        OutOfMovesBorder.gameObject.SetActive(false);
        selectedUnit.gameObject.SetActive(false);
    }
}
