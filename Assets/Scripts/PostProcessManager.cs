using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PostProcessProfile profile = GetComponent<PostProcessVolume>().profile;
        profile.GetSetting<MotionBlur>().enabled.overrideState = SaveSystem.SaveObject.SettingsData.MotionBlur == 1;
        profile.GetSetting<DepthOfField>().enabled.overrideState = SaveSystem.SaveObject.SettingsData.LensBlur == 1;
        profile.GetSetting<ChromaticAberration>().enabled.overrideState = SaveSystem.SaveObject.SettingsData.ChromaticAberration == 1;
        profile.GetSetting<AmbientOcclusion>().enabled.overrideState = SaveSystem.SaveObject.SettingsData.AmbientOcclusion == 1;

    }
}
