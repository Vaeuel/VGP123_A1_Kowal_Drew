using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    public Dictionary<string, int> resources = new Dictionary<string, int>();

    private Dictionary<string, int> defaultResources = new Dictionary<string, int>
    {
        { "Wood", 0 },
        { "Stone", 0 },
        { "NPC", 0 },
        { "extraLives", 3 }
    };

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            ResetInventory();
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetInventory()
    {
        resources = new Dictionary<string, int>(defaultResources);
    }

    public void AddResource(string resourceName, int amount)
    {
        if (resources.ContainsKey(resourceName))
        {
            resources[resourceName] += amount;
            Debug.Log($"{resourceName} count: {resources[resourceName]}"); //$ indicates string interpolation and allows imbedding Variables directly using {}.
            UpdateUI(resourceName);
        }

        else
        {
            Debug.LogWarning($"Resource '{resourceName}' does not exist in the inventory.");
        }
    }

    private void UpdateUI(string resourceName)
    {
        //Future UI Logic
    }

    public void AreaConditional() //Will display when the player tries to leave the area
    {
        foreach (var item in resources)
        {
            Debug.Log($"Player has {item.Value} {item.Key}");
        }
    }
}
