using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyAdder : MonoBehaviour
{
    int clicks;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Click);
    }

    // Update is called once per frame
    void Click()
    {
        clicks++;
        if (clicks>20&& HardMode.Mode&&GameManager.gm.player.GetComponent<ModObject>().GetInstalledParts().Contains("PaintYellow"))
        {
            clicks = 0;
            SaveSystem.AddCash(100000);
        }
    }
}
