// -----
// TankInputHandler.cs
// Prototype1 (v2): 
// Handles player input using the new Input System
// -----

using UnityEngine;
using UnityEngine.InputSystem;

public class TankInputHandler : MonoBehaviour
{
    // Singleton instance for global communication
    public static TankInputHandler Instance { get; private set; }

    #region Variables

    [Header("Input Actions")]
    [SerializeField] private InputActionReference movementInput;
    [SerializeField] private InputActionReference lookingInput;
    [SerializeField] private InputActionReference primaryFireInput;
    [SerializeField] private InputActionReference reloadInput;

    [Header("MISC")]
    private Vector2 playerMoving;
    private Vector2 playerLooking;

    #endregion

    #region Game Cycle Methods

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Update()
    {
        if (movementInput?.action != null)
            playerMoving = movementInput.action.ReadValue<Vector2>();
            
        if (lookingInput?.action != null)
            playerLooking = lookingInput.action.ReadValue<Vector2>();
    }
    
    private void OnEnable()
    {
        movementInput.action.Enable();
        lookingInput.action.Enable();
        primaryFireInput.action.Enable();
        reloadInput.action.Enable();
    }

    private void OnDisable()
    {
        movementInput.action.Disable();
        lookingInput.action.Disable();
        primaryFireInput.action.Disable();
        reloadInput.action.Disable();
    }

    #endregion

    #region Game Mechanic Methods

    //
    // Bools & Values
    //

    public Vector2 MovingValue()
    {
        return playerMoving;
    }

    public Vector2 LookingValue()
    {
        return playerLooking;
    }

    public Vector2 MouseDeltaInput()
    {
        return playerLooking;
    }

    public bool PrimaryFireTriggered()
    {
        return primaryFireInput.action.WasPressedThisFrame();
    }

    public bool ReloadTriggered()
    {
        return reloadInput.action.WasPressedThisFrame();
    }

    #endregion
}
