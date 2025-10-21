using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HaydenMovement : MonoBehaviour
{
    public float jumpForce = 12f;
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
            var rb = GetComponent<Rigidbody2D>();
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;

            // play jump sound
            if (jumpSound != null)
            {
                audioSource.pitch = Random.Range(0.95f, 1.05f); // adds slight variation
                audioSource.PlayOneShot(jumpSound);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
            isGrounded = true;
    }
}
