using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GearIndicator : MonoBehaviour
{
    Text text;
    GameCanvasController c;
    // Start is called before the first frame update
    void Start()
    {
        c = GetComponentInParent<GameCanvasController>();
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (c.player.v.gear>0)
        {
            text.text = $"{c.player.v.gear.ToString()}";
        }
        if (c.player.v.gear==0)
        {
            text.text = "N";
        }
        if (c.player.v.gear == -1)
        {
            text.text = "R";
        }
    }
}
