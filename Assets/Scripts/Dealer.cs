using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dealer : MonoBehaviour,IVehicleViewChanged
{
    public VehicleView view;
    public Text PriceText;
    List<VehicleInfo> vehicleInfoList;
    // Start is called before the first frame update
    async void OnEnable()
    {
        Resources.LoadAll<VehicleInfo>("ModSystem/Objects");

        var vehicles = new List<Save.Vehicle>();
        vehicleInfoList =new List<VehicleInfo>( Resources.FindObjectsOfTypeAll<VehicleInfo>());
        foreach (var item in vehicleInfoList)
        {
            vehicles.Add(item.GetSaveVehicle());
        }
        view.Vehicles = vehicles;
        await view.SpawnVehicle(0);
    }

    // Update is called once per frame
    public void BuyButton()
    {
        VehicleInfo vehicle = vehicleInfoList[view.SelectedIndex];

        if (SaveSystem.GetCash()>=vehicle.Price)
        {
            Save.Vehicle v = vehicle.GetSaveVehicle();
            SaveSystem.AddVehicle(v);
            SaveSystem.AddCash(-vehicle.Price);
            SaveSystem.AddLog(new Save.LogEntry("Dealer",$"Added car {v.Type}"));
            SaveSystem.SetSelectedVehicleIndex(SaveSystem.GetVehicles().Count-1);
            GarageManager.instance.CloseDealerMenu();
        }
        else
        {
            Debug.Log("git gud fagit");
        }
    }

    public void Changed(int id)
    {
        PriceText.text = $"Price: $ {vehicleInfoList[view.SelectedIndex].Price}";
    }
}
