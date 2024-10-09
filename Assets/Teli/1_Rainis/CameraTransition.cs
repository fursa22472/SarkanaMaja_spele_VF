using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    public Transform targetPosition;
    public float transitionSpeed = 2.0f;
    public float smoothTime = 0.3f;

    private Transform originalParent;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool isTransitioning = false;

    private Vector3 currentVelocity;

    void Start()
    {
        originalParent = transform.parent;
        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;
    }

    public void MoveToTarget(Transform newTarget)
    {
        targetPosition = newTarget;
        isTransitioning = true;
    }

    public void ResetCamera()
    {
        targetPosition = null;
        isTransitioning = true;
    }

    void Update()
    {
        if (isTransitioning)
        {
            if (targetPosition != null)
            {
                Vector3 targetPos = targetPosition.position;
                Quaternion targetRot = targetPosition.rotation;

                transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref currentVelocity, smoothTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * transitionSpeed);

                transform.parent = null;

                if (Vector3.Distance(transform.position, targetPos) < 0.01f && Quaternion.Angle(transform.rotation, targetRot) < 1.0f)
                {
                    isTransitioning = false;
                }
            }
            else
            {
                Vector3 targetPos = originalParent.TransformPoint(originalPosition);
                Quaternion targetRot = originalParent.rotation * originalRotation;

                transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref currentVelocity, smoothTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * transitionSpeed);

                if (Vector3.Distance(transform.position, targetPos) < 0.01f && Quaternion.Angle(transform.rotation, targetRot) < 1.0f)
                {
                    transform.parent = originalParent;
                    isTransitioning = false;
                }
            }
        }
    }
}
