using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : BaseMenu
{
    public Button backBtn;
    public Slider volSlider;
    public TMP_Text volSliderText;
    

    public override void Init(MenuController context)
    {
        base.Init(context);
        state = MenuStates.Settings;//Sets the name of this state in the Dictionary

        if (backBtn) backBtn.onClick.AddListener(JumpBack);

        if (volSlider)
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
}
