using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WantedLevelIndicator : MonoBehaviour
{
    public float timer;
    int newstar;
    int oldlvl;
    Text text;
    void Start()
    {
        text = GetComponent<Text>();
        text.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer>0.5)
        {
            timer = 0;

            int wantedlvl = GameManager.gm.player.WantedLevel;
            if (wantedlvl!=oldlvl)
            {
                newstar = 6;
            }
            
            if (newstar%2==0||newstar<0)
            {
                text.text = "★★★★★★".Substring(0, wantedlvl);
            }
            else
            {
                text.text = "";
            }
            newstar--;
            oldlvl = wantedlvl;
        }
    }
}
