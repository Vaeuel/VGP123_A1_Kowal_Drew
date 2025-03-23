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

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        //string sceneName = SceneManager.GetActiveScene().name == "GameOver" ? "TitleScreen" : 
    //        //    SceneManager.GetActiveScene().name == "TitleScreen" ? "Forest Area 1" : "GameOver";
            
    //        SceneManager.LoadScene("TitleScreen");
    //    }
    //}

    public void GameOver()
    {
        ResetGame();

        SceneManager.LoadScene("GameOver");
    }

    public void ResetGame()
    {
        GameObject player = GameObject.Find("Player");

        if (player != null)
        {
            Destroy(player);
        }
        else
        {
            Debug.LogWarning("Player object not found Mother Fucker!");
        }

        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.ResetInventory(); // Reset inventory before restarting
        }
    }

    public void Respawn()
    {
        if (_playerInstance != null)
        {
            PlayerControl pc = _playerInstance.GetComponent<PlayerControl>();

            _playerInstance.transform.position = currentCheckPoint.position;

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
        _playerInstance.name = playerPreFab.name;
    }

    public void UpdateCheckpoint(Transform updatedCheckPoint)
    {
        currentCheckPoint = updatedCheckPoint;
    }
}
