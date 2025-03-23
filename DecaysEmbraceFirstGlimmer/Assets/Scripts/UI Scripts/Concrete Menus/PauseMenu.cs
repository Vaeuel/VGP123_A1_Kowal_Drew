using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : BaseMenu
{
    public Button resumeBtn;
    public Button mainMenuBtn;
    public Button quitBtn;

    public override void Init(MenuController context)
    {
        base.Init(context);
        state = MenuStates.Pause;

        if(resumeBtn) resumeBtn.onClick.AddListener(ResumeGame);
        if(mainMenuBtn) mainMenuBtn.onClick.AddListener(QuitToMainMenu);
        if (quitBtn) quitBtn.onClick.AddListener(QuitGame);
    }

    private void QuitToMainMenu()
    {
        UnloadLevel();
        SceneManager.LoadScene("TitleScreen");
    }

    public void UnloadLevel()
    {
        ExitState();
        GameManager.Instance.ResetGame();
    }


    //private void ResetGame()
    //{
    //    // Reset Inventory
    //    if (InventoryManager.Instance != null)
    //    {
    //        InventoryManager.Instance.ResetInventory();
    //    }

    //    // Destroy persistent objects
    //    foreach (var obj in FindObjectsOfType<DontDestroyOnLoad>())
    //    {
    //        Destroy(obj.gameObject);
    //    }

    //    // Reset time scale (in case game was paused)
    //    Time.timeScale = 1;
    //}
public override void EnterState()
    {
        Time.timeScale = 0;
    }

    public override void ExitState()
    {
        Time.timeScale = 1;
    }

    private void ResumeGame()
    {
        JumpBack();
    }
}