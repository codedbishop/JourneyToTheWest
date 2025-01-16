using UnityEngine;

[CreateAssetMenu(fileName = "NewScriptableObject", menuName = "ScriptableObject/Item", order = 1)]
public class ItemSo : ScriptableObject
{
    [SerializeField] private string name; 
    [SerializeField] private Sprite icon;

    public Sprite GetIcon()
    {
        return icon; 
    }

}
