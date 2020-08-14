using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VersionText : MonoBehaviour
{
    string Version = "1.0";
    string Date ="13.08.2020";
    bool date;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = Version;
    }
    public void Click()
    {
        date = !date;
        if (date)
        {
            GetComponent<Text>().text = Date;
        }
        else
        {
            GetComponent<Text>().text = Version;
        }
    }
}
