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
        Mounts unitMount = unitOnThisButton.GetComponent<Human>().GetMount();
        Debug.Log("Unit Mount is " + unitMount);
        if (unitMount != null)
        {
            UnitActionSystem.Instance.SetSelectedUnit(unitMount.GetComponent<Unit>());
        }
        else
        {
            UnitActionSystem.Instance.SetSelectedUnit(unitOnThisButton);
        }
    }

  
}
