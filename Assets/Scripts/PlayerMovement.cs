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


    private PlayerControls playerControls;
    private Vector2 movement;
    private float
        acceleration,
        calculatedSpeed,
        leftRightRotation,
        upDownRotation;

    #endregion

    #region Serialized fields

    [SerializeField]
    private float leftRightSpeed = 100f;

    [SerializeField]
    private float upDownSpeed = 100f;

    [SerializeField]
    private float forwardSpeed = 100f;

    [SerializeField]
    private float minimumSpeed = 0.4f;

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

        leftRightRotation = -movement.y * upDownSpeed;
        upDownRotation = -movement.x * leftRightSpeed;


        if (acceleration == 0f) 
        { 
            calculatedSpeed = minimumSpeed * forwardSpeed; 
        }
        else 
        {
            calculatedSpeed = acceleration * forwardSpeed;
        }

    }

    private void FixedUpdate()
    {

        transform.position += transform.forward * calculatedSpeed * Time.fixedDeltaTime;

        transform.Rotate(leftRightRotation * Time.fixedDeltaTime, 0f,  upDownRotation * Time.fixedDeltaTime);
    }



}
