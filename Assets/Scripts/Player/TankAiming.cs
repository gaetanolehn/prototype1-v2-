// -----
// TankAiming.cs
// Prototype1 (v2): 
// Rotates the turret towards the cursor using raycast
// -----

using UnityEngine;

public class TankAiming : MonoBehaviour
{
    // Note: it is recommended to use the following:
    //public Vector2 MouseDelta()
    //{
    //    return playerLooking;
    //}
    //

    #region Variables

    [Header("References")]
    [SerializeField] private Transform barrelObject;
    [SerializeField] private Transform turretObject;

    [Tooltip("Set Aim layer for raycast hit")]
    [SerializeField] private LayerMask aimLayer;

    [Header("Turret & Barrel Settings")]
    [Tooltip("Aim Settings")]
    [SerializeField] private float turretRotationSpeed = 2f;
    [SerializeField] private float barrelPitchSpeed = 2f;
    [SerializeField] private float minPitch = -10f;
    [SerializeField] private float maxPitch = 25f;
    private float currentPitch = 0f;

    #endregion


    #region Game Cycle Methods

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        AimTurret();
    }

    #endregion
    

    #region Game Mechanic Methods

    private void AimTurret()
    {
        Vector2 mouseDelta = TankInputHandler.Instance.MouseDeltaInput();

        // Horizontal movement = turret yaw
        float yaw = mouseDelta.x * turretRotationSpeed * Time.deltaTime;
        turretObject.Rotate(0f, yaw, 0f, Space.Self);

        // Vertical movement = barrel pitch
        currentPitch -= mouseDelta.y * barrelPitchSpeed * Time.deltaTime;
        currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch);

        barrelObject.localRotation = Quaternion.Euler(currentPitch, 0f, 0f);
    }
    
    #endregion

}
