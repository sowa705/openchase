using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageManager : MonoBehaviour,IVehicleViewChanged
{
    public VehicleView VehicleView;
    public ModEditor Editor;
    public GarageState state;
    public Garage GarageView;
    public Dealer DealerView;

    public static GarageManager instance;
    bool ginitialized=true;
    int ScheduledJob;
    async void Start()
    {
        instance = this;
        VehicleView.CallList.Add(this);
        VehicleView.CallList.Add(DealerView);
        VehicleView.CallList.Add(GarageView);
        VehicleView.Vehicles = SaveSystem.GetVehicles();
        VehicleView.SelectedIndex = SaveSystem.GetSelectedVehicleIndex();
        await VehicleView.SpawnVehicle(SaveSystem.GetSelectedVehicleIndex());
        DealerView.view = VehicleView;
    }
    async void Update()
    {
        ScheduledJob -= 1;
        if (ScheduledJob==0)
        {
            switch (state)
            {
                case GarageState.Editor:
                    await Editor.Reload(VehicleView.InitializedObject);
                    GarageView.gameObject.SetActive(false);
                    VehicleView.gameObject.SetActive(false);
                    break;
                case GarageState.Garage:
                    VehicleView.Vehicles = SaveSystem.GetVehicles();
                    await VehicleView.SpawnVehicle(SaveSystem.GetSelectedVehicleIndex());
                    VehicleView.SelectedIndex = SaveSystem.GetSelectedVehicleIndex();
                    ginitialized = true;
                    break;
                case GarageState.Dealer:
                    break;
                default:
                    break;
            }
        }
    }
    // Update is called once per frame
    public void EditVehicle()
    {
        GarageRotater.FOV = 47;
        state = GarageState.Editor;
        Editor.gameObject.SetActive(true);
        ScheduledJob =5;
    }
    public void SaveEditedVehicle()
    {
        GarageRotater.FOV = 60;
        SaveSystem.UpdateVehicleParts(Editor.target.GetInstalledParts(), SaveSystem.GetSelectedVehicleIndex());
        SaveSystem.AddLog(new Save.LogEntry("GarageManager", $"Updated vehicle parts {Editor.target.Name}"));
        state = GarageState.Garage;
        Editor.gameObject.SetActive(false);
        GarageView.gameObject.SetActive(true);
        VehicleView.gameObject.SetActive(true);
        ScheduledJob =5;
    }
    public void ExitEditor()
    {
        GarageRotater.FOV = 60;
        state = GarageState.Garage;
        Editor.gameObject.SetActive(false);
        GarageView.gameObject.SetActive(true);
        VehicleView.gameObject.SetActive(true);
        ScheduledJob = 5;
    }
    public void SaveEditedVehicleWithoutExiting()
    {
        SaveSystem.UpdateVehicleParts(Editor.target.GetInstalledParts(), SaveSystem.GetSelectedVehicleIndex());
        SaveSystem.AddLog(new Save.LogEntry("GarageManager", $"Updated vehicle parts {Editor.target.Name} without exiting"));
    }
    public void OpenDealerMenu()
    {
        ginitialized = false;
        state = GarageState.Dealer;
        GarageView.gameObject.SetActive(false);
        DealerView.gameObject.SetActive(true);
        ScheduledJob = 5;
    }
    public void CloseDealerMenu()
    {
        state = GarageState.Garage;
        GarageView.gameObject.SetActive(true);
        DealerView.gameObject.SetActive(false);
        ScheduledJob = 5;
    }

    public void Changed(int id)
    {
        if (ginitialized)
        {
            SaveSystem.SetSelectedVehicleIndex(id);
        }
    }
}
public enum GarageState
{
    Editor,
    Garage,
    Dealer
}