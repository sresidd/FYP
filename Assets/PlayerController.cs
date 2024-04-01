using UnityEngine;
using UnityEngine.InputSystem.iOS;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private Animator animator;
    [SerializeField] private float walkSpeed= 5f;
    [SerializeField] private float runSpeed= 5f;
    [SerializeField] private float animationTransitionTime = 5f;
    [SerializeField] private float accelerationSpeed = 1f;
    [SerializeField] private float sprintTransitionTime = 1f;

    private float speed;
    private float moveSpeed;
    private float animatorMultiplier = .5f;

    private Vector3 playerVelocity;
    private bool isSprinting;

    private void Start() {
        moveSpeed = walkSpeed;
        InputManager.Instance.OnSprintPerformed += Sprint;
    }
    private void FixedUpdate()
    {
        ProcessMove();
    }

private void ProcessMove()
{
    Vector3 moveDirection = Vector3.zero;
    Vector2 input = InputManager.Instance.GetMovementVectorNormalized();
    
    // Lerp the speed based on input
    speed = Mathf.Lerp(0, moveSpeed, input.magnitude); // Assuming minSpeed and maxSpeed are defined elsewhere

    moveDirection.x = input.x;
    moveDirection.z = input.y;

    // Lerp the player's velocity for smooth movement
    playerVelocity = Vector3.Lerp(playerVelocity, moveDirection * speed, Time.deltaTime * accelerationSpeed);

    controller.Move(playerVelocity * Time.deltaTime);

    // Lerp animator parameters for smooth animation transition
    float animatorXTarget = input.x * animatorMultiplier;
    float animatorYTarget = input.y * animatorMultiplier;
    animator.SetFloat("X", Mathf.Lerp(animator.GetFloat("X"), animatorXTarget, Time.deltaTime * animationTransitionTime));
    animator.SetFloat("Y", Mathf.Lerp(animator.GetFloat("Y"), animatorYTarget, Time.deltaTime * animationTransitionTime));
}


   private void Sprint()
{
    isSprinting = !isSprinting;
    Debug.Log(isSprinting);
    if (isSprinting)
    {
        // Lerp moveSpeed and animator multiplier for sprinting
        moveSpeed = Mathf.Lerp(walkSpeed, runSpeed, sprintTransitionTime);
        animatorMultiplier = Mathf.Lerp(animatorMultiplier, 1f, sprintTransitionTime);
    }
    else
    {
        // Lerp moveSpeed and animator multiplier for walking
        moveSpeed = Mathf.Lerp(runSpeed, walkSpeed, sprintTransitionTime);
        animatorMultiplier = Mathf.Lerp(animatorMultiplier, .5f, sprintTransitionTime);
    }
}
}
