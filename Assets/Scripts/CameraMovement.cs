using UnityEngine.InputSystem;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [SerializeField] float zSpeed = 10f;

    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();

        playerControls.Gameplay.Accelerate.performed += ctx => Accelerate();

    }

    private void Accelerate()
    {
        transform.position += Vector3.forward * zSpeed;
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
