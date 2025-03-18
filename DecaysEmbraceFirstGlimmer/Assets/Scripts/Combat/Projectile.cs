using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField, Range(1, 20)] private float lifeTime = 1.0f;
    [SerializeField] public string attackType;
    Rigidbody2D rb;
    CircleCollider2D cc2d;

    

    void Awake()
    {
        rb = gameObject.AddComponent<Rigidbody2D>();
        cc2d = gameObject.AddComponent<CircleCollider2D>();
        cc2d.isTrigger = true;
    }

    void Start()
    {
        if (rb == null) Debug.Log("Warning, no projectile assigned");

        Destroy (gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<BoxCollider2D>() != null || other.CompareTag("Ground"))
        {
            Destroy(gameObject, .07f);
        }
    }

    public void InitProj(string type)
    {
        
        attackType = type;
    }

    public void SetVelocity(Vector2 velocity)
    {
        rb.linearVelocity = velocity;
    }
}