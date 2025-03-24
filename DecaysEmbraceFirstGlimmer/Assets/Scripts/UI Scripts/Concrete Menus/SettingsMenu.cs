using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class SettingsMenu : BaseMenu
{
    public Button backBtn;

    [Header("Sliders")]
    public Slider volSlider;
    public Slider soundFXSlider;
    public Slider musicSlider;

    [Header("Slider Text")]
    public TMP_Text volSliderText;
    public TMP_Text soundFXText;
    public TMP_Text musicText;
    

    public override void Init(MenuController context)
    {
        base.Init(context);
        state = MenuStates.Settings;//Sets the name of this state in the Dictionary

        if (backBtn) backBtn.onClick.AddListener(JumpBack);

        SetupSlider(volSlider, volSliderText, "masterVol");
        SetupSlider(soundFXSlider, soundFXText, "sfxVol");
        SetupSlider(musicSlider, musicText, "musicVol");
    }

    private void SetupSlider(Slider slider, TMP_Text text, string parameterName)
    {
        if (slider)
        {
            slider.onValueChanged.AddListener(value => OnSliderValueChanged(slider, text, value, parameterName));
            OnSliderValueChanged(slider, text, slider.value, parameterName);
        }
    }

    private void OnSliderValueChanged(Slider slider, TMP_Text text, float value, string parameterName)
    {
        if (text)
        {
            value = (value == 0f) ? -80f : 20f * Mathf.Log10(slider.value);
            //float roundedValue = Mathf.Round(value * 100);
            text.text = (value == -80f) ? "0%" : $"{slider.value * 100}%";
            //mixer.SetFloat(parameterName, value); 
        }
    }
    //    if (volSlider)
    //    {
    //        volSlider.onValueChanged.AddListener(OnSliderValueChanged);
    //        OnSliderValueChanged(volSlider.value);
    //    }


    //}

    //private void OnSliderValueChanged(float value)
    //{
    //    float roundedValue = Mathf.Round(value * 100);
    //    if (volSliderText) volSliderText.text = $"{roundedValue}%";
    //}
}
