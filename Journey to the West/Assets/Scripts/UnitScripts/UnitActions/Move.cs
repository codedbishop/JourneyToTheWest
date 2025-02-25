using Pathfinding;
using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnitActionSystem;

public class Move : UnitActions
{
    public ProceduralGraphMover proceduralGraphMover;
    bool isMoving;
    Vector3 target;
    float stoppingDistance = 0.2f;


    public void Update()
    {
        if(isMoving)
        {
            if(Vector3.Distance(target, this.gameObject.transform.position) < stoppingDistance)
            {
                isMoving = false;
                this.gameObject.GetComponent<AIPath>().enabled = false;
            }
        }
    }


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

    public void SetTarget(Vector3 target)
    {
        isMoving = true;
        this.target = target;

        this.gameObject.GetComponent<AIPath>().enabled = true; //turns pathfinding on 


        //Assuming the unit has an AIPath component for pathfinding and movement
        AIPath aiPath = this.gameObject.GetComponent<AIPath>();


        if (aiPath != null)
        {
            // Set the target position for the AIPath component
            aiPath.destination = target;
            aiPath.SearchPath(); // Request a path update
        }
    }
   
}
