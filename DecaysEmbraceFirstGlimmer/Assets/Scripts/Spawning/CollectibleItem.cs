using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log($"{gameObject.name} collided with {other.name}");
        if (!other.CompareTag("Player")) return;

        switch (gameObject.name)
        {
            case "Wood":
                InventoryManager.Instance.AddResource("Wood", 1);
                break;

            case "Stone":
                InventoryManager.Instance.AddResource("Stone", 1);
                break;

            case "NPC":
                InventoryManager.Instance.AddResource("NPC", 1);
                break;

            default:
                Debug.LogWarning($"Unknown collectible item: {gameObject.name}"); //$ indicates string interpolation and allows imbedding Variables directly using {}.
                return;
        }

        Destroy(gameObject);
    }
}