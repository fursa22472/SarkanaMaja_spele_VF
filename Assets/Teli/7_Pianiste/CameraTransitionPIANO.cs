using System;
using UnityEngine;

public class CameraTransitionPIANO : MonoBehaviour
{
    [Header("Camera Settings")]
    public Transform originalPositionTransform; // The transform representing the original position and rotation
    public float transitionSpeed = 2.0f; // Speed of the camera transition
    public float smoothTime = 0.3f; // Smooth time for SmoothDamp

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 currentVelocity; // Velocity used by SmoothDamp

    private bool isTransitioning = false;
    private Transform targetPositionTransform; // The transform to move to during dialogue

    private void Start()
    {
        if (originalPositionTransform != null)
        {
            initialPosition = originalPositionTransform.position;
            initialRotation = originalPositionTransform.rotation;
        }
        else
        {
            Debug.LogWarning("Original position transform not set in CameraTransitionPIANO.");
        }
    }

    public void MoveToTarget(Transform newTarget)
    {
        targetPositionTransform = newTarget;
        isTransitioning = true;
    }

    public void ResetCamera()
    {
        targetPositionTransform = null;
        isTransitioning = true;
    }

    private void Update()
    {
        if (isTransitioning)
        {
            if (targetPositionTransform != null)
            {
                // Smoothly move towards the target position
                Vector3 targetPos = targetPositionTransform.position;
                Quaternion targetRot = targetPositionTransform.rotation;

                transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref currentVelocity, smoothTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * transitionSpeed);

                // Check if close enough to stop transitioning to target
                if (Vector3.Distance(transform.position, targetPos) < 0.01f && Quaternion.Angle(transform.rotation, targetRot) < 1.0f)
                {
                    isTransitioning = false;
                }
            }
            else
            {
                // Smoothly move back to the original position and rotation
                Vector3 targetPos = initialPosition;
                Quaternion targetRot = initialRotation;

                transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref currentVelocity, smoothTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * transitionSpeed);

                // Check if close enough to stop transitioning to original
                if (Vector3.Distance(transform.position, targetPos) < 0.01f && Quaternion.Angle(transform.rotation, targetRot) < 1.0f)
                {
                    isTransitioning = false;
                }
            }
        }
    }
}
