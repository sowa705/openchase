using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class VehicleStatView : MonoBehaviour
{
    public Image Power;
    public Image Power2;
    public Image Handling;
    public Image Handling2;
    public Image Health;
    public Image Health2;
    List<VehicleInfo> VTypes;
    void Start()
    {
        VTypes = new List<VehicleInfo>(Resources.FindObjectsOfTypeAll<VehicleInfo>());
    }

    // Update is called once per frame
    public void ShowValues(ModObject target)
    {
        VehicleInfo info = VTypes.FirstOrDefault(x => x.name == target.Name + "I");
        Handling.fillAmount = info.Handling / 100f;
        Health.fillAmount = info.HP / 180f;
        Power.fillAmount = info.Power / 400f;
        Power2.fillAmount = target.GetComponentInChildren<EngineManager>().GetPower() / 400f;
        Handling2.fillAmount = target.GetComponentInChildren<HandlingManager>().GetHandling() / 100f;
        Health2.fillAmount= target.GetComponentInChildren<HealthManager>().GetHealth() / 180f;
    }
}
