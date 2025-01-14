using System;
using UnityEngine;
using UnityEngine.UI;

public class UnitActionsUi : MonoBehaviour
{
    [SerializeField] Transform buttonTemplate;

    UnitActionButton unitActionButton;


    public void SetUpButtons()
    {
        DestroyButtons();

        Instantiate(buttonTemplate, this.transform).GetComponent<UnitActionButton>().SetUpButton("Move", SetMoveAction);
        Instantiate(buttonTemplate, this.transform).GetComponent<UnitActionButton>().SetUpButton("Eat", FeedUnit);


        if (UnitActionSystem.Instance.GetSelectedHexTile().GetTileModifire() != null)
        {
            unitActionButton = Instantiate(buttonTemplate, this.transform).GetComponent<UnitActionButton>();
            unitActionButton.SetUpButton("Hunt", UnitActionSystem.Instance.HuntTile);
        }
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
        UnitActionSystem.Instance.FeedUnit();
    }
}
