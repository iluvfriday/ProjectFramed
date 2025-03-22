using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float sprintSpeed = 6f;
    public float crouchSpeed = 1.5f;
    public float gravity = -9.81f;
    public float crouchHeight = 1f;

    public Transform playerCamera;
    public float mouseSensitivity = 200f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float originalHeight;
    private float xRotation = 0f;
    private Vector3 originalCenter;
    private float originalCameraY;

    void Start()
    {
        DisableCursor();
        controller = GetComponent<CharacterController>();
        originalHeight = controller.height;
        originalCenter = controller.center;
        originalCameraY = playerCamera.localPosition.y; // Lưu vị trí gốc của camera
    }

    private static void DisableCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
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
        }
        if (controller.height < originalHeight)
        {
            speed = crouchSpeed;
        }
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
        bool isCrouching = Input.GetKey(KeyCode.LeftControl);

        // Điều chỉnh collider height
        float targetHeight = isCrouching ? crouchHeight : originalHeight;
        controller.height = Mathf.Lerp(controller.height, targetHeight, Time.deltaTime * 10f);

        // Điều chỉnh vị trí center để đảm bảo collider vẫn chạm đất
        float centerY = originalCenter.y - (originalHeight - controller.height) / 2;
        controller.center = new Vector3(originalCenter.x, centerY, originalCenter.z);

        // Điều chỉnh camera theo công thức originalHeight - crouchHeight
        float targetCameraY = isCrouching
            ? (originalCameraY - (originalHeight - crouchHeight))
            : originalCameraY;
        playerCamera.localPosition = new Vector3(
            playerCamera.localPosition.x,
            Mathf.Lerp(playerCamera.localPosition.y, targetCameraY, Time.deltaTime * 10f),
            playerCamera.localPosition.z
        );
    }
}
