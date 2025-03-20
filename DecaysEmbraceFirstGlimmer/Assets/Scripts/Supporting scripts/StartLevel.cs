using UnityEngine;

public class StartLevel : MonoBehaviour
{
    [SerializeField]private Transform startPos;

    void Start() => GameManager.Instance.InitPlayer(startPos); //Expression body Function. **these use the "Little arrow"**
}
