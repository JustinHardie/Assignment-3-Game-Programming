using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HaydenMovement : MonoBehaviour
{
    public float jumpForce = 12f;
    private bool isGrounded = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            var rb = GetComponent<Rigidbody2D>();
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
            isGrounded = true;
    }
}
