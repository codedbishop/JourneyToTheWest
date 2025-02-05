using UnityEngine;

public class BuildCamp : UnitActions
{
    [SerializeField] Transform campPrefab;

    public override void CanPreformAction()
    {
       
        PanelController.Instance.AddAction(this);
        
    }

    public override void PreformAction()
    {
        Debug.Log("Building Camp");
        Transform camp = Instantiate(campPrefab, LevelSystem.Instance.GetHexWorldPositionWithGridPosition(LevelSystem.Instance.GetGridPosition(this.transform.position)), Quaternion.identity, this.transform.parent);
        this.gameObject.SetActive(false);

        UnitsOnMap.Instance.SwapUnitsOnMap(this.gameObject.GetComponent<Unit>(), camp.GetComponent<Unit>());
    }
}
