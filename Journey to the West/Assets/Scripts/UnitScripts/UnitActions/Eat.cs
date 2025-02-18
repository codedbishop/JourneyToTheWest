using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using static UnitActionSystem;

public class Eat : UnitActions
{
    public override void CanPreformAction()
    {
        if (CheckForFood())
        {
            PanelController.Instance.AddAction(this.GetActionName(), PreformAction);
        }
    }

    public override void PreformAction()
    {
        FeedUnit();
    }

    public bool CheckForFood()
    {
        foreach (ItemSo inventoryItem in this.gameObject.GetComponent<Unit>().GetInventory())
        {
            if (inventoryItem is FoodSo)
            {
                return true;
            }
        }
        return false;
    }

    public void FeedUnit()
    {
        this.GetComponent<Human>().RemoveFoodItemFromInventory();
        this.GetComponent<Human>().RestoreHunger(10);

        PanelController.Instance.ResetInventory();
        UnitsOnMap.Instance.UpdateUnitProfileStats();
        PanelController.Instance.ResetUnitActions();
    }
}
