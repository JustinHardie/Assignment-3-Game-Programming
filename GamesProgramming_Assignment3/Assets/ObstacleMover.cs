using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    [HideInInspector] public float speed = 1f;

    void Update()
    {
        // Move left at a constant speed
        transform.Translate(Vector2.left * speed * Time.deltaTime, Space.World);
    }
}
