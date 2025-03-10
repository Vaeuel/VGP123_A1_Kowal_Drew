using UnityEngine;



public class Ranged : MonoBehaviour
{
    Animator anim;
    Shoot shoot;

    [SerializeField] public Projectile projectilePrefab; //Projectile field in inspector doesn't accept prefab item being dragged in **maybe wrong field type?

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        shoot = GetComponent<Shoot>() ?? gameObject.AddComponent<Shoot>();

        if (projectilePrefab != null && projectilePrefab.GetComponent<Projectile>() == null) //Checks if the projectile script is added to the projectile game object that was added to this script
        {
            projectilePrefab.gameObject.AddComponent<Projectile>(); //Adds the projectile script to the game object
            Debug.Log($"Added Projectile script to {projectilePrefab.gameObject.name}");
        }
    }

    public void Fire()
    {
        anim.SetTrigger("Shooting");
        shoot.Fire();
    }
}
