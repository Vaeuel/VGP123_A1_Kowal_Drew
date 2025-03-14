using UnityEngine;


public class Shoot : MonoBehaviour
{
    SpriteRenderer sr;
    BoxCollider2D col; //Will break if another collider type is used.**This is a temporary fix**
    Ranged rng;
    Animator anim;

    [SerializeField] private Vector2 initShotVelocity = Vector2.zero;

    private Transform spawnPoint;

    
    void Start()
    {
        CreateSpawn();
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();//Pt.2 of the above issue
        rng = GetComponent<Ranged>();
        anim = GetComponent<Animator>();

        if (initShotVelocity == Vector2.zero)
        {
            initShotVelocity.x = 25.0f;
            Debug.Log("init Shot Velocity is set to default");
        }

        if (!spawnPoint ||  !rng.projectilePrefab)
            Debug.Log($"Please set default values on {gameObject.name}.");
    }
    public void Fire()
    {
        //Spawn point being set tto Col.bounds.extent.x isn't working as expected. spawnPoint get set to the bound of teh first animation frame rather than flagged frame. **Needs fixing
        Projectile curProjectile;
        anim.ResetTrigger("Shooting");
        //ColliderResize();//Defunct.

        if (!sr.flipX)
        {
            spawnPoint.localPosition = new Vector2(col.bounds.extents.x +3.5f, 2.5f);  // Position for right side
            curProjectile = Instantiate(rng.projectilePrefab, spawnPoint.position, spawnPoint.rotation).GetComponent<Projectile>();
            curProjectile.SetVelocity(initShotVelocity);
        }

        else
        {
            spawnPoint.localPosition = new Vector2(-col.bounds.extents.x - 4.5f, 2.5f);  // Position for left side
            curProjectile = Instantiate(rng.projectilePrefab, spawnPoint.position, spawnPoint.rotation).GetComponent<Projectile>();
            curProjectile.SetVelocity(new Vector2(-initShotVelocity.x, initShotVelocity.y));
            curProjectile.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    void CreateSpawn()
    {
        GameObject spawnObject = new GameObject("ProjectileSpawnPoint"); //Creates and names a new spawnpoint object
        spawnObject.transform.SetParent(transform);//Parents the new object to Game object that this script is attached to (in this case rng to PC to Player)
        spawnObject.transform.localPosition = new Vector2();// Zeros the tansforms of the object
        spawnPoint = spawnObject.transform;//Passes this objects transforms out to the global spawnPoint value
    }

    //void ColliderResize() //Didn't work. **I would need to resize the Collider on every animation**
    //{
    //    col.size = new Vector2(sr.bounds.size.x, sr.bounds.size.y);
    //}
}
