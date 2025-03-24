using System;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public event Action OnDestroy;

    public AudioClip Wood_Pu;
    public AudioClip Stone_Pu;
    public AudioClip NPC_Pu;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Wood_Pu = Resources.Load<AudioClip>("Audio/Audio_SFX/Wood_PU");
        Stone_Pu = Resources.Load<AudioClip>("Audio/Audio_SFX/Stone_PU");
        NPC_Pu = Resources.Load<AudioClip>("Audio/Audio_SFX/NPC_PU");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log($"{gameObject.name} collided with {other.name}");
        if (!other.CompareTag("Player")) return;

        switch (gameObject.name)
        {
            case "Wood":
                audioSource.PlayOneShot(Wood_Pu);
                InventoryManager.Instance.AddResource("Wood", 1);
                break;

            case "Stone":
                audioSource.PlayOneShot(Stone_Pu);
                InventoryManager.Instance.AddResource("Stone", 1);
                break;

            case "NPC":
                audioSource.PlayOneShot(NPC_Pu);
                InventoryManager.Instance.AddResource("NPC", 1);
                break;

            default:
                Debug.LogWarning($"Unknown collectible item: {gameObject.name}"); //$ indicates string interpolation and allows imbedding Variables directly using {}.
                return;
        }

        OnDestroy?.Invoke();
    }
}