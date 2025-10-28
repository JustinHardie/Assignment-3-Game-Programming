using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class HaydenMovement : MonoBehaviour
{
    [Header("Movement")]
    public float jumpForce = 12f;
    private bool isGrounded = false;

    [Header("Audio")]
    public AudioClip jumpSound;
    private AudioSource audioSource;

    [Header("Visuals")]
    [SerializeField] private SpriteRenderer splashSR;

    private Rigidbody2D rb;

    void Start()
    {
        // Cache Rigidbody and AudioSource
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        // Get splash sprite renderer if not assigned
        if (!splashSR)
            splashSR = GameObject.FindGameObjectWithTag("Splash")?.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Jump input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;

            // Play jump sound if available
            if (jumpSound != null)
            {
                audioSource.pitch = Random.Range(0.95f, 1.05f);
                audioSource.PlayOneShot(jumpSound);
            }
        }

        // Update splash visibility based on grounded state
        if (splashSR)
            splashSR.enabled = isGrounded;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }
}
