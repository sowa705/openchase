using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceLights : MonoBehaviour
{
    MeshRenderer mr;
    float timer;
    bool light;
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer>0.3f)
        {
            timer = 0;
            light = !light;

            if (light)
            {
                mr.materials[0].color = new Color(0, 0, 0, 0);
                mr.materials[1].color = new Color(1, 0, 0, 0);
            }
            else
            {
                mr.materials[0].color = new Color(0, 0, 1, 0);
                mr.materials[1].color = new Color(0, 0, 0, 0);
            }
        }
    }
}
