using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private PlayerControls playerControls;
    private Vector2 cameraMovement;
    private float upDownRotation;
    private float leftRightRotation;

    [SerializeField] private float leftRightMaxAngle = 30f;
    [SerializeField] private float upDownMaxAngle = 30f;

    private void Awake()
    {
        // Add event handlers for the joystick input
        playerControls = new PlayerControls();

        playerControls.Gameplay.MoveCamera.performed += ctx => cameraMovement = ctx.ReadValue<Vector2>();
        playerControls.Gameplay.MoveCamera.canceled += ctx => cameraMovement = Vector2.zero;
    }

    private void Update()
    {

        leftRightRotation = -cameraMovement.y * leftRightMaxAngle;
        upDownRotation = -cameraMovement.x * upDownMaxAngle;

    }

    private void FixedUpdate()
    {
        transform.localEulerAngles = new Vector3(
            -leftRightRotation * Time.fixedDeltaTime,
            -upDownRotation * Time.fixedDeltaTime,
            0f);
    }

    private void OnEnable()
    {
        playerControls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        playerControls.Gameplay.Disable();
    }
}
