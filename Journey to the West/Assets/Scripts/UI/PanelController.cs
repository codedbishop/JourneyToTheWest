using UnityEngine;

public class PanelController : MonoBehaviour
{
    public static PanelController Instance;
    [SerializeField] GameObject unitActionPanal;
    [SerializeField] GameObject unitOnTilePanel;

    public void Awake()
    {
        Instance = this;
    }

    public GameObject GetUnitOnTilePanel()
    {
        return unitOnTilePanel;
    }

    public void SetUnitActionPanelActive()
    {
        unitActionPanal.SetActive(true);
        unitOnTilePanel.SetActive(false);
    }
    public void SetUntOnnTilePanalActive()
    {
        unitActionPanal.SetActive(false);
        unitOnTilePanel.SetActive(true);
    }
}
