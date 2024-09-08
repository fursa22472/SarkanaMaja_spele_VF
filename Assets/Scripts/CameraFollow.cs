using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The target object to follow
    public Vector3 offset; // Offset position from the target

    void Start()
    {
        // Initialize the offset based on the current position of the camera and the target
        if (target != null)
        {
            offset = transform.position - target.position;
        }
    }

    void LateUpdate()
    {
        // Update the camera position to follow the target
        if (target != null)
        {
            transform.position = target.position + offset;
        }
    }
}
