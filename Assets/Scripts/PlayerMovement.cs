using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    #region Constant variables

    private const float
        XMIN = -6.5f,
        XMAX = 6.5f,
        YMIN = -3.5f,
        YMAX = 4f;

    #endregion

    #region Private variables

    private float
        _xMovementAmount,
        _xMovementAmountRaw,
        _yMovementAmount,
        _yMovementAmountRaw;

    #endregion

    #region Serialized fields

    [Tooltip("In ms^-1")][SerializeField]
    private float xSpeed = 6f;

    [Tooltip("In ms^-1")][SerializeField]
    private float ySpeed = 6f;

    #endregion


    private void Update()
    {
        CalculateMove();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.localPosition = new Vector3(
            _xMovementAmountRaw,
            _yMovementAmountRaw,
            transform.localPosition.z);
    }

    private void CalculateMove()
    {
        _xMovementAmount = CrossPlatformInputManager.GetAxis("Horizontal") * xSpeed * Time.fixedDeltaTime;
        _xMovementAmountRaw = Mathf.Clamp(transform.localPosition.x + _xMovementAmount, XMIN, XMAX);

        _yMovementAmount = CrossPlatformInputManager.GetAxis("Vertical") * ySpeed * Time.fixedDeltaTime;
        _yMovementAmountRaw = Mathf.Clamp(transform.localPosition.y + _yMovementAmount, YMIN, YMAX);

        transform.localPosition = new Vector3(
            _xMovementAmountRaw,
            _yMovementAmountRaw,
            transform.localPosition.z);

    }
}
