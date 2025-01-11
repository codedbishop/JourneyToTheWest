using UnityEngine;

public class UnitActionsUi : MonoBehaviour
{
   public void SetMoveAction()
    {
        UnitActionSystem.Instance.SetActionStateToMove();
    }

    public void FeedUnit()
    {
        UnitActionSystem.Instance.FeedUnit();
    }
}
