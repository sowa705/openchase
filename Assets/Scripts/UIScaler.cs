using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScaler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (SaveSystem.SaveObject.SettingsData.UIScale)
        {
            case 0:
                GetComponent<UnityEngine.UI.CanvasScaler>().referenceResolution = new Vector2(1000, 600);
                break;
            case 1:
                GetComponent<UnityEngine.UI.CanvasScaler>().referenceResolution = new Vector2(1500, 900);
                break;
            case 2:
                GetComponent<UnityEngine.UI.CanvasScaler>().referenceResolution = new Vector2(2000, 1200);
                break;
            default:
                break;
        }
    }
}
