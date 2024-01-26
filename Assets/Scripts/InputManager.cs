using UnityEngine;
using System;
using Unity.VisualScripting;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions OnFoot;

    public event Action OnJumpPerformed;
    public event Action OnCrouchPerformed;
    public event Action OnSprintPerformed;
    public event Action OnReloadPerformed;
    public event Action OnToggleFireModePerformed;
    public event Action OnToggleADSPerformed;

    public event Action<bool> OnShoot;

    public static InputManager Instance;

    void Awake()
    {
        Instance = this;

        playerInput = new PlayerInput();
        OnFoot = playerInput.OnFoot;

        OnFoot.Jump.performed += ContextMenu => Jump_Performed();
        // OnFoot.ReduceHealth.performed += ContextMenu => playerHealth.TakeDamage(Random.Range(10, 15));
        // OnFoot.IncreaseHealth.performed += ContextMenu => playerHealth.IncreaseHeath(Random.Range(10, 15));
        OnFoot.Crouch.performed += ContextMenu => Crouch_Performed();
        OnFoot.Sprint.performed += ContextMenu => Sprint_Performed();
        OnFoot.Shoot.started += ContextMenu => Firing_Context(true);
        OnFoot.Shoot.canceled += ContextMenu => Firing_Context(false);
        OnFoot.Reload.performed += ContextMenu => Reload_Performed();
        OnFoot.ToggleFireMode.performed += ContextMenu => ToggleFireMode_Performed();
        OnFoot.ToggleADS.performed += ContextMenu => ToggleADSPerformed();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void ToggleADSPerformed()
    {
        OnToggleADSPerformed?.Invoke();
    }

    private void ToggleFireMode_Performed()
    {
        OnToggleFireModePerformed?.Invoke();
    }

    private void Reload_Performed()
    {
        OnReloadPerformed?.Invoke();
    }

    private void Sprint_Performed()
    {
        OnSprintPerformed?.Invoke();
    }

    private void Crouch_Performed()
    {
        OnCrouchPerformed?.Invoke();
    }

    private void Jump_Performed()
    {
        OnJumpPerformed?.Invoke();
    }

    public Vector2 GetMovementVectorNormalized(){
        Vector2 inputVector = OnFoot.Movement.ReadValue<Vector2>();
        return inputVector;
    }

    public Vector2 GetLookVectorNormalized()
    {
        Vector2 inputVector = OnFoot.Look.ReadValue<Vector2>();
        return inputVector;
    }

    // private static int RandomFloat()
    // {
    //     return Random.Range(10, 15);
    // }



    private void Firing_Context(bool isFiring){
        OnShoot?.Invoke(isFiring);
    }

    private void OnEnable(){
        OnFoot.Enable();
    }
    private void OnDisable(){
        OnFoot.Disable();
    }
}
