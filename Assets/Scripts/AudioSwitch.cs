using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ChangeAudio);
        GetComponentInChildren<Text>().text = SaveSystem.SaveObject.SettingsData.EnableAudio ? "Audio" : "No audio";
        AudioListener.volume = SaveSystem.SaveObject.SettingsData.EnableAudio ? 1 : 0;
    }

    // Update is called once per frame
    void ChangeAudio()
    {
        SaveSystem.SaveObject.SettingsData.EnableAudio = !SaveSystem.SaveObject.SettingsData.EnableAudio;
        GetComponentInChildren<Text>().text = SaveSystem.SaveObject.SettingsData.EnableAudio ? "Audio" : "No audio";
        AudioListener.volume = SaveSystem.SaveObject.SettingsData.EnableAudio ? 1 : 0;
    }
}
