using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class VehicleView : MonoBehaviour
{
    GameObject preview;
    public List<Save.Vehicle> Vehicles;
    public List<IVehicleViewChanged> CallList = new List<IVehicleViewChanged>();
    public ModObject InitializedObject;
    public Text VCount;
    public Text Name;
    public Image Health;
    public Image Health2;
    public Image Handling;
    public Image Handling2;
    public Image Power;
    public Image Power2;
    public int SelectedIndex;

    public List<VehicleInfo> VTypes;

    void Start()
    {
        Resources.LoadAll<VehicleInfo>("ModSystem/Objects");
        ModSystemTools.LoadParts();
        var vehicles = new List<Save.Vehicle>();
        VTypes = new List<VehicleInfo>(Resources.FindObjectsOfTypeAll<VehicleInfo>());
    }
    public async void ScrollButton(bool add)
    {
        if (add)
        {
            SelectedIndex++;
            if (SelectedIndex>=Vehicles.Count)
            {
                SelectedIndex = 0;
            }
        }
        else
        {
            SelectedIndex--;
            if (SelectedIndex < 0)
            {
                SelectedIndex = Vehicles.Count-1;
            }
        }
        await SpawnVehicle(SelectedIndex);
    }
    public async Task SpawnVehicle(int index)
    {
        if (preview!=null)
        {
            Destroy(preview);
        }
        VCount.text = $"{index+1}/{Vehicles.Count}";
        VehicleInfo info = VTypes.FirstOrDefault(x => x.name == Vehicles[index].Type + "I");
        Handling.fillAmount = info.Handling/100f;
        Health.fillAmount = info.HP/180f;
        Power.fillAmount = info.Power/400f;
        

        Name.text = TranslationSystem.GetString("CAR_" + info.name.Substring(0,info.name.Length-1));

        var v = Vehicles[index];

        GameObject gm = new GameObject();
        gm.transform.position = new Vector3(0,1,0);
        preview = gm;
        var modobj = gm.AddComponent<ModObject>();
        await modobj.Create(v.Type);
        foreach (var item in v.InstalledParts)
        {
            modobj.AttachPart(item);
        }
        InitializedObject = modobj;
        //await Task.Delay(100);
        Power2.fillAmount = modobj.GetComponentInChildren<EngineManager>().GetPower()/400f;
        Handling2.fillAmount = modobj.GetComponentInChildren<HandlingManager>().GetHandling() / 100f;
        Health2.fillAmount = modobj.GetComponentInChildren<HealthManager>().GetHealth() / 180f;

        foreach (var item in CallList)
        {
            try {
                item.Changed(index);
            }
            catch { }
            
        }
    }
}
public interface IVehicleViewChanged
{
    void Changed(int id);
}