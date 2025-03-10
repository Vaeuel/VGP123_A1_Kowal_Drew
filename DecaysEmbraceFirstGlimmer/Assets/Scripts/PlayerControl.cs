 using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
[RequireComponent(typeof(Ground_check), typeof(Jump), typeof(Ranged))]
[RequireComponent (typeof(Melee), typeof(InventoryManager))]

public class PlayerControl : MonoBehaviour
{
    //Component references
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    Ground_check gc;
    Jump jump;
    Ranged rng;
    Melee mle;

    //Movement variables
    [Range(.5f, 10)]
    public float speed = 9f;
    [Range(.5f, 10)]
    public float jumpForce = 9f;

    public bool isGrounded = false;

    private void Awake()
    { //**These lines of code can create unintended behaviour if a component has been disabled intentionally...**
        jump = GetComponent<Jump>() ?? gameObject.AddComponent<Jump>();
        rng = GetComponent<Ranged>() ?? gameObject.AddComponent<Ranged>();
        mle = GetComponent<Melee>() ?? gameObject.AddComponent<Melee>();
    }

// Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        gc = GetComponent<Ground_check>();

    }

    // Update is called once per frame
    void Update()
    {

        CheckIsGrounded();
        float hInput = Input.GetAxis("Horizontal");
        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0); //Creates an animator array named curPlayingClips and sets it to by the anim for later use.
        rb.linearVelocity = new Vector3(hInput * speed, rb.linearVelocity.y, 0);

        if (curPlayingClips.Length > 0)
        {
            if (!(curPlayingClips[0].clip.name == "Ranged"))
            {
                //rb.linearVelocity = new Vector3(hInput * speed, rb.linearVelocity.y, 0);

                if (Input.GetButtonDown("Fire2"))
                {
                    rng.Fire();
                }
            }


            if (!(curPlayingClips[0].clip.name == "Melee"))
            {
                //rb.linearVelocity = new Vector3(hInput * speed, rb.linearVelocity.y, 0);

                if (Input.GetButtonDown("Fire1")) mle.Swing();
            }
        }

        if (jump != null)
        {
            if (Input.GetButtonDown("Jump")) jump.Jumping();
            if (Input.GetButtonUp("Jump")) jump.JumpRelease();
        }

        // sprite flipping
        if (hInput != 0) { sr.flipX = (hInput < 0); }

        anim.SetFloat("hInput", Mathf.Abs(hInput));
        anim.SetBool("IsGrounded", isGrounded);

    }
    void CheckIsGrounded()
    {
        if (!isGrounded)
        {
            if (rb.linearVelocity.y <= 0 && !isGrounded)
                isGrounded = gc.IsGrounded();
        }
        else
            isGrounded = gc.IsGrounded();
    }
}