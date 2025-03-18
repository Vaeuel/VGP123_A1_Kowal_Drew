using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    BoxCollider2D bc2d;

    public float speed = 2f;
    private Transform target;//Player transform from Player Detection VIA Enemy
    public bool isIdle = false;
    public bool isPatrolling = false;
    public bool isChasing = false;
    public bool isDead = false;


    private void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        sr = transform.GetComponent<SpriteRenderer>();
        bc2d = transform.GetComponent<BoxCollider2D>();
        if (bc2d == null) Debug.LogError("BoxCollider2D is missing on " + gameObject.name);
        StartCoroutine(RestOrPatrol());
    }

    public IEnumerator RestOrPatrol()
    {
        while (!isDead && !isChasing)
        {
            if (Random.value < .5f)
            {
                isIdle = true;
                isPatrolling = false;
                //Debug.Log("Idle was chosen");
                yield return new WaitForSeconds(1.5f);
            }

            else
            {
                isPatrolling = true;
                //Debug.Log("Patrol started");
                yield return new WaitForSeconds(2.5f);
            }
        }
    }
    public void Chase(Transform newTarget)
    {
        target = newTarget;

        if (target != null)
        {
            isChasing = true;
            isIdle = false;
            isPatrolling = false;

            Vector2 direction = (target.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, target.position, (speed * 1.5f) * Time.deltaTime);

            if (direction.x > 0)
                sr.flipX = true; // Moving right
            else if (direction.x < 0)
                sr.flipX = false;  // Moving left
        }

        else
        {
            isChasing = false;
        }
    }

    public void Idle()
    {
        rb.linearVelocity = new Vector2 (0f, rb.linearVelocity.y);
        isIdle = true;
        isPatrolling = false;
    }

    public void Patrol()
    {
        isPatrolling = true;
        isIdle = false;
        rb.linearVelocity = (sr.flipX) ? new Vector2(speed, rb.linearVelocity.y) : new Vector2(-speed, rb.linearVelocity.y);
    }

    public void Dead()
    {
        isDead = true;
        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
        isPatrolling = false;
        isIdle = false;
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.CompareTag("Barrier") && bc2d.IsTouching(collision))
        {
            Idle();
            sr.flipX = !sr.flipX;
        }
    }
}
