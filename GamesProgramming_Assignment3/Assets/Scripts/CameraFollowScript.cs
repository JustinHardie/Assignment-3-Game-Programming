using UnityEngine;

public class SimpleCameraFollow : MonoBehaviour
{
    public Transform target; // the player (penguin)

    // Position offset — slightly higher and further back for better view
    public Vector3 offset = new Vector3(0f, 2.5f, -7f);

    // Smoother, slower follow speed
    public float smoothSpeed = 2.5f;

    // Slight downward tilt
    public float lookHeightOffset = 1.5f;

    void LateUpdate()
    {
        if (target == null) return;

        // Calculate desired position relative to player
        Vector3 desiredPosition = target.position + target.TransformDirection(offset);

        // Smooth camera movement
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Look slightly above player (so you see front area)
        Vector3 lookPoint = target.position + Vector3.up * lookHeightOffset;
        transform.LookAt(lookPoint);
    }
}
