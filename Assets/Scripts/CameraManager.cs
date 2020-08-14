using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
public class CameraManager : MonoBehaviour
{
    PostProcessLayer layer;
    float currentScale;
    void Start()
    {
        layer = GetComponent<PostProcessLayer>();
    }

    // Update is called once per frame
    void Update()
    {
        float newScale = (4f - SaveSystem.SaveObject.SettingsData.ResolutionScaling) / 4.0f;
        if (currentScale!=newScale)
        {
            currentScale = newScale;
            ScalableBufferManager.ResizeBuffers(newScale, newScale);
        }
        switch (SaveSystem.SaveObject.SettingsData.AntiAliasing)
        {
            case 0:
                layer.antialiasingMode = PostProcessLayer.Antialiasing.None;
                break;
            case 1:
                layer.antialiasingMode = PostProcessLayer.Antialiasing.FastApproximateAntialiasing;
                break;
            case 2:
                layer.antialiasingMode = PostProcessLayer.Antialiasing.TemporalAntialiasing;
                break;
            default:
                break;
        }
       
        if (SaveSystem.SaveObject.SettingsData.AntiAliasing!=0||SaveSystem.SaveObject.SettingsData.LensBlur!=0|| SaveSystem.SaveObject.SettingsData.AmbientOcclusion != 0|| SaveSystem.SaveObject.SettingsData.MotionBlur != 0|| SaveSystem.SaveObject.SettingsData.ChromaticAberration != 0)
        {
            layer.enabled = true;
        }
        else
        {
            layer.enabled = false;
        }
    }
}
