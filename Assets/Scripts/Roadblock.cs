using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roadblock : MonoBehaviour
{
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        if (Random.value>0.4f)
        {
            return;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            if (Random.value>0.5f)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
