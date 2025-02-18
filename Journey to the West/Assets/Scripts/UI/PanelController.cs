using System;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public static PanelController Instance;
    [SerializeField] GameObject unitActionPanal;
    [SerializeField] GameObject unitOnTilePanel;

    [SerializeField] GameObject unitInventoryPanal;


    public void Awake()
    {
        Instance = this;
    }

    public GameObject GetUnitOnTilePanel()
    {
        return unitOnTilePanel;
    }

    public void SetUnitActionPanelActive()
    {
        unitActionPanal.SetActive(true);
        unitOnTilePanel.SetActive(false);
        unitActionPanal.GetComponent<UnitActionsUi>().SetUpButtons();
        unitInventoryPanal.SetActive(true);
        unitInventoryPanal.GetComponent<InventoryUi>().SetInventorySlots(UnitActionSystem.Instance.GetSelectedUnit().GetComponent<Unit>().GetInventory());
    }

    public void ResetInventory()
    {
        unitInventoryPanal.GetComponent<InventoryUi>().ResetInventorySlots();
        unitInventoryPanal.GetComponent<InventoryUi>().SetInventorySlots(UnitActionSystem.Instance.GetSelectedUnit().GetComponent<Unit>().GetInventory());
    }

    public void ResetUnitActions()
    {
        unitActionPanal.GetComponent<UnitActionsUi>().SetUpButtons();
    }


    public void SetUntOnTilePanalActive()
    {
        unitActionPanal.SetActive(false);
        unitOnTilePanel.SetActive(true);
        unitInventoryPanal.SetActive(false);

    }

    public void AddAction(string actionName, Action action) => unitActionPanal.GetComponent<UnitActionsUi>().AddAction(actionName, action);

    public void DestroyButtons() => unitActionPanal.GetComponent <UnitActionsUi>().DestroyButtons();
}
