using UnityEngine;

public class PlayerMoter : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private bool crouching;
    private bool lerpCrouch;
    private bool sprinting;
    public float gravity = -9.8f;
    public float speed = 5f;
    public float jumpHeight = 1f;
    float crouchTimer;

    void Start()
    {
        controller = GetComponent<CharacterController>();   
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
        if(lerpCrouch){
            crouchTimer += Time.deltaTime;
            // print(Time.deltaTime);
            // print("crouchTimer" +crouchTimer);
            float p = crouchTimer /1;
            // print("p" + p);
            p *= p;
            // print("p-change" + p);
            if(crouching)
                controller.height = Mathf.Lerp(controller.height,1,p);
            else
                controller.height = Mathf.Lerp(controller.height,2,p);
            
            if(p>1){
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
    }

    public void ProcessMove(Vector2 input){
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) *speed*Time.deltaTime);
        playerVelocity.y += gravity*Time.deltaTime;
        if(isGrounded && playerVelocity.y<0)
            playerVelocity.y = -2f;
        controller.Move(playerVelocity*Time.deltaTime);
    }

    public void Jump(){
        if(isGrounded){
            playerVelocity.y =(jumpHeight*-1 *gravity)/5;

            // Mathf.Sqrt
            print(playerVelocity.y);
            if(crouching){
                playerVelocity.y/=2;
            }
        }
    }

    public void Crouch(){
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }

    public void Sprint(){
        sprinting = !sprinting;
        if(sprinting)
            speed = 8f;
        else
            speed = 5f;
    }
}
