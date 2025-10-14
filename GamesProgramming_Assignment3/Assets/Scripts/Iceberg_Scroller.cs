using UnityEngine;
public class Iceberg_Scrolller : MonoBehaviour {
    public float speed = 8f;
    public float leftDespawnX = -30f;
    void Update() {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        if (transform.position.x < leftDespawnX) Destroy(gameObject);
    }
}
