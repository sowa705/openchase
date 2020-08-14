using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SSLog : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string s="";
        foreach (var item in SaveSystem.SaveObject.SaveLog)
        {
            s += $"[{DateTime.FromFileTimeUtc(item.Time)}] ({item.Subsystem}) {item.Message}\n";
        }
        GetComponent<Text>().text =s;
    }
}