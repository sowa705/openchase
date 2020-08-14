using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HardMode : MonoBehaviour
{
    Toggle toggle;
    public static bool Mode;
    // Start is called before the first frame update
    void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(Checked);
        Mode = SaveSystem.SaveObject.PlayerData.HardModeSelected;
        toggle.isOn = Mode;
    }

    void Checked(bool checkvalue)
    {
        Mode = checkvalue;
        SaveSystem.SaveObject.PlayerData.HardModeSelected = Mode;
        SaveSystem.SaveData();
    }
    int Round(int[] arr)
    {
        int a=0;
        foreach (var item in arr)
        {
            a += item;
        }
        a /= arr.Length;
        return a;
    }
}
