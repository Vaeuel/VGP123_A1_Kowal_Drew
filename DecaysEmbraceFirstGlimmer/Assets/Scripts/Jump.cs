using UnityEngine;

public class Jump : MonoBehaviour
{

    Rigidbody2D rb;
    PlayerControl pc;
    Animator anim;
    
    [SerializeField, Range(1, 10)] private float jumpHeight = 7f;
    [SerializeField, Range(2, 15)] private float jumpFallForce = 2f;
    [SerializeField] private int maxJumps = 2;

    private int jumpCount = 0;
    float calculatedJumpForce;

    bool isFalling = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PlayerControl>();
        anim = GetComponent<Animator>();
        calculatedJumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * rb.gravityScale));
    }
    public void Jumping()
    {
        if (jumpCount < maxJumps) // Allow jump if under max jumps
        {
            ++jumpCount; // Increase jump count
            isFalling = false;
            anim.SetTrigger("Jump");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // Reset vertical velocity for the double jump
            rb.AddForce(new Vector2(0, calculatedJumpForce), ForceMode2D.Impulse); //Adds force to RB 
        }
    }
    public void JumpRelease()
    {
        anim.ResetTrigger("Jump");
        anim.ResetTrigger("Landed");

        if (rb.linearVelocity.y < -1f) return;

        if (jumpCount <= maxJumps) isFalling = true; //Prevents the falling script from running when not able
    }

    // Update is called once per frame
    void Update()
    {


        if (rb.linearVelocity.y < 0f) //walks off an edge the following happens
        {
            JumpRelease();
        }

        if (isFalling)
        {
            rb.AddForce(Vector2.down * jumpFallForce);
            anim.SetBool("IsFalling", isFalling);
        }

        if (pc.isGrounded)
        {
            jumpCount = 0;

            if (isFalling)
            { 
                isFalling = false;
                anim.SetBool("IsFalling", isFalling);
                anim.SetTrigger("Landed");
            }

        }
    }
}
