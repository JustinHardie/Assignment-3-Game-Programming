using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviourScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    
    public float jumpForce =  7f;

    private bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Human Controller Bad Example Started");

    }
    
    // Update is called once per frame
    void Update()
    {
        Debug.Log("Frame " + Time.frameCount);

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        float rightKey = (Input.GetKey(KeyCode.RightArrow)) || (Input.GetKey(KeyCode.D)) ? 1f : 0f;
        float leftKey = (Input.GetKey(KeyCode.LeftArrow)) || (Input.GetKey(KeyCode.A)) ? -1f : 0f;
        float axisInput = Input.GetAxis("Horizontal");

        float move = (axisInput + rightKey + leftKey) / 1.5f;

        rb.linearVelocity = new Vector2 (move * moveSpeed, rb.linearVelocity.y);

        if (move > 0) // moving right
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (move < 0) // moving left
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
    }
    
   void OnCollisionEnter2D(Collision2D col)
{
    if (col.gameObject.CompareTag("Ground"))
    {
        isGrounded = true;
    }
}
}
