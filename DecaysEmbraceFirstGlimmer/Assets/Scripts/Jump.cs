using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{

    Rigidbody2D rb;
    PlayerControl pc;

    [SerializeField, Range(1, 10)] private float jumpHeight = 7f;
    [SerializeField, Range(2, 15)] private float jumpFallForce = 2f;
    float timeHeld;
    float maxHoldTime = 0.5f;
    float jumpInputTime;
    float calculatedJumpForce;

    bool jumpCancelled = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PlayerControl>();

        calculatedJumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * rb.gravityScale));
    }

    // Update is called once per frame
    void Update()
    {
        if (pc.isGrounded)
        {
            jumpCancelled = false; //Prevents game from applying downward force after landing
            timeHeld = 0; //Reset storage
            jumpInputTime = 0; //reset timer
        
        }

        if (Input.GetButtonDown("Jump")) jumpInputTime = Time.time; //Gets and stores when Jump was pressed
        if (Input.GetButton("Jump")) timeHeld += Time.deltaTime; //Gets and stores how long Jump was pressed
        if (Input.GetButtonUp("Jump")) //When the player releases Jump the following happens
        {
            if (rb.linearVelocity.y < -10) return;
            jumpCancelled = true;
        }

        if (jumpInputTime > 0 && timeHeld <= maxHoldTime)
        {
            if (pc.isGrounded)
            {
                jumpCancelled = false;
                rb.linearVelocity = Vector2.zero;
                rb.AddForce(new Vector2(0, calculatedJumpForce), ForceMode2D.Impulse);
            }
        }

        if (jumpCancelled)
        {
            rb.AddForce(Vector2.down * jumpFallForce);
        }

    }
}
