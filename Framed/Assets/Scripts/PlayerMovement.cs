using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float sprintSpeed = 6f;
    public float crouchSpeed = 1.5f;
    public float gravity = -9.81f;

    public Transform playerCamera;
    public float mouseSensitivity = 200f;
    public Texture2D cursor;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float originalHeight;
    private float crouchHeight = 1f;

    private float xRotation = 0f;
    private Animator animator;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        originalHeight = controller.height;
    }

    private void OnMouseEnter()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        Debug.Log("Cursor Visible");
        Cursor.visible = true;
    }

    private void OnMouseExit()
    {
        Debug.Log("Cursor not visible");
        Cursor.visible = false;
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
        {
            speed = sprintSpeed;
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
        if (controller.height < originalHeight)
        {
            speed = crouchSpeed;
            animator.SetBool("Crouch", true);
        }
        else
        {
            animator.SetBool("Crouch", false);
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;
        controller.Move(moveDirection * speed * Time.deltaTime);
        if (Mathf.Abs(moveDirection.magnitude) > 0)
            animator.SetBool("IsWalking", true);
        else
            animator.SetBool("IsWalking", false);

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
