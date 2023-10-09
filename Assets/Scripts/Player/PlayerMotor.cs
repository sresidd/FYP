using Unity.Netcode;
using UnityEngine;

public class PlayerMotor : NetworkBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private bool isCrouching;
    private bool lerpCrouch;
    private bool isSprinting;
    public float gravity = -9.8f;
    public float speed = 5f;
    public float jumpHeight = 1f;
    float crouchTimer;

    private void Start()
    {
        controller = GetComponent<CharacterController>(); 


        InputManager.Instance.OnJumpPerformed += Jump;
        InputManager.Instance.OnCrouchPerformed += Crouch;
        InputManager.Instance.OnSprintPerformed += Sprint;
    }

    private void FixedUpdate()
    {
        if(!IsOwner) return;
        ProcessMove();
    }

    private void Update()
    {
        if(!IsOwner) return;
        isGrounded = controller.isGrounded;
        if(lerpCrouch){
            crouchTimer += Time.deltaTime;
            // print(Time.deltaTime);
            // print("crouchTimer" +crouchTimer);
            float p = crouchTimer /1;
            // print("p" + p);
            p *= p;
            // print("p-change" + p);
            if(isCrouching)
                controller.height = Mathf.Lerp(controller.height,1,p);
            else
                controller.height = Mathf.Lerp(controller.height,2,p);
            
            if(p>1){
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
    }

    private void ProcessMove()
    {
        Vector3 moveDirection = Vector3.zero;
        Vector2 input = InputManager.Instance.GetMovementVectorNormalized();
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) *speed*Time.deltaTime);
        playerVelocity.y += gravity*Time.deltaTime;
        if(isGrounded && playerVelocity.y<=0)
            playerVelocity.y = -2f;
        controller.Move(playerVelocity*Time.deltaTime);
    }

    private void Jump()
    {
        if(!IsOwner) return;
        if(isGrounded){
            playerVelocity.y =(jumpHeight*-1 *gravity)/5;

            // Mathf.Sqrt
            print(playerVelocity.y);
            if(isCrouching){
                playerVelocity.y/=2;
            }
        }
    }

    private void Crouch()
    {
        if(!IsOwner) return;
        isCrouching = !isCrouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }

    private void Sprint()
    {
        if(!IsOwner) return;
        isSprinting = !isSprinting;
        if(isSprinting)
            speed = 8f;
        else
            speed = 5f;
    }
}
