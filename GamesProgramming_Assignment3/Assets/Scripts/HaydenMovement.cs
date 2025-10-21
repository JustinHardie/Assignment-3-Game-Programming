using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HaydenMovement : MonoBehaviour
{
    public float jumpForce = 12f;
    public bool isGrounded = false;
    [SerializeField] SpriteRenderer splashSR;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            var rb = GetComponent<Rigidbody2D>();
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }

        if (!splashSR)
        splashSR = GameObject.FindGameObjectWithTag("Splash")?.GetComponent<SpriteRenderer>();

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
            isGrounded = true;
        
        if (splashSR) splashSR.enabled = isGrounded;
    }
}
