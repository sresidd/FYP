using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;



    private PlayerMoter playerMoter;
    private PlayerLook playerLook;
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        onFoot.Jump.performed += ContextMenu => playerMoter.Jump();
        onFoot.Crouch.performed += ContextMenu => playerMoter.Crouch();
        onFoot.Sprint.performed += ContextMenu => playerMoter.Sprint();


        playerMoter = GetComponent<PlayerMoter>();
        playerLook = GetComponent<PlayerLook>();
    }

    void FixedUpdate(){
        playerMoter.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    void LateUpdate(){
        playerLook.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable(){
        onFoot.Enable();
    }
    private void OnDisable(){
        onFoot.Disable();
    }
}
