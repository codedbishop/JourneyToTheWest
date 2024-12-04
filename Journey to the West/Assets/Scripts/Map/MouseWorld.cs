using UnityEngine;
using UnityEngine.EventSystems;

public class MouseWorld : MonoBehaviour
{
    private static MouseWorld Instance;
    [SerializeField] LayerMask mousePlaceLayerMask;


    void Start()
    {
        Instance = this;
    }

    public static Vector3? GetPosition()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return null;
        }
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, Instance.mousePlaceLayerMask))
        {
            return raycastHit.point;

        }
        return null;
    }
}


