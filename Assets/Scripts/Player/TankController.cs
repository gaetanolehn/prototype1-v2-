// -----
// PlayerController.cs
// Prototype1 (v2): 
// Controls the movement of the tank object altogether
// -----

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TankController : MonoBehaviour
{
    #region Variables

    [Header("Movement Settings")]
    [SerializeField] private float bodyMovementSpeed = 8f;
    [SerializeField] private float bodyTurningSpeed = 60f;
    [SerializeField] private float bodyAccelerationRate = 5f;
    public float bodyCurrentSpeed = 0f;
    private float bodyTargetSpeed = 0f;

    [Header("References")]
    [Tooltip("Do not add object - automatically set")]
    private Rigidbody tankRigidbody;

    #endregion


    #region Game Cycle Methods

    private void Awake()
    {
        tankRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        HandleBodyMovement();
    }

    #endregion

    #region Game Mechanic Methods

    private void HandleBodyMovement()
    {
        Vector2 input = TankInputHandler.Instance.MovingValue();

        // Target speed based on input
        bodyTargetSpeed = input.y * bodyMovementSpeed;

        // Smooth acceleration
        bodyCurrentSpeed = Mathf.Lerp
        (
            bodyCurrentSpeed,
            bodyTargetSpeed,
            bodyAccelerationRate * Time.fixedDeltaTime
        );

        // Forward/backward
        Vector3 move = transform.forward * bodyCurrentSpeed * Time.fixedDeltaTime;
        tankRigidbody.MovePosition(tankRigidbody.position + move);

        // Rotate body
        float rotation = input.x * bodyTurningSpeed * Time.fixedDeltaTime;
        Quaternion turn = Quaternion.Euler(0f, rotation, 0f);
        tankRigidbody.MoveRotation(tankRigidbody.rotation * turn);
    }

    #endregion

    public float CurrentSpeed => bodyCurrentSpeed;

}
