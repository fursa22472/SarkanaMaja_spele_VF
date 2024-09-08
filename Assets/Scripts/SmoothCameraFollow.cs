using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public Transform target; // Reference to the cube
    public float smoothSpeed = 0.125f; // Adjust for smoother movement
    public Vector3 offset; // Offset position of the camera

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Optionally keep the camera target rotation fixed or look at the cube
        // transform.LookAt(target);
    }
}
