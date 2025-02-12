using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
[RequireComponent(typeof(Ground_check), typeof(Jump))]

public class PlayerControl : MonoBehaviour
{
    //Component references
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    Ground_check gc;
    Jump jump;

    //Movement variables
    [Range(.5f, 10)]
    public float speed = 6.5f;
    [Range(.5f, 10)]
    public float jumpForce = 6.5f;

    public bool isGrounded = false;


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
        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);

        CheckIsGrounded();
        float hInput = Input.GetAxis("Horizontal");

        rb.linearVelocity = new Vector3(hInput * speed, rb.linearVelocity.y, 0);

        /*
        if (curPlayingClips.Length > 0)
        {
            if (!(curPlayingClips[0].clip.name == "Attack"))
            {
                
            }

        }*/

        /*if (Input.GetButtonDown("Fire1")) // Firing
        {
            anim.SetTrigger("IsFiring");
        }*/

        // sprite flipping
        if (hInput != 0) { sr.flipX = (hInput < 0); }

       // anim.SetFloat("hInput", Mathf.Abs(hInput));
       // anim.SetBool("IsGrounded", isGrounded);

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
