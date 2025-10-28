using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HaydenMovement : MonoBehaviour
{
    public float jumpForce = 12f;
    public bool isGrounded = false;
    [SerializeField] private SpriteRenderer splashSR;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Get splash sprite renderer if not assigned in Inspector
        if (!splashSR)
            splashSR = GameObject.FindGameObjectWithTag("Splash")?.GetComponent<SpriteRenderer>();
    }
    private bool isGrounded = false;
    public AudioClip jumpSound; 
    private AudioSource audioSource;

    void Start()
    {
        // Assign the AudioSource component
        audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;

            // play jump sound
            if (jumpSound != null)
            {
                audioSource.pitch = Random.Range(0.95f, 1.05f); // adds slight variation
                audioSource.PlayOneShot(jumpSound);
            }
        }

        // Update visibility continuously
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
