using UnityEngine;

public class SimpleCameraFollow : MonoBehaviour
{
    public Transform target;       // the player (penguin)
    public Vector3 offset = new Vector3(0f, 3f, -6f); // height and distance
    public float smoothSpeed = 5f; // how smoothly the camera follows

    void LateUpdate()
    {
        if (target == null) return;

        // Desired position behind and above the player
        Vector3 desiredPosition = target.position + target.TransformDirection(offset);

        // Smoothly interpolate camera position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Make camera look at the player
        transform.LookAt(target);
    }
}
