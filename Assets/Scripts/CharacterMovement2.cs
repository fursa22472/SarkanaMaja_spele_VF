using UnityEngine;

public class CharacterMovement2 : MonoBehaviour
{
    public float speed = 5f;  // Movement speed
    public float rotationSpeed = 10f; // Rotation speed for smoother turning
    public float cameraRotationSpeed = 100f; // Camera rotation speed
    public float cameraDistance = 5f; // Distance of the camera from the character
    public float cameraHeight = 2f; // Height of the camera above the character
    public Transform cameraTransform; // Reference to the camera's transform

    private CharacterController characterController;
    private Animator animator;
    private float pitch = 0f; // Vertical rotation (pitch)
    private float yaw = 0f;   // Horizontal rotation (yaw)
    private Vector3 movement = Vector3.zero; // Movement vector
    private float gravity = -9.81f; // Gravity force
    private float verticalVelocity = 0f; // Vertical velocity for gravity

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        Cursor.lockState = CursorLockMode.None; // Unlock the cursor

        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }

        // Initialize the camera's rotation to match the character's rotation
        yaw = transform.eulerAngles.y;
        pitch = cameraTransform.eulerAngles.x;
    }

    void Update()
    {
        // Camera rotation with arrow keys
        float rotateHorizontal = Input.GetAxis("HorizontalArrow") * cameraRotationSpeed * Time.deltaTime;
        float rotateVertical = Input.GetAxis("VerticalArrow") * cameraRotationSpeed * Time.deltaTime;

        yaw += rotateHorizontal;
        pitch -= rotateVertical;
        pitch = Mathf.Clamp(pitch, -30f, 60f); // Clamp pitch to avoid extreme angles

        // Update the camera's rotation
        cameraTransform.rotation = Quaternion.Euler(pitch, yaw, 0f);

        // Character movement with WASD keys
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 inputMovement = new Vector3(moveHorizontal, 0f, moveVertical).normalized;
        inputMovement = Quaternion.Euler(0, yaw, 0) * inputMovement; // Rotate movement direction to match camera's yaw

        if (characterController.isGrounded)
        {
            verticalVelocity = 0f; // Reset vertical velocity when grounded
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime; // Apply gravity
        }

        movement = inputMovement * speed;
        movement.y = verticalVelocity; // Include vertical movement (gravity)

        // Move the character
        characterController.Move(movement * Time.deltaTime);

        // Set the Speed parameter in the Animator to control animations
        animator.SetFloat("Speed", inputMovement.magnitude);

        // Smoothly rotate the character to face the movement direction
        if (inputMovement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(inputMovement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void LateUpdate()
    {
        // Update the camera's position relative to the character
        Vector3 cameraOffset = new Vector3(0f, cameraHeight, -cameraDistance);
        cameraTransform.position = transform.position + Quaternion.Euler(0, yaw, 0) * cameraOffset;
    }
}
