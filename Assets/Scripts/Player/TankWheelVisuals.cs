// -----
// WheelVisuals.cs
// Prototype1 (v2)
// Spins left and right tank wheels based on movement and turning speed
// -----

using UnityEngine;

public class WheelVisuals : MonoBehaviour
{
    #region Variables

    [Header("References")]
    [SerializeField] private Transform[] wheels;
    [SerializeField] private TankController tankController;

    [Header("Settings")]
    [Tooltip("Multiplier for visual rotation speed")]
    [SerializeField] private float wheelRotationSpeed = 360f;

    #endregion


    #region Game Cycle Methods

    void Start()
    {
        tankController = GetComponent<TankController>();
    }

    private void FixedUpdate()
    {
        HandleWheelRotation();
    }

    #endregion


    #region  Game Mechanic Methods

    private void HandleWheelRotation()
    {
        if (tankController == null || wheels == null || wheels.Length == 0)
            return;

        float currentSpeed = tankController.bodyCurrentSpeed;
        float rotationAmount = currentSpeed * wheelRotationSpeed * Time.fixedDeltaTime;

        foreach (Transform wheel in wheels)
        {
            wheel.Rotate(Vector3.right, rotationAmount, Space.Self);
        }
    }
    
    #endregion
}
