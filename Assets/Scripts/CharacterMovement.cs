using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 5f;  // Movement speed
    public float cameraRotationSpeed = 100f; // Camera rotation speed
    public float cameraDistance = 5f; // Distance of the camera from the character
    public float cameraHeight = 2f; // Height of the camera above the character
    public Transform cameraTransform; // Reference to the camera's transform

    private Rigidbody rb;
    private Animator animator;
    private float pitch = 0f; // Vertical rotation (pitch)
    private float yaw = 0f;   // Horizontal rotation (yaw)

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical).normalized;
        movement = Quaternion.Euler(0, yaw, 0) * movement; // Rotate movement direction to match camera's yaw

        // Prevent vertical movement
        movement.y = 0;

        Vector3 newPosition = rb.position + movement * speed * Time.deltaTime;
        rb.MovePosition(newPosition);

        // Set the Speed parameter in the Animator to control animations
        animator.SetFloat("Speed", movement.magnitude);

        // Rotate the character to face the movement direction
        if (movement != Vector3.zero)
        {
            transform.forward = movement;
        }
    }

    void LateUpdate()
    {
        // Update the camera's position relative to the character
        Vector3 cameraOffset = new Vector3(0f, cameraHeight, -cameraDistance);
        cameraTransform.position = transform.position + Quaternion.Euler(0, yaw, 0) * cameraOffset;
    }
}

