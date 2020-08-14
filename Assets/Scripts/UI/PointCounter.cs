using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointCounter : MonoBehaviour
{
    Player p;
    Text text;
    void Start()
    {
        text = GetComponent<Text>();
        p = GameCanvasController.instance.player;
    }

    // Update is called once per frame
    void Update()
    {
        if (p==null)
        {
            p = GameCanvasController.instance.player;
            return;
        }
        text.text =$"{p.Points}";
    }
}
