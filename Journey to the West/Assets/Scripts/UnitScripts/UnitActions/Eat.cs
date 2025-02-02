using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using static UnitActionSystem;

public class Eat : UnitActions
{
    public override void CanPreformAction()
    {
        Debug.Log("Adding Eat Action");
        if (CheckForFood())
        {
            PanelController.Instance.AddAction(this);
        }
    }

    public override void PreformAction()
    {
        Debug.Log("Feeding Unit");
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
        this.GetComponent<Unit>().RemoveFoodItemFromInventory();
        this.GetComponent<Unit>().RestoreHunger(10);

        PanelController.Instance.ResetInventory();
        UnitsOnMap.Instance.UpdateUnitProfileStats();
        PanelController.Instance.ResetUnitActions();
    }
}
