using UnityEngine;

public class PanelController : MonoBehaviour
{
    public static PanelController Instance;
    [SerializeField] GameObject unitOnTilePanel;

    public void Awake()
    {
        Instance = this;
    }

    public GameObject GetUnitOnTilePanel()
    {
        return unitOnTilePanel;
    }
}
