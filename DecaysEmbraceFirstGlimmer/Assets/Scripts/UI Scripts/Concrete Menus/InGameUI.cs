using TMPro;
using UnityEngine;

public class InGameUI : BaseMenu
{
    public Health health;

    public TMP_Text livesText;
    public TMP_Text healthText;
    public TMP_Text woodText;
    public TMP_Text stoneText;
    public TMP_Text npcText;

    public override void Init(MenuController context)
    {
        base.Init(context);
        state = MenuStates.InGame;
    }

    private void OnEnable()
    {
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.InventoryChange += UpdateInventoryUI;

            foreach (var resource in InventoryManager.Instance.resources)
            {
                UpdateInventoryUI(resource.Key, resource.Value);
            }
        }

        if (healthText)
        {
            health.HealthChanged += UpdateHealthUI;
            UpdateHealthUI(health.currentHealth);
        }
    }

    private void OnDisable()
    {
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.InventoryChange -= UpdateInventoryUI;
        }

        if (healthText)
        {
            health.HealthChanged -= UpdateHealthUI;
            UpdateHealthUI(health.currentHealth);
        }
    }

    private void UpdateHealthUI(int value) => healthText.text = $"Health: {health.currentHealth}";

    private void UpdateInventoryUI(string resourceName, int amount)
    {
        if (InventoryManager.Instance != null)
        {
            livesText.text = $"Lives: {InventoryManager.Instance.resources["extraLives"]}";
            woodText.text = $"Wood: {InventoryManager.Instance.resources["Wood"]}";
            stoneText.text = $"Stone: {InventoryManager.Instance.resources["Stone"]}";
            npcText.text = $"NPCs: {InventoryManager.Instance.resources["NPC"]}";
        }
    }
}
