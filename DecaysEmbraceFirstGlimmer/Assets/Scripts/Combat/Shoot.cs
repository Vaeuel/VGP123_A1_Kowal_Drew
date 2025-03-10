using UnityEngine;


public class Shoot : MonoBehaviour
{
    SpriteRenderer sr;
    Collider2D col;
    Ranged rng;

    [SerializeField] private Vector2 initShotVelocity = Vector2.zero;

    private Transform spawnPoint;

    //[SerializeField] private Projectile projectilePrefab;

    void Start()
    {
        CreateSpawn();
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        rng = GetComponent<Ranged>();
        if (initShotVelocity == Vector2.zero)
        {
            initShotVelocity.x = 7.0f;
            Debug.Log("init Shot Velocity is set to default");
        }

        //if (projectilePrefab != null && projectilePrefab.GetComponent<Projectile>() == null) //Checks if the projectile script is added to the projectile game object that was added to this script
        //{
        //    projectilePrefab.gameObject.AddComponent<Projectile>(); //Adds the projectile script to the game object
        //    Debug.Log($"Added Projectile script to {projectilePrefab.gameObject.name}");
        //}

        if (!spawnPoint ||  !rng.projectilePrefab)
            Debug.Log($"Please set default values on {gameObject.name}.");
    }
    public void Fire()
    {
        //Spawn point being set tto Col.bounds.extent.x isn't working as expected. spawnPoint get set to the bound of teh first animation frame rather than flagged frame. **Needs fixing
        Projectile curProjectile;

        if (!sr.flipX)
        {
            spawnPoint.localPosition = new Vector2(col.bounds.extents.x, 1.75f);  // Position for right side
            curProjectile = Instantiate(rng.projectilePrefab, spawnPoint.position, spawnPoint.rotation);
            curProjectile.SetVelocity(initShotVelocity);
        }

        else
        {
            spawnPoint.localPosition = new Vector2(-col.bounds.extents.x, 0.5f);  // Position for left side
            curProjectile = Instantiate(rng.projectilePrefab, spawnPoint.position, spawnPoint.rotation);
            curProjectile.SetVelocity(initShotVelocity * -1);
        }
    }

    void CreateSpawn()
    {
        GameObject spawnObject = new GameObject("ProjectileSpawnPoint");
        spawnObject.transform.SetParent(transform);
        spawnObject.transform.localPosition = new Vector2();
        spawnPoint = spawnObject.transform;
    }
}
