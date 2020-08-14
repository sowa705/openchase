using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupIndicator : MonoBehaviour
{
    Text text;
    GameCanvasController c;
    public Image img;
    public Image img2;
    public Sprite[] powerups;
    // Start is called before the first frame update
    void Start()
    {
        c = GetComponentInParent<GameCanvasController>();
        text = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Image>().fillAmount = c.player.GetComponent<Vehicle>().powerupduration / 15f;
        img.color = new Color(1,1,1,1);
        img2.color = new Color(0, 0, 0, 0.5f);
        text.text="";
        switch (c.player.GetComponent<Vehicle>().poweruptype)
        {
            case Powerup.PowerupType.None:
                img.color = new Color(0,0,0,0);
                img2.color = new Color(0, 0, 0, 0);
                break;
            case Powerup.PowerupType.HPRegen:
                img.sprite = powerups[0];
                text.text = "HP Regeneration";
                break;
            case Powerup.PowerupType.QuadPower:
                img.sprite = powerups[1];
                text.text = "Quad power";
                break;
            case Powerup.PowerupType.Tank:
                img.sprite = powerups[2];
                text.text = "Tank";
                break;
            default:
                break;
        }
    }
}
