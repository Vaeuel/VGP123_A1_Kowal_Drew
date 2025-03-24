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
    AudioSource audioSource;
    
    //Movement variables
    [Range(.5f, 10)]
    public float speed = 9f;
    [Range(.5f, 10)]
    public float jumpForce = 9f;

    public bool isGrounded = false;
    private bool canMove = true;

    private AudioClip death;
    private AudioClip gunShot;
    #endregion


    private void Awake()
    { //**These lines of code can create unintended behaviour if a component has been disabled intentionally...**
        jump = GetComponent<Jump>() ?? gameObject.AddComponent<Jump>();
        rng = GetComponent<Ranged>() ?? gameObject.AddComponent<Ranged>();
        mle = GetComponent<Melee>() ?? gameObject.AddComponent<Melee>();
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        gc = GetComponent<Ground_check>();
        health = GetComponent<Health>();
        gunShot = Resources.Load<AudioClip>("Audio/Audio_SFX/SingleStarterShot");
        death = Resources.Load<AudioClip>("Audio/Audio_SFX/DeathGrunt");
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
                        audioSource.PlayOneShot(gunShot);
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

        if (hInput != 0) { sr.flipX = (hInput < 0); }

        }        

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

    public void PlayerReset()
    {
        canMove = true;
        rb.simulated = true;
        anim.Rebind();
        anim.Update(0f);
    }

    private void Death(string type)
    {
        canMove = false;
        rb.simulated = false;
        audioSource.PlayOneShot(death);
        anim.Play("Death");//Add TG for enemy animation number

        StartCoroutine(DelayDeathLogic());
    }

    private IEnumerator DelayDeathLogic()
    {
        yield return new WaitForSeconds(2f);

        InventoryManager.Instance.AddResource("extraLives", -1);
        if (InventoryManager.Instance.resources["extraLives"] > 0)
        {

            health.ResetHealth();
            //PlayerReset();
            GameManager.Instance.Respawn();
        }
        else
        {
            health.OnDeath -= Death; //Unsubscribe to prevent memory leaks
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