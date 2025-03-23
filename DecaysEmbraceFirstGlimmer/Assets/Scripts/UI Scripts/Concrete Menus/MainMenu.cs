using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class MainMenu : BaseMenu
{
    public Button startBtn;
    public Button settingsBtn;
    public Button quitBtn;

    public override void Init(MenuController context)
    {
        base.Init(context);
        state = MenuStates.MainMenu;

        if (startBtn) startBtn.onClick.AddListener(() => SceneManager.LoadScene("Forest Area 1"));
        if (settingsBtn) settingsBtn.onClick.AddListener(() => SetNextMenu(MenuStates.Settings));//creates an anonymous function that passes values forward?
        if (quitBtn) quitBtn.onClick.AddListener(QuitGame);
    }
}
