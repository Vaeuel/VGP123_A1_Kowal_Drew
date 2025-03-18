using UnityEngine;

[RequireComponent (typeof(SpriteRenderer), typeof(Animator))]
public class Enemy : MonoBehaviour, IPlayerDetection
{
    SpriteRenderer sr;
    Animator anim;
    Movement move;
    PlayerDetection pd;
    Rigidbody2D rb;
    Health health;
    DamageHandler dh;

    private string tg;
    private bool wasChasing = false;

    private void Awake()
    {
        InitSetUp();
        health.OnDeath += Death;
    }

    private void OnDestroy()
    {
        health.OnDeath -= Death;
    }

    void Update()
    {
        if (pd && move != null)
        {
            if(pd.chaseRange)
            {
                move.Chase(pd.player);
                wasChasing = true;
                anim.Play("Walk" + tg);
                //Debug.Log("Enemy: Chase should be active");
            }

            if (wasChasing && pd.chaseRange == false)
            {
                move.Chase(null);
                move.StartCoroutine(move.RestOrPatrol());
                wasChasing = false;
            }

            if (move.isIdle) anim.Play("Idle" + tg);

            if (move.isPatrolling)
            {
                move.Patrol(); //Might create conflict once attack and death animations are added **will need to carfully manage conditions**
                anim.Play("Walk" + tg);
            }
        }
    }

private void Death(string type)
    {
        move.Dead();
        anim.Play(type + "Death" + tg);//Add TG for enemy animation number
        Destroy(gameObject, 2.5f); //Destoys attached object after a slight delay

    }

    void InitSetUp()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
        rb.freezeRotation = true;
        move = gameObject.AddComponent<Movement>();
        pd = gameObject.AddComponent<PlayerDetection>();//May not be applied to the correct location
        health = gameObject.AddComponent<Health>();
        dh = gameObject.AddComponent<DamageHandler>();
        tg = gameObject.tag;
        //Debug.Log("TG is set to " + tg);
    }

    public void OnPlayerEnterRange()
    {
        Debug.Log("Enemy detected the player!");
    }

    public void OnPlayerExitRange()
    {
        Debug.Log("Enemy lost sight of the player.");
    }
}
