using System;
using UnityEngine;
using UnityEngine.InputSystem;

// TODO implement banking while rotatin left and right

public class PlayerMovement : MonoBehaviour
{

    private ParticleSystem projectiles;
    private Rigidbody rb;

    #region Private variables


    private PlayerControls playerControls;
    private bool isControlEnabled;
    private Vector2 movement;
    private float
        acceleration,
        calculatedSpeed,
        leftRightRotation,
        upDownRotation,
        shootProjectile;

    #endregion

    #region Serialized fields

    [SerializeField]
    private float leftRightSpeed = 100f;

    [SerializeField]
    private float upDownSpeed = 100f;

    [SerializeField]
    private float forwardSpeed = 100f;

    [SerializeField]
    private float minimumSpeed = 0.1f;

    #endregion

    private void Awake()
    {

        // Add event handlers for the joystick input
        playerControls = new PlayerControls();

        playerControls.Gameplay.Accelerate.performed += ctx => acceleration = ctx.ReadValue<float>();
        playerControls.Gameplay.Accelerate.canceled+= ctx => acceleration = 0f;

        playerControls.Gameplay.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
        playerControls.Gameplay.Move.canceled += ctx => movement = Vector2.zero;

        //playerControls.Gameplay.Shoot.performed += ctx => Shoot();
        playerControls.Gameplay.Shoot.performed += ctx => shootProjectile = ctx.ReadValue<float>();
        playerControls.Gameplay.Shoot.canceled += ctx => shootProjectile = 0f;

        isControlEnabled = true;

        // Cache references
        projectiles = GetComponent<ParticleSystem>();
        rb = GetComponent<Rigidbody>();

    }


    void OnPlayerCollision()
    {
        Debug.Log("Frozen controls");
        isControlEnabled = false;
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
        // movement.x gives the up/down movement of the stick (rotation on x-axis)
        // movement.y gives the left/right movement of the stick (rotation on u-axis)
        if (isControlEnabled)
        {
            DetermineMovement();
            ShootProjectile();
        }

    }

    private void ShootProjectile()
    {
        if (shootProjectile >= Mathf.Epsilon)
        {
            Debug.Log("FIRE");
            projectiles.Play();
        }

    }

    private void DetermineMovement()
    {
        leftRightRotation = -movement.y * leftRightSpeed;
        upDownRotation = -movement.x * upDownSpeed;


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

/*        float posX = 0f;
                float posY = movement.y;
                float posZ = 1f;

                Vector3 newPos = new Vector3(posX, posY, posZ);

                Debug.Log(posX + " " + posY + " " + posZ);

        Quaternion newRotation = transform.rotation;
        Vector3 newEulerAngles = newRotation.eulerAngles;

       
*/



        transform.position += transform.forward * calculatedSpeed * Time.fixedDeltaTime;

/*        Quaternion target = Quaternion.Euler(leftRightRotation, 0f, upDownRotation);

        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.fixedDeltaTime * 5f);*/



        // transform.rotation = Quaternion.RotateTowards(transform.rotation, target, Time.fixedDeltaTime * 50f);

        transform.Rotate(leftRightRotation * Time.fixedDeltaTime, 0f,  upDownRotation * Time.fixedDeltaTime);


/*
        if (leftRightRotation > 0)
        {
            rb.AddForceAtPosition(Vector3.left * leftRightRotation, transform.position);
            Debug.Log("Banking right");

        }
        else if (leftRightRotation < 0)
        {
            rb.AddForceAtPosition(Vector3.right * leftRightRotation, transform.position);
            Debug.Log("Banking left");

        }
        else rb.AddForceAtPosition(Vector3.forward * 10f, transform.position);*/




    }


}
