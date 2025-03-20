using UnityEngine;

public class UpdateCheckpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.UpdateCheckpoint(transform);
        }
    }
}
