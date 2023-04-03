using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The target to follow (usually the player)
    public Vector3 offset; // The offset position of the camera relative to the target
    public float smoothSpeed = 0.125f; // The speed at which the camera follows the target

    private void LateUpdate()
    {
        // Calculate the desired position of the camera based on the target's position and the offset
        Vector3 desiredPosition = target.position + offset;

        // Smoothly interpolate the camera's position from its current position to the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update the camera's position
        transform.position = smoothedPosition;
    }
}
