using UnityEngine;

public class DeerAI : MonoBehaviour, ITileModifire
{
    [SerializeField] ItemSo itemDrop;

    [SerializeField] UnitActions unitAction;

    public ItemSo GetItemDrop()
    {
        return itemDrop;
    }

    public UnitActions GetUnitAction()
    {
        return unitAction;
    }

}
