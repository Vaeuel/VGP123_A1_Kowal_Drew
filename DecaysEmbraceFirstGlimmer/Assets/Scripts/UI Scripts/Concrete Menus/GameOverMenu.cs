using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameOverMenu : BaseMenu
{
    public Button restartBtn;
    
    public override void Init(MenuController context)
    {
        base.Init(context);
        state = MenuStates.GameOver;

        if (restartBtn) restartBtn.onClick.AddListener(QuitToMainMenu);
    }

    private void QuitToMainMenu()
    {
        //UnloadLevel(); //This Function isn't required as nothing from the game gets moved int othis scene
        SceneManager.LoadScene("TitleScreen");
    }

    public void UnloadLevel()
    {
        ExitState();
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        GameObject[] allGameObjects = FindObjectsByType<GameObject>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (GameObject gameObject in allGameObjects)
        {
            Destroy(gameObject);
        }
    }
}