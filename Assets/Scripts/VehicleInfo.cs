using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/VehicleInfo", order = 1)]
public class VehicleInfo : ScriptableObject
{
    public int Price;
    public List<string> DefaultParts;
    public float HP;
    public float Power;
    public float Handling;
    public int GearCount=4;
    public float FinalDriveRatio;
    public bool Diesel;
    public DriveType DriveType;
    public Save.Vehicle GetSaveVehicle()
    {
        Save.Vehicle v = new Save.Vehicle();
        v.InstalledParts = new List<string>(DefaultParts.ToArray()); //dont ask why
        v.Type = name.Substring(0,name.Length-1);
        return v;
    }
}
public enum DriveType
{
    RWD,
    FWD,
    AWD
}