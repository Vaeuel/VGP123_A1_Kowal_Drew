using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    CircleCollider2D cc2d;
    [SerializeField]public float detectionRadius = 5f;
    public bool chaseRange = false;
    public Transform player;
    private IPlayerDetection dh;
    private Transform detectionZone;
    private LayerMask isGroundLayer;

    private void Awake()
    {
        InitSetUp();//Creates game object for detection script
        dh = GetComponentInParent<IPlayerDetection>();//looks for scripts implimenting 'IPlayerDetection'
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform; //looks for and stores 'Player' object transforms in here
        if (player == null) Debug.LogError("No object tagged 'Player' found in the scene.");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) //Other works only with collisions or trigger events. Other literally means anything other than the object using thsi script.
        {
            chaseRange = true;
            dh?.OnPlayerEnterRange();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            chaseRange = false;
            dh?.OnPlayerExitRange();
        }
    }

    private void InitSetUp()
    {
        if (transform.Find("Detection") == null)
        {
            //Debug.Log("No Detection Zone set. creating one assing pivot is bottom center");

            GameObject newGameObject = new GameObject(); // creates new game object in scene and names it 
            newGameObject.transform.SetParent(transform); // childs the new game object under what ever uses this script
            newGameObject.transform.localPosition = Vector3.zero; // Zeros the new object location local to its' parent
            newGameObject.name = "Detection"; // renames the game object in for the hierarchy
            detectionZone = newGameObject.transform; // Sets and returns the ground check objects trans values to global variable.

            cc2d = newGameObject.AddComponent<CircleCollider2D>();
            cc2d.offset = new Vector2 (0 , 4.5f);
            cc2d.radius = detectionRadius;
            cc2d.isTrigger = true;
            cc2d.excludeLayers = ~isGroundLayer; // Exclude Ground Layer ** ~ symbol indicates only this**

        }
    }
}
