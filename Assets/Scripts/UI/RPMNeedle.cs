using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RPMNeedle : MonoBehaviour
{
    public GameCanvasController c;

    public float MaxRPM=7000;
    public float MinAngle;
    public float MaxAngle;

    public bool invert;

    public float ChangeRate;
    float oldrpm;

    public Image[] BlurP;
    public Image[] BlurN;

    public float BlurVal;
    void Start()
    {
        c = GetComponentInParent<GameCanvasController>();
        oldrpm = c.player.v.RPM;
    }

    // Update is called once per frame
    void Update()
    {
        float rotrange = MaxAngle - MinAngle;
        float rpm = (c.player.v.RPM / MaxRPM);

        float changeRate = (Mathf.Clamp(c.player.v.RPM,0,7000)-oldrpm)/Time.deltaTime;
        oldrpm = Mathf.Clamp(c.player.v.RPM, 0, 7000);

        ChangeRate = Mathf.Lerp(ChangeRate,changeRate,Time.deltaTime*10f);

        if (invert)
        {
            rpm=1 - rpm;
        }
        rpm = Mathf.Clamp01(rpm);
        transform.rotation = Quaternion.Euler(0,0,rpm*rotrange+MinAngle);

        float blurval = 0;
        if (Mathf.Abs(ChangeRate) >1600)
        {
            blurval = (Mathf.Abs(ChangeRate) - 1600)/8000;
        }
        blurval = Mathf.Abs(blurval);
        for (int i = 0; i < BlurP.Length; i++)
        {
            BlurP[i].rectTransform.localRotation = Quaternion.Euler(0,0, blurval*(i+1/(float)BlurP.Length));
            BlurN[i].rectTransform.localRotation = Quaternion.Euler(0, 0, -blurval * (i+1 / (float)BlurP.Length) );
            Color c = new Color(1,1,1, (1-(i / (float)BlurP.Length))/(blurval*16));
            if (rpm < 0.25f)
            {
                c = new Color(1, 0, 0, (1 - (i / (float)BlurP.Length)) / (blurval * 16));
            }
            BlurP[i].color = c;
            BlurN[i].color = c;
        }
        
        GetComponent<Image>().color = new Color(1, 1, 1, 1 / (blurval *48));
        if (rpm < 0.25f)
        {
            GetComponent<Image>().color = new Color(1, 0, 0, 1 / (blurval * 48));
        }
    }
}
