using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    //main
    public Dropdown Audio;
    public Dropdown UIScale;
    //graphics
    public Dropdown ShadowQuality;
    public Dropdown MotionBlur;
    public Dropdown LensBlur;
    public Dropdown AntiAliasing;
    public Dropdown ResolutionScaling;
    public Dropdown DynamicReflections;
    public Dropdown ChromaticAbberation;
    public Dropdown AmbientOcclusion;
    //Debug
    public Dropdown ShowFPS;
    public Button ResetSave;
    public InputField CodeInput;
    // Start is called before the first frame update
    void Start()
    {
        ResolutionScaling.value = SaveSystem.SaveObject.SettingsData.ResolutionScaling;
        Audio.value = SaveSystem.SaveObject.SettingsData.EnableAudio?1:0;
        DynamicReflections.value = SaveSystem.SaveObject.SettingsData.HQReflections;
        ShadowQuality.value = SaveSystem.SaveObject.SettingsData.ShadowQuality;
        MotionBlur.value = SaveSystem.SaveObject.SettingsData.MotionBlur;
        AntiAliasing.value = SaveSystem.SaveObject.SettingsData.AntiAliasing;
        LensBlur.value = SaveSystem.SaveObject.SettingsData.LensBlur;
        AmbientOcclusion.value = SaveSystem.SaveObject.SettingsData.AmbientOcclusion;
        ChromaticAbberation.value = SaveSystem.SaveObject.SettingsData.ChromaticAberration;
        UIScale.value = SaveSystem.SaveObject.SettingsData.UIScale;
        ShowFPS.value= SaveSystem.SaveObject.SettingsData.ShowFPS ? 1 : 0;
        switch (ShadowQuality.value)
        {
            case 0:
                QualitySettings.shadowResolution = ShadowResolution.Low;
                break;
            case 1:
                QualitySettings.shadowResolution = ShadowResolution.Medium;
                break;
            case 2:
                QualitySettings.shadowResolution = ShadowResolution.High;
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    public void Save()
    {
        SaveSystem.SaveObject.SettingsData.ResolutionScaling = ResolutionScaling.value;
        SaveSystem.SaveObject.SettingsData.EnableAudio = Audio.value==1;
        SaveSystem.SaveObject.SettingsData.HQReflections = DynamicReflections.value;
        SaveSystem.SaveObject.SettingsData.LensBlur = LensBlur.value;
        SaveSystem.SaveObject.SettingsData.MotionBlur = MotionBlur.value;
        SaveSystem.SaveObject.SettingsData.ShadowQuality = ShadowQuality.value;
        SaveSystem.SaveObject.SettingsData.AntiAliasing = AntiAliasing.value;
        SaveSystem.SaveObject.SettingsData.AmbientOcclusion = AmbientOcclusion.value;
        SaveSystem.SaveObject.SettingsData.ChromaticAberration = ChromaticAbberation.value;
        SaveSystem.SaveObject.SettingsData.UIScale = UIScale.value;
        SaveSystem.SaveObject.SettingsData.ShowFPS = ShowFPS.value == 1;
        SaveSystem.AddLog(new Save.LogEntry("Settings","Saved user settings"));
        switch (ShadowQuality.value)
        {
            case 0:
                QualitySettings.shadowResolution = ShadowResolution.Low;
                break;
            case 1:
                QualitySettings.shadowResolution = ShadowResolution.Medium;
                break;
            case 2:
                QualitySettings.shadowResolution = ShadowResolution.High;
                break;
            default:
                break;
        }
        
        SaveSystem.SaveData();
    }
    public void ResetProgress()
    {
        PlayerPrefs.DeleteKey("Save");
        Application.Quit();
    }
    public void EnterCode()
    {
        switch (CodeInput.text)
        {
            default:
                break;
        }
    }
}
