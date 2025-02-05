using Unity.VisualScripting;
using UnityEngine;

public class Hunt : UnitActions
{
    public override void CanPreformAction()
    {
        if (CheckForHuntable())
        {
            PanelController.Instance.AddAction(this);
        }
    }

    public override void PreformAction()
    {
        Debug.Log("Hunt");
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
        Debug.Log("Hunt");
        this.GetComponent<Unit>().RemoveEnergy(20);

        HexTile selectedHexTile = UnitActionSystem.Instance.GetSelectedHexTile();

        this.GetComponent<Unit>().AddItemToInventory(selectedHexTile.GetHuntableObject().GetComponent<DeerAI>().GetItemDrop());
        PanelController.Instance.GetComponent<PanelController>().ResetInventory();

        Destroy(selectedHexTile.GetHuntableObject());
        selectedHexTile.RemoveHuntableObject();
        PanelController.Instance.ResetUnitActions();

    }
}
