using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class CanvasManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button startBtn;
    public Button settingsBtn;
    public Button backBtn;

    public Button quitBtn;
    public Button resumeBtn;
    public Button mainMenuBtn;



    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject pauseMenu;

    [Header("Text")]
    public TMP_Text volSliderText;
    public TMP_Text livesText;

    [Header("Sliders")]
    public Slider volSlider;

    private GameObject previousMenu;
    private GameObject currentMenu;

    void Start()
    {
        if (startBtn) startBtn.onClick.AddListener(() => SceneManager.LoadScene("Forest Area 1"));
        if (settingsBtn) settingsBtn.onClick.AddListener(() => SetMenus(settingsMenu, mainMenu));//creates an anonymous function that passes values forward?
        if (backBtn) backBtn.onClick.AddListener(() => SetMenus(previousMenu, currentMenu));
        //if (quitBtn) quitBtn.onClick.AddListener(QuitGame);
        if (resumeBtn) resumeBtn.onClick.AddListener(() => SetMenus (null, pauseMenu));
        if (mainMenuBtn) mainMenuBtn.onClick.AddListener(() => SceneManager.LoadScene("TitleScreen"));
 
        if(volSlider)
        {
            volSlider.onValueChanged.AddListener(OnSliderValueChanged);
            OnSliderValueChanged(volSlider.value);
        }
    }

    private void OnSliderValueChanged(float value)
    {
        float roundedValue = Mathf.Round(value * 100);
        if (volSliderText) volSliderText.text = $"{roundedValue}%";
    }

    private void OnDisable()
    {
        if (startBtn) startBtn.onClick.RemoveAllListeners();
        if (settingsBtn) settingsBtn.onClick.RemoveAllListeners();
        if (backBtn) backBtn.onClick.RemoveAllListeners();
        if (quitBtn) quitBtn.onClick.RemoveAllListeners();
        if (resumeBtn) resumeBtn.onClick.RemoveAllListeners();
        if (mainMenuBtn) mainMenuBtn.onClick.RemoveAllListeners();
    }

    private void SetMenus(GameObject menuToActivate, GameObject menuToDeactivate)
    {
        currentMenu = menuToActivate;
        previousMenu = menuToDeactivate;
        if (menuToActivate) menuToActivate.SetActive(true);
        if (menuToDeactivate) menuToDeactivate.SetActive(false);
    }



    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu) return;

        if(Input.GetKeyDown(KeyCode.P))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);

            if(pauseMenu.activeSelf)
            {

            }
            else
            {

            }
        }

    }
}
