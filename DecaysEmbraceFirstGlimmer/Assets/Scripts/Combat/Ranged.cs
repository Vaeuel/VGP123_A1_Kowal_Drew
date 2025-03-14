using UnityEditor.Rendering;
using UnityEngine;
using static Spawner;



public class Ranged : MonoBehaviour
{
    Animator anim;
    Shoot shoot;

    public enum UsesBullet { Player, Enemy }//Creates drop down menu and options
    public UsesBullet ub; //Displays name and DD menu

    [SerializeField] public GameObject projectilePrefab; //Projectile field in inspector doesn't accept prefab item being dragged in **maybe wrong field type?

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        shoot = GetComponent<Shoot>() ?? gameObject.AddComponent<Shoot>();
        ub = gameObject.name.Contains("Player") ? UsesBullet.Player : UsesBullet.Enemy;//Not scalable, needs to search for object name and set ub equal to.

        if (ub == UsesBullet.Player)
        {
            GameObject pbpf = Resources.Load<GameObject>("Scenes/Prefabs/PBullet"); //Creates temp object to store prefab**MUST have full directory**
            //Debug.Log("Player is using The Bullet");

            if (pbpf != null)
            {
                //Debug.Log("Temp object is not null");
                if (pbpf.GetComponent<Projectile>() == null)
                {
                    pbpf.AddComponent<Projectile>();
                    //Debug.Log("Added Projectile script to prefab projectile object");
                }

                projectilePrefab = pbpf;
                //Debug.Log("Projectile Prefab is set to; " + pbpf.name);
            }

            else
            {
                Debug.Log("Bullet prefab is not found under Resources, or Resources Folder doesn't exist by default. Check folder structure for compatibility");
            }
           
        }

        //Might need to add delay so the script can be set a frame after the prefab is added to this script

        if (projectilePrefab != null && projectilePrefab.GetComponent<Projectile>() == null) //Checks if the projectile script is added to the projectile game object that was added to this script
        {
            projectilePrefab.gameObject.AddComponent<Projectile>(); //Adds the projectile script to the game object
            Debug.Log($"Added Projectile script to {projectilePrefab.gameObject.name}");
        }

    }

    public void Fire()
    {
        anim.SetTrigger("Shooting");
    }
}
