using UnityEngine;
using UnityEngine.TextCore.Text;

public class playerMove : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravity = -20f;

    private CharacterController controller;
    private Vector3 verticalVelocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = transform.right * horizontal + transform.forward * vertical;

        //Prevent diagonal from being fast (NO BHOPPING YET)
        direction = Vector3.ClampMagnitude(direction, 1f);

        //Current speed uses sprint speed if leftshift is pressed else use walkspeed
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        controller.Move(direction * currentSpeed * Time.deltaTime);
    }

    private void HandleGravityAndJump()
    {
        //Push down wile grounded so controller stays grounded
        if (controller.isGrounded && verticalVelocity.y < 0f)
        {
            verticalVelocity.y = -2f;
        }

        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            verticalVelocity.y  = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        verticalVelocity.y += gravity *  Time.deltaTime;

        controller.Move(verticalVelocity * Time.deltaTime);
    }
}
