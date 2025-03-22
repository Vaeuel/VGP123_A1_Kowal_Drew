using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;// '=>' creates a pointer in here
    public Transform currentCheckPoint;

    #region PLAYER INSTANCE INFO 
    [SerializeField] private GameObject playerPreFab; //Still need to drag the Player prefab into the proper field within the inspector
    private GameObject _playerInstance;

    public GameObject PlayerInstance => _playerInstance;
    #endregion 

    void Awake()
    {
        //Creates a Singleton **Useful for only making one object that is supposed to persist through game states.
        if (!_instance)//checks for Game manager and adds it automatically if it's not in existance
        {
            _instance = this;
            DontDestroyOnLoad(this);
            return;
        }

        Destroy(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            string sceneName = SceneManager.GetActiveScene().name == "GameOver" ? "TitleScreen" : 
                SceneManager.GetActiveScene().name == "TitleScreen" ? "Forest Area 1" : "GameOver";

            SceneManager.LoadScene(sceneName);
        }
    }

    public void GameOver()
    {
        Debug.Log("Respawn logic can go here");
        //Add animations maybe before changing positions
        GameObject player = GameObject.Find("Player");
        Debug.Log("Tried to find player");

        if (player != null)
        {
            Debug.Log("Player found for use in Respawn");
            Destroy(player);
            Debug.Log("Player Destroyed");
        }
        else
        {
            Debug.LogWarning("Player object not found Mother Fucker!");
        }
        Debug.Log("Game over goes here");

        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.ResetInventory(); // Reset inventory before restarting
        }

        SceneManager.LoadScene("GameOver");
    }

    public void Respawn()
    {
        if (_playerInstance != null)
        {
            PlayerControl pc = _playerInstance.GetComponent<PlayerControl>();

            _playerInstance.transform.position = currentCheckPoint.position;
            Debug.Log("Player respawned at checkpoint.");

            pc.PlayerReset();
        }
        else
        {
            Debug.LogWarning("No player instance found during respawn");
        }
    }

    public void InitPlayer(Transform spawnLocation)
    {
        currentCheckPoint = spawnLocation;

        _playerInstance = Instantiate(playerPreFab, currentCheckPoint.position, Quaternion.identity);
        Debug.Log("playerInstance set equal to Instantiation");
        _playerInstance.name = playerPreFab.name;
        Debug.Log("Instance renamed");

    }

    public void UpdateCheckpoint(Transform updatedCheckPoint)
    {
        currentCheckPoint = updatedCheckPoint;
        Debug.Log($"Current checkpoint {currentCheckPoint} set equal to updatedCheckPoint {updatedCheckPoint}");
    }
}
