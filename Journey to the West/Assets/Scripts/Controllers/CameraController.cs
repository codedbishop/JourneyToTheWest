using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float minZoom;
    [SerializeField] float maxZoom;
    [SerializeField] int zoomSpeed;

    [SerializeField] private CinemachineCamera cinemachineCamera;
    private float targetFieldOfView;

    //private Vector3 targetFallowOffset;

    private void Awake()
    {
        targetFieldOfView = cinemachineCamera.Lens.FieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        HandelMovement();
        HandelRotation();
        HandelZoom();
    }

    private void HandelMovement()
    {
        Vector3 inputMoveDir = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputMoveDir.z = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputMoveDir.z = -1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputMoveDir.x = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputMoveDir.x = +1f;
        }

        Vector3 moveVector = transform.forward * inputMoveDir.z + transform.right * inputMoveDir.x;
        transform.position += moveVector * moveSpeed * Time.deltaTime;
    }

    private void HandelRotation()
    {
        Vector3 rotationVector = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.Q))
        {
            rotationVector.y = +1f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotationVector.y = -1f;
        }

        float rotationSpeed = 100f;
        transform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime;
    }

    private void HandelZoom()
    {
        float zoomAmount = 1f;

        if (Input.mouseScrollDelta.y > 0)
        {
            targetFieldOfView -= zoomAmount;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            targetFieldOfView += zoomAmount;
        }

        targetFieldOfView = Mathf.Clamp(targetFieldOfView, minZoom, maxZoom);

        cinemachineCamera.Lens.FieldOfView = Mathf.Lerp(cinemachineCamera.Lens.FieldOfView, targetFieldOfView, zoomSpeed * Time.deltaTime);
    }
}
