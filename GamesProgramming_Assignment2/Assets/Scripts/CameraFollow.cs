using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;   // Assign Player here
    public Vector3 offset;
    public float smoothSpeed = 0.7f;

    private void LateUpdate()
    {
        if (playerTransform == null) return;  // 🔹 Prevent error if player is destroyed

        Vector3 desiredPosition = playerTransform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }

    // Optional: call this if you respawn or reload a player
    public void SetTarget(Transform newTarget)
    {
        playerTransform = newTarget;
    }
}
