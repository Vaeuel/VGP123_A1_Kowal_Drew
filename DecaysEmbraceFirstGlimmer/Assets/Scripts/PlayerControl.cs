using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
[RequireComponent(typeof(Ground_check), typeof(Jump), typeof(Ranged))]
[RequireComponent (typeof(Melee), typeof(InventoryManager), typeof(Health))]

public class PlayerControl : MonoBehaviour
{
    #region PC References/ Variables
    //Component references
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    Ground_check gc;
    Jump jump;
    Ranged rng;
    Melee mle;
    Health health;
    
    //Movement variables
    [Range(.5f, 10)]
    public float speed = 9f;
    [Range(.5f, 10)]
    public float jumpForce = 9f;

    public bool isGrounded = false;
    private bool canMove = true;
    #endregion


    private void Awake()
    { //**These lines of code can create unintended behaviour if a component has been disabled intentionally...**
        jump = GetComponent<Jump>() ?? gameObject.AddComponent<Jump>();
        rng = GetComponent<Ranged>() ?? gameObject.AddComponent<Ranged>();
        mle = GetComponent<Melee>() ?? gameObject.AddComponent<Melee>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        gc = GetComponent<Ground_check>();
        health = GetComponent<Health>();
        health.OnDeath += Death;//Subscribes on start if placed 
    }

    void Update()
    {
        CheckIsGrounded();
        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0); //Creates an animator array named curPlayingClips and sets it to by the anim for later use.

        float hInput = Input.GetAxis("Horizontal");

        if (canMove)
        {
        rb.linearVelocity = new Vector3(hInput * speed, rb.linearVelocity.y, 0);

            #region ATTACKING? CHECK
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
            #endregion

            if (jump != null)
            {
                if (Input.GetButtonDown("Jump")) jump.Jumping();
            }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string colliderName = collision.gameObject.name;
        string colliderTag = collision.gameObject.tag;

        if (colliderName == "Enemy " + colliderTag)
        {
            health.TakeDamage(10, "Melee");
        }
    }

    private void PlayerReset()
    {
        sr.enabled = true;
        rb.simulated = true;
    }



    private void Death(string type)
    {
        canMove = false;
        anim.Play("Death");//Add TG for enemy animation number
        //rb.simulated = false;
        //health.OnDeath -= Death; //Unsubscribe to prevent memory leaks
        //Destroy(gameObject, 2.5f); //Destoys attached object after a slight delay **noBueno for player characters with respawn**

        StartCoroutine(DelayDeathLogic());
    }

    private IEnumerator DelayDeathLogic()
    {
        yield return new WaitForSeconds(2f);
        //sr.enabled = false;
        if (InventoryManager.Instance.resources["extraLives"] > 0)
        {
            InventoryManager.Instance.resources["extraLives"]--;
            canMove = true;
            health.ResetHealth();
            //PlayerReset();
            GameManager.Instance.Respawn();
        }
        else
        {
            Debug.Log("Decided the game is over!");
            GameManager.Instance.GameOver();
        }
    }
}

    //    if (InventoryManager.Instance.resources["extraLives"] > 0)//Maybe linked to InventoryManager instead of local Variable?
    //    {
    //        InventoryManager.Instance.resources["extraLives"] --;
    //        canMove = true;
    //        health.ResetHealth();
    //        GameManager.Instance.Respawn();
    //    }
    //    else
    //    {

    //        GameManager.Instance.GameOver();
    //        //What ever other required logic exists for the particular health based destruction
    //    }
    //}