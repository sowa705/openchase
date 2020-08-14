using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Garage : MonoBehaviour,IVehicleViewChanged
{
    public Text SellPrice;
    public GameObject SellConfirmation;
    int ids;
    void Start()
    {
        
    }
    public void OpenScreen()
    {
        if (SaveSystem.GetVehicles().Count > 1)
        {
            SellPrice.text = $"Sell {SaveSystem.GetVehicles()[ids].Type} ($ {CalculatePrice(SaveSystem.GetVehicles()[ids])})";

            SellConfirmation.SetActive(true);
        }
    }
    // Update is called once per frame
    public void Sell()
    {
        if (SaveSystem.GetVehicles().Count>1)
        {
            SaveSystem.AddCash(CalculatePrice(SaveSystem.GetVehicles()[ids]));
            SaveSystem.RemoveVehicle(ids);
            SellConfirmation.SetActive(false);
        }
    }
    int CalculatePrice(Save.Vehicle vehicle)
    {
        Resources.LoadAll<VehicleInfo>("ModSystem/Objects");

        var vehicleInfoList = new List<VehicleInfo>(Resources.FindObjectsOfTypeAll<VehicleInfo>());
        return (int)(vehicleInfoList.First(x => x.name == vehicle.Type + "I").Price * 0.75f);
    }

    public void Changed(int id)
    {
        ids = id;
        
    }
}
