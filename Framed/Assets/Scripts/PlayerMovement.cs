using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 3f;
    public float sprintSpeed = 6f;
    public float crouchSpeed = 1.5f;
    public float gravity = -9.81f;

    [Header("Mouse Look")]
    public Transform playerCamera;
    public float mouseSensitivity = 200f;
    public bool lockCursor = true;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float originalHeight;
    private float crouchHeight = 1f;

    private float xRotation = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalHeight = controller.height;

        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();
        HandleCrouch();
    }

    void HandleMovement()
    {
        float speed = walkSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
            speed = sprintSpeed;

        if (controller.height < originalHeight)
            speed = crouchSpeed;

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;
        controller.Move(moveDirection * speed * Time.deltaTime);

        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleCrouch()
    {
        float targetHeight = Input.GetKey(KeyCode.LeftControl) ? crouchHeight : originalHeight;
        controller.height = Mathf.Lerp(controller.height, targetHeight, Time.deltaTime * 10f);

        if (Mathf.Abs(controller.height - originalHeight) < 0.01f)
            controller.height = originalHeight;
    }
}
