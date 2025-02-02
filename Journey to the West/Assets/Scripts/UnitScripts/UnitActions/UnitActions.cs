using UnityEngine;

public class UnitActions : MonoBehaviour
{
    [SerializeField] string actionName;

    public virtual void CanPreformAction()
    {

    }

    public virtual void PreformAction()
    {

    }

    public string GetActionName()
    {
        return  actionName;
    }
}
