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
        Debug.Log("Moving");
        if (moveUnit())
        {
            PanelController.Instance.AddAction(this);
        }
    }

    public override void PreformAction()
    {
        //FindTilesUnitCanMoveTo();
        UnitActionSystem.Instance.SetActionStateToMove();

        Debug.Log("Feeding Unit");
        //FeedUnit();
    }

    public bool moveUnit()
    {

        return true;
    }
   
}
