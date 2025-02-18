using Pathfinding;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnitActionSystem;

public class Move : UnitActions
{
    public ProceduralGraphMover proceduralGraphMover;


    public override void CanPreformAction()
    {
        if (moveUnit())
        {
            PanelController.Instance.AddAction(this.GetActionName(), PreformAction);
        }
    }

    public override void PreformAction()
    {
        //FindTilesUnitCanMoveTo();
        UnitActionSystem.Instance.SetActionStateToMove();

        //FeedUnit();
    }

    public bool moveUnit()
    {

        return true;
    }
   
}
