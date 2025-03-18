using System.Linq.Expressions;
using Unity.Cinemachine;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    [SerializeField] private int bulletDamage = 7;
    [SerializeField] private int playerMeleeDamage = 5;
    [SerializeField] private int enemyMeleeDamage = 3;

    Health health;
    BoxCollider2D bc2d;
    private int damage;
    public string attack;

    private void Start()
    {
        health = GetComponent<Health>();
        bc2d = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (damage != 0) damage = 0;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Something Triggered a collision");

        if (bc2d.IsTouching(other))
        {
            //Debug.Log("Something touched the character Box collider");

            switch (other.gameObject.name)
            {
                case "PBullet":
                    damage = bulletDamage;
                    FindType(other.gameObject);
                    break;
                case "PMelee":
                    damage = playerMeleeDamage;
                    FindType(other.gameObject);
                    break;
                case "EMelee":
                    damage = enemyMeleeDamage;
                    FindType(other.gameObject);
                    break;
                default:
                    Debug.LogWarning($"Unrecognized Collision source: {other.gameObject.name}. Doing nothing.");
                    break;
            }

            if (health != null && damage != 0f)
            {
                //Debug.Log($"Triggering 'TakeDamage()' forwarding {damage} to the health script.");
                health.TakeDamage(damage, attack);
            }
        }
    }

    private void FindType(GameObject obj)
    {
        var projectile = obj.GetComponent<Projectile>();
        
        if (projectile != null)
        {
            attack = projectile.attackType;
            //Debug.Log($"Found attackType in Projectile: {attack}");
            return;
        }
        //To be commented in once Melee attacks exist
        //var melee = obj.GetComponent<Melee>();

        //if (melee != null)
        //{
        //    attack = melee.attackType;
        //    Debug.Log($"Found attackType in Melee: {attack}");
        //    return;
        //}
    }
}
