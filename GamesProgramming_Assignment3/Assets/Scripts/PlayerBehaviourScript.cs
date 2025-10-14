using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviourScript : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    [Header("Slow Ground")]
    public float slowMultiplier = 0.5f;   // e.g., 0.5 = half speed on slow ground

    [Header("Ice (Slippery)")]
    [Tooltip("How quickly horizontal speed blends toward input on ice (lower = more slide).")]
    public float iceLerp = 0.05f;         // 0.03–0.08 feels nice and slidey

    private bool isGrounded = false;
    private bool onSlowGround = false;
    private bool onIce = false;

    // cache for performance + correctness
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null) Debug.LogError("Rigidbody2D missing on Player!");
    }

    void Update()
    {
        // --- INPUT ---
        float rightKey = (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) ? 1f : 0f;
        float leftKey  = (Input.GetKey(KeyCode.LeftArrow)  || Input.GetKey(KeyCode.A)) ? -1f : 0f;
        float axisInput = Input.GetAxis("Horizontal");
        float moveInput = (axisInput + rightKey + leftKey) / 1.5f; // your original blend

        // --- CURRENT SPEED (slow ground support) ---
        float currentSpeed = onSlowGround ? moveSpeed * slowMultiplier : moveSpeed;

        // --- HORIZONTAL MOVE ---
        if (onIce)
        {
            // Smoothly blend towards target velocity on ice (creates slide)
            float targetX = moveInput * currentSpeed;
            float newX = Mathf.Lerp(rb.linearVelocity.x, targetX, iceLerp);
            rb.linearVelocity = new Vector2(newX, rb.linearVelocity.y);
        }
        else
        {
            // Normal direct control
            rb.linearVelocity = new Vector2(moveInput * currentSpeed, rb.linearVelocity.y);
        }

        // --- FLIP SPRITE ---
        if (moveInput > 0f)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (moveInput < 0f)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        // --- JUMP ---
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
    }

    // ----- COLLISIONS -----
    void OnCollisionEnter2D(Collision2D col)
    {
        // Grounding (generic ground)
        if (col.gameObject.CompareTag("Ground"))
            isGrounded = true;

        // Slow ground
        if (col.gameObject.CompareTag("SlowGround"))
        {
            isGrounded = true;
            onSlowGround = true;
        }

        // Ice
        if (col.gameObject.CompareTag("Ice"))
        {
            isGrounded = true;
            onIce = true;
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        // Stay grounded while standing on any ground-like surface
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("SlowGround") || col.gameObject.CompareTag("Ice"))
            isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        // Leaving slow ground
        if (col.gameObject.CompareTag("SlowGround"))
            onSlowGround = false;

        // Leaving ice
        if (col.gameObject.CompareTag("Ice"))
            onIce = false;

        // If you only had one contact, you might be airborne now.
        // (Grounded is set true again by OnCollisionStay2D when touching any ground.)
    }
}
