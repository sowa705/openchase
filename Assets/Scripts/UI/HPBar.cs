using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Camera camera;
    public Vehicle vehicle;
    public Image img;
    public Image ring;

    public GameObject AirTime;
    public Text AirTimeText;

    public bool player;
    float smoothcaught;
    void Start()
    {
        //img = GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            if (vehicle == null)
            {
                vehicle =GameCanvasController.instance.player.v;
                return;
            }
        }
        else
        {
            if (vehicle == null)
            {
                Destroy(gameObject);
                return;
            }
            if (vehicle.HP < 0)
            {
                Destroy(gameObject);
            }
        }

        if (player)
        {
            var caught = vehicle.GetComponent<Player>().Caught;
            smoothcaught = Mathf.Lerp(smoothcaught,caught,Time.deltaTime*10f);
            if (smoothcaught>0.1f)
            {
                ring.fillAmount = 1 - smoothcaught;
                ring.color = Color.white;
                if (caught > 0.8f)
                {
                    ring.color = Color.red;
                }
            }
            else
            {
                ring.fillAmount = 0;
            }
            if (vehicle.AirTime>5)
            {
                AirTime.SetActive(true);
                AirTimeText.text = $"Air time {(int)vehicle.AirTime}";
            }
            else
            {
                AirTime.SetActive(false);
            }
        }
        
        img.fillAmount = vehicle.HP / vehicle.MaxHP;
        GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 0.8f);
        if (vehicle.HP/vehicle.MaxHP<0.2f)
        {
            img.color = Color.red;
        }
        
        else
        {
            img.color = Color.white;
        }
        if (vehicle.MaxHP - vehicle.HP < 0.1f)
        {
            img.fillAmount = 0;
            GetComponent<Image>().color = new Color(0,0,0,0);
        }
        if (Vector3.Dot( (vehicle.transform.GetChild(0).position- camera.transform.position),camera.transform.forward)<0)
        {
            
            img.fillAmount = 0;
            GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }
        //now you can set the position of the ui element
        GetComponent<RectTransform>().position = camera.WorldToScreenPoint(vehicle.transform.GetChild(0).position)+transform.up*30;
    }
}
