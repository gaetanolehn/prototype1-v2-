// -----
// TankCameraFollow.cs
// Prototype1 (v2)
// Smoothly follows the tank barrel (pitch + yaw) in third-person perspective
// -----

using UnityEngine;

public class TankCameraFollow : MonoBehaviour
{
    #region Variables

    [Header("Target Tracking")]
    [Tooltip("Attach the Barrel object here")]
    [SerializeField] private Transform barrelObject;


    [Header("Camera Settings")]
    [SerializeField] private Vector3 cameraOffset = new Vector3(0f, 4f, -15f);
    [SerializeField] private float movementSmoothSpeed = 12f;
    [SerializeField] private float rotationSmoothSpeed = 12f;

    #endregion

    #region Game Cycle Methods


    void LateUpdate()
    {
        if (barrelObject == null)
            return;

        FollowBarrel();
    }

    #endregion
    

    #region Game Mechanic Methods


    void FollowBarrel()
    {
        // Follow position using local offset relative to the barrel
        Vector3 targetPosition = barrelObject.TransformPoint(cameraOffset);
        transform.position = Vector3.Lerp
        (
            transform.position,
            targetPosition,
            movementSmoothSpeed * Time.deltaTime
        );

        // Match camera rotation to barrel's pitch + yaw
        Quaternion targetRotation = barrelObject.rotation;
        transform.rotation = Quaternion.Slerp
        (
            transform.rotation,
            targetRotation,
            rotationSmoothSpeed * Time.deltaTime
        );
    }
    
    #endregion
}
