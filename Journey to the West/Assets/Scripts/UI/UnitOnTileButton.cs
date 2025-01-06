using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.CanvasScaler;

public class UnitOnTileButton : MonoBehaviour
{
    [SerializeField] private Button selectButton;
    [SerializeField] private TMP_Text unitNameText;
    Unit unitOnThisButton;


    public void SetUpButton(Unit unit)
    {
        unitOnThisButton = unit;
        unitNameText.text = unit.GetName();
    }

    public void SelectThisUnit()
    {
        UnitActionSystem.Instance.SetSelectedUnit(unitOnThisButton);
    }

  
}
