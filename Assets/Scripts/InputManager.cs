using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions OnFoot;
    private PlayerMoter playerMoter;
    private PlayerLook playerLook;
    private PlayerHealth playerHealth;
    private WeaponSway weaponSway;
    private Shoot shoot;

    Coroutine fireCoroutine, reloadCoroutine;

    void Awake()
    {
        playerInput = new PlayerInput();
        OnFoot = playerInput.OnFoot;

        OnFoot.Jump.performed += ContextMenu => playerMoter.Jump();
        OnFoot.ReduceHealth.performed += ContextMenu => playerHealth.TakeDamage(Random.Range(10, 15));
        OnFoot.IncreaseHealth.performed += ContextMenu => playerHealth.IncreaseHeath(Random.Range(10, 15));
        OnFoot.Crouch.performed += ContextMenu => playerMoter.Crouch();
        OnFoot.Sprint.performed += ContextMenu => playerMoter.Sprint();
        OnFoot.Shoot.started += ContextMenu => StartFiring();
        OnFoot.Shoot.canceled += ContextMenu => StopFiring();
        OnFoot.Reload.performed += ContextMenu => StartReload();
        OnFoot.FireMode.performed += ContextMenu => shoot.ToggleFireMode(!shoot.rapidFire);
        OnFoot.ADS.performed += ContextMenu =>shoot.ToggleADS(!shoot.isADS);
        OnFoot.ADS.performed += ContextMenu => playerLook.ChangeSensitivity(shoot.isADS);

        playerMoter = GetComponent<PlayerMoter>();
        playerLook = GetComponent<PlayerLook>();
        playerHealth = GetComponent<PlayerHealth>();
        weaponSway = GetComponent<WeaponSway>();
        shoot = GetComponent<Shoot>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private static int RandomFloat()
    {
        return Random.Range(10, 15);
    }

    private void StartReload(){
        reloadCoroutine = StartCoroutine(shoot.Reload());
    }

    private void StartFiring(){
        fireCoroutine = StartCoroutine(shoot.RapidFire());
    }

    private void StopFiring(){
        if(fireCoroutine!= null){
            StopCoroutine(fireCoroutine);
        }
    }
    void FixedUpdate(){
        playerMoter.ProcessMove(OnFoot.Movement.ReadValue<Vector2>());
    }

    void LateUpdate(){
        playerLook.ProcessLook(OnFoot.Look.ReadValue<Vector2>());
        weaponSway.UpdateSway(OnFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable(){
        OnFoot.Enable();
    }
    private void OnDisable(){
        OnFoot.Disable();
    }
}
