using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    Text text;
    GameCanvasController c;
    public float RealSpeed;

    Vehicle v;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        c = GetComponentInParent<GameCanvasController>();
        v = c.player.v;
    }

    // Update is called once per frame
    void Update()
    {
        if (v==null)
        {
            v = c.player.v;
            return;
        }
        RealSpeed = v.GetComponentInChildren<Rigidbody>().velocity.magnitude * 3.6f;
        if (v.gear > 0)
        {
            text.text = $"{Mathf.Round(Mathf.Lerp(((v.RPM/ v.GearRatios[v.gear]/4 * 60) * (0.7f*3.141f)/1000),RealSpeed,1f))}";
        }
        if (v.gear == 0)
        {
            text.text = $"<i>{Mathf.Round(RealSpeed)}</i>";
        }
        if (v.gear == -1)
        {
            text.text = $"<i>{Mathf.Round(-v.RPM* v.GearRatios[v.gear]*4*0.7f*3.141f)}</i>";
        }
    }
}
