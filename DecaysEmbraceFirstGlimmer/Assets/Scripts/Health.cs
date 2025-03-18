using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    public int currentHealth;//Can be accessed for UI functionality
    private bool isDead;
    private string type;

    //public event Action HealthChange;//Creates scripting trigger events that can be subscribed to. **This one is for UI functionality**
    public event Action <string> OnDeath;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void ResetHealth()//Intended to aid multilife players. **in the player script add double conditions IE. if(health.isDead && player.1UP) adjust per case use**
    {
        currentHealth = maxHealth;
        isDead = false; // Allow interaction again
    }

    public void TakeDamage(int damage, string dType)
    {
        if (isDead) return;//Prevents unintended use
        currentHealth -= damage;//adjusts health accordingly
        type = dType;

        if (currentHealth <= 0)
        {
            isDead = true;//Set bool to prevent unintentional use
            OnDeath?.Invoke(type);//Triggers what ever script is subscribed to this action
        }
    }
}

/*Use the below chunks in which ever script is using this health system

health.OnDeath += Death;//Subscribes on awake if placed 

private void OnDestroy() //Unity Lifecycle method. **Includes Awake, Start, Update, OnDestroy**
{
    health.OnDeath -= Death; //Unsubscribe to prevent memory leaks
}
 
private void Death(string type)
{
    if (extraLives > 0)//Maybe linked to InventoryManager instead of local Variable?
    {
        extraLives--;
        health.ResetHealth();
    }
    else
    {
        move.Dead();
        anim.Play(type + "Death" + tg);//Add TG for enemy animation number
        Destroy(gameObject, 2f); //Destoys attached object after a slight delay
        //What ever other required logic exists for the particular health based destruction
    }
}*/
