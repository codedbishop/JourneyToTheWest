using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UnitActionsUi : MonoBehaviour
{
    [SerializeField] Transform buttonTemplate;

    UnitActionButton unitActionButton;

    [SerializeField] ItemSo meat;

    public void SetUpButtons()
    {
        DestroyButtons();

        //Instantiate(buttonTemplate, this.transform).GetComponent<UnitActionButton>().SetUpButton("Move", SetMoveAction);

        //this hardcoded meat item so will need to be changed to be more dinamic. like using a foodSO 
        //if (UnitActionSystem.Instance.GetSelectedUnit().GetComponent<Unit>().CheckForFood())
        //{
        //    Instantiate(buttonTemplate, this.transform).GetComponent<UnitActionButton>().SetUpButton("Eat", FeedUnit);
        //}


        //if (UnitActionSystem.Instance.GetSelectedHexTile().GetTileModifire() != null && UnitActionSystem.Instance.GetSelectedUnit().GetComponent<Unit>().GetEnergyAmount() >= 20)
        //{
        //    unitActionButton = Instantiate(buttonTemplate, this.transform).GetComponent<UnitActionButton>();
        //    unitActionButton.SetUpButton("Hunt", UnitActionSystem.Instance.HuntTile);
        //}
    }

    public void AddAction(UnitActions unitActions)
    {
        Instantiate(buttonTemplate, this.transform).GetComponent<UnitActionButton>().SetUpButton(unitActions.GetActionName(), unitActions.PreformAction);
    }

    public void DestroyButtons()
    {
        foreach (Transform child in this.transform) 
        { 
            GameObject.Destroy(child.gameObject); 
        }
    }

   public void SetMoveAction()
    {
        UnitActionSystem.Instance.SetActionStateToMove();
    }

    public void FeedUnit()
    {
        //UnitActionSystem.Instance.FeedUnit();
    }
}
