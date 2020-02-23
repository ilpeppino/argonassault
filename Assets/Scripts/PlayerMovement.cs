using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    #region Constant variables

    private const float
        XMIN = -6.5f,
        XMAX = 6.5f,
        YMIN = -4f,
        YMAX = 4f,
        MAXROLLROTATION = 45f,
        MAXTILTROTATION = 20f,
        COMPENSATIONTILTROTATION = 5f,
        COMPENSATIONYAWROTATION = 5f;

    #endregion

    #region Private variables

    private float
        _xMovementAmount,
        _xMovementAmountRaw,
        _yMovementAmount,
        _yMovementAmountRaw,
        _rollRotation,
        _yawRotation,
        _tiltRotation,
        _compensationTiltRotation;

    private Camera camera;

    private PlayerControls playerControls;
    private Vector2 movement;
    private float acceleration;

    #endregion

    #region Serialized fields

    [SerializeField]
    private float xSpeed = 50f;

    [SerializeField]
    private float ySpeed = 50f;

    [SerializeField]
    private float zSpeed = 30f;

    #endregion

    private void Awake()
    {

        // Add event handlers for the joystick input
        playerControls = new PlayerControls();

        playerControls.Gameplay.Accelerate.performed += ctx => acceleration = ctx.ReadValue<float>();
        playerControls.Gameplay.Accelerate.canceled+= ctx => acceleration = 0f;

        playerControls.Gameplay.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
        playerControls.Gameplay.Move.canceled += ctx => movement = Vector2.zero;


    }

    private void OnEnable()
    {
        playerControls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        playerControls.Gameplay.Disable();
    }

    private void Update()
    {
        transform.position += transform.forward * acceleration * zSpeed * Time.fixedDeltaTime;
        transform.Rotate(-movement.y, 0f, -movement.x);

    }




}
