using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(CollectibleItem))]

public class Spawner : MonoBehaviour
{
    Animator anim;
    SpriteRenderer sr;
    BoxCollider2D bc2d;
    public enum SpawnType { Item, Enemy }
    public SpawnType spawnType;

    //Class specific variables. These will ensure that all instances of this script only produce X resources
    private static int woodCount = 0;
    private static int stoneCount = 0;
    private static int npcCount = 0;
    private static HashSet<string> usedNPCs = new HashSet<string>(); //Make sure it's static so all instants share the same variable!!

    private const int maxWood = 15;
    private const int maxStone = 15;
    private const int maxNPCs = 5;

    private void Awake()
    {
        InitSetup();
    }

    void Start()
    {
        anim = GetComponent<Animator>();

        if (spawnType == SpawnType.Enemy) //SpawnEnemy();
        {
            // need to add movement ai type controls. the enemy script can have combat and etc. **Don't forget to gameObject.AddComponent<Enemy>(); or which ever script matchs**
            Debug.Log("Please add enemy scripts to the Spawner script.");
            return;
        }

        if (spawnType == SpawnType.Item) SpawnItem();

        StartCoroutine(SetColliderSizeAfterStart()); //Yields control to UNity for a time **Requires IEnumerator to function**
    }

    private IEnumerator SetColliderSizeAfterStart()
    {
        yield return null; //Waits one frame after start **9 other terms exist ranging from frame and time to conditions and functions (Ask Chat)**

        bc2d.size = new Vector2(.5f, sr.bounds.size.y);
        bc2d.offset = new Vector2(0, (sr.bounds.size.y / 2));
    }

    private void SpawnItem()
    {
        //Randomly sets animations based on total counts
        if (woodCount < maxWood && Random.value < 0.33f)
        {
            gameObject.name = "Wood";
            anim.Play("Wood");
            woodCount++;
            Debug.Log("Wood count is " + woodCount);
        }

        else if (stoneCount < maxStone && Random.value < 0.66f) //Might have trouble as .33 is less than .66. Though if less than .33 then should be wood.
        {
            gameObject.name = "Stone";
            anim.Play("Stone");
            stoneCount++;
            Debug.Log("Stone count is " + stoneCount);
        }

        else if (npcCount < maxNPCs)
        {
            string uniqueNPC = GetUniqueNPC(); //Creates a local variable and sets it == to a naming function
            //Debug.Log("Post GetUniqueNPC() UniqueNPC is " + uniqueNPC);
            if (uniqueNPC != null) //if a unique name is returned then this runs...
            {
                gameObject.name = "NPC";
                ++npcCount;
                anim.Play(uniqueNPC);
                //Debug.Log("NPC count is " + npcCount + ", and UniqueNPC is " + uniqueNPC);
            }
        }
        gameObject.AddComponent<CollectibleItem>();
    }

    private string GetUniqueNPC()
    {
        if (npcCount >= maxNPCs) return null; //Skips this process if the npc count is full

        string nameTry = null;
        //Debug.Log("Pre while loop nameTry is " + nameTry);
        while (nameTry == null || usedNPCs.Contains (nameTry)) //Runs until an unused value is found **Will run forever without being controlled elsewhere if all available names are used**
        {
            int randomNum = Random.Range(1, maxNPCs +1); //Random number between 1 and 5 to match animation naming convention
            nameTry = "NPC" + randomNum.ToString(); //Concatenates NPC and random number
            //Debug.Log("Inter while loop nameTry is " + nameTry);
        }

        usedNPCs.Add (nameTry); //Adds to the global hashset

        return nameTry; //Returns string
    }

    void InitSetup()
    {
        sr = gameObject.AddComponent<SpriteRenderer>();
        sr.sortingLayerName = "Play Area";
        
        anim = gameObject.AddComponent<Animator>();
        anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("anim/Collectibles/Collectible");

        bc2d = gameObject.AddComponent<BoxCollider2D>();
        bc2d.isTrigger = true;
    }
}