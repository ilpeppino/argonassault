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
    private Vector2 acceleration;

    #endregion

    #region Serialized fields

    [SerializeField]
    private float xSpeed = 50f;

    [SerializeField]
    private float ySpeed = 50f;

    #endregion

    private void Awake()
    {

        // Add event handlers for the joystick input
        playerControls = new PlayerControls();

     
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
        CalculateMovementAndRotation();
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        transform.localPosition = new Vector3(
            _xMovementAmountRaw,
            _yMovementAmountRaw,
            transform.localPosition.z);
    }

    private void Rotate()
    {

        transform.localRotation = Quaternion.Euler(_tiltRotation, _yawRotation, _rollRotation);

    }

    private void CalculateMovementAndRotation()
    {
        // Calculates the ship movement on x and y axis
        _xMovementAmount = movement.x;
        _xMovementAmountRaw = Mathf.Clamp(
            transform.localPosition.x + (_xMovementAmount * xSpeed * Time.fixedDeltaTime), 
            XMIN, 
            XMAX);

        _yMovementAmount = movement.y;
        _yMovementAmountRaw = Mathf.Clamp(
            transform.localPosition.y + (_yMovementAmount * ySpeed * Time.fixedDeltaTime), 
            YMIN, 
            YMAX);

        Debug.Log(_xMovementAmountRaw + " " + _yMovementAmountRaw);

        // Rotation on X Axis
        // Calculates the tilt angle (pitch up/down) depending on the Y localposition, so that the ship's nose always points straight forward
        _compensationTiltRotation = -(_yMovementAmountRaw * COMPENSATIONTILTROTATION);
        _tiltRotation = -(_yMovementAmount * MAXTILTROTATION) + _compensationTiltRotation;

        // Rotation on Y Axis
        // Calculates the yaw angle (pitch up/down) depending on the X localposition, so that the ship's nose always points straight forward
        _yawRotation = transform.localPosition.x * COMPENSATIONYAWROTATION;

        // Rotation on Z Axis
        // Calculates the rolling angle (left/right) 
        _rollRotation = -(_xMovementAmount * MAXROLLROTATION);
        
    }



}
