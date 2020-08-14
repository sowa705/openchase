using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReflections : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (SaveSystem.SaveObject.SettingsData.HQReflections==1&&GetComponentInParent<Player>()!=null)
        {
            SetLayerRecursively(transform.parent.gameObject,9);
        }
        else if (SaveSystem.SaveObject.SettingsData.HQReflections == 2)
        {
            SetLayerRecursively(transform.parent.gameObject, 9);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (obj==null)
        {
            return;
        }

        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            if (child==null)
            {
                continue;
            }
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

}
