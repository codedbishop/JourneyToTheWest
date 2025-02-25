using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;

public class Drive : UnitActions
{

    public string oppisoteAction;
    public override void CanPreformAction()
    {
        if (this.gameObject.GetComponent<Mounts>().GetMountaidUnit() == null)
        {
            PanelController.Instance.AddAction(this.GetActionName(), PreformAction);
        }
        else
        {
            PanelController.Instance.AddAction(oppisoteAction, Dismount);
        }
    }

    public override void PreformAction()
    {
        Unit interactingUnit = UnitActionSystem.Instance.GetSelectedUnit().GetComponent<Unit>();
        Unit mountaidUnit = this.gameObject.GetComponent<Unit>();

        this.gameObject.GetComponent<Mounts>().AddMountaidUnit(interactingUnit);

        UnitActionSystem.Instance.SetSelectedUnit(mountaidUnit);

        Mounts mount = mountaidUnit as Mounts;
        //interactingUnit.GetComponent<MoveAction>().SetIsActiveToFalse();
        interactingUnit.GetComponent<AIPath>().enabled = false;

        interactingUnit.transform.position = mount.GetMountPosition().position;
        interactingUnit.gameObject.transform.parent = mount.GetMountPosition();
        //mountaidUnit.GetComponents<Mounts>().GetMountPosition()


        UnitActionSystem.Instance.SetActionStateToMove();


        interactingUnit.GetComponent<Human>().MountUnit(this.gameObject.GetComponent<Mounts>());

        UnitsOnMap.Instance.UpdateUnitProfileStats();
        PanelController.Instance.ResetUnitActions();
    }

    public void Dismount()
    {
        //gets this current unit
        Unit mountaidUnit = this.gameObject.GetComponent<Unit>();

        //gets unit riding this unit
        Unit interactingUnit = mountaidUnit.GetComponent<Mounts>().GetMountaidUnit();
        //sets the mount unit rider to null
        this.gameObject.GetComponent<Mounts>().AddMountaidUnit(null);
        //sets the selected unit to the base unit
        UnitActionSystem.Instance.SetSelectedUnit(interactingUnit);
        //sets the riding unit mount to null
        interactingUnit.GetComponent<Human>().MountUnit(null);

        //** need to add code that sets a location for unit to move to then turns on pathfinging 
        interactingUnit.GetComponent<Move>().SetTarget(this.gameObject.transform.position);

        //interactingUnit.GetComponent<AIPath>().enabled = true; //turns pathfinding on 
        UnitsOnMap.Instance.UpdateUnitProfileStats();
        PanelController.Instance.ResetUnitActions();
    }
}
