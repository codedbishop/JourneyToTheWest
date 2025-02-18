using Unity.VisualScripting;
using UnityEngine;

public class Hunt : UnitActions
{
    public override void CanPreformAction()
    {
        if (CheckForHuntable())
        {
            PanelController.Instance.AddAction(this.GetActionName(), PreformAction);
        }
    }

    public override void PreformAction()
    {
        HuntTile();
    }

    public bool CheckForHuntable()
    {
        if(UnitActionSystem.Instance.GetSelectedHexTile().GetTileModifire() != null)
        {
            return true;
        }
        return false;
    }

    public void HuntTile()
    {
        Unit interactingUnit = UnitActionSystem.Instance.GetSelectedUnit().GetComponent<Unit>();

        interactingUnit.RemoveEnergy(20);

        HexTile selectedHexTile = UnitActionSystem.Instance.GetSelectedHexTile();

        interactingUnit.AddItemToInventory(selectedHexTile.GetHuntableObject().GetComponent<DeerAI>().GetItemDrop());
        PanelController.Instance.GetComponent<PanelController>().ResetInventory();

        Destroy(selectedHexTile.GetHuntableObject());
        selectedHexTile.RemoveHuntableObject();
        PanelController.Instance.ResetUnitActions();

    }
}
