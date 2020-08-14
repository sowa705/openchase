using Save;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static Save.Save SaveObject;
    public static bool initialized;
    void Awake() {
        //PlayerPrefs.DeleteKey("Save");
        try
        {
            if (initialized)
            {
                Destroy(gameObject);
                return;
            }
            initialized = true;
            DontDestroyOnLoad(gameObject);
            //PlayerPrefs.DeleteKey("Cash");
            //PlayerPrefs.DeleteKey("Save");
            if (!PlayerPrefs.HasKey("Save"))
            {
                if (PlayerPrefs.HasKey("Cash"))
                {
                    SaveObject = Save.Save.FromOldSave();
                    GameLoader.instance.ShowDialog("Save converted to SaveSystem format");
                    SaveData();
                    return;
                }
                SaveObject = new Save.Save();
                SaveObject.VersionLog.Add(new LogEntry("SaveSystem", "Created new save"));
                SaveData();
            }
            else
            {
                PlayerPrefs.SetString("SaveBackup", PlayerPrefs.GetString("Save"));
                SaveObject = JsonUtility.FromJson<Save.Save>(PlayerPrefs.GetString("Save"));
                SaveObject.LoadCheck();
                
            }
        }
        catch(Exception e)
        {
            Debug.Log($"ss, {e.Message} , line {e.StackTrace}");
        }
       
    }

    public static int GetCash()
    {
        return SaveObject.PlayerData.Cash;
    }
    public static void SetCash(int amount)
    {
        SaveObject.PlayerData.Cash=amount;
        SaveData();
    }
    public static void AddCash(int amount)
    {
        SaveObject.PlayerData.Cash += amount;
        SaveData();
    }
    public static void AddLog(LogEntry entry)
    {
        if (SaveObject.SaveLog.Count>200)
        {
            SaveObject.SaveLog.RemoveAt(0);
        }
        SaveObject.SaveLog.Add(entry);
    }
    public static List<Save.Vehicle> GetVehicles()
    {
        return SaveObject.GarageData.Vehicles;
    }
    public static void AddVehicle(Save.Vehicle vehicle)
    {
        SaveObject.GarageData.Vehicles.Add(vehicle);
        if (SaveObject.GarageData.VehicleTypes.FirstOrDefault(x => x.Type == vehicle.Type)==null)
        {
            VehicleType type = new VehicleType();
            type.Type = vehicle.Type;
            type.OwnedParts = vehicle.InstalledParts;
            SaveObject.GarageData.VehicleTypes.Add(type);
        }
        SaveData();
    }
    public static void RemoveVehicle(int index)
    {
        SaveObject.GarageData.Vehicles.RemoveAt(index);
        SaveData();
    }
    public static List<string> GetOwnedPartsForVehicleType(string type)
    {
        var vType = SaveObject.GarageData.VehicleTypes.First(x => x.Type == type);
        return vType.OwnedParts;
    }
    public static void UpdateVehicleParts(List<string> Parts, int index)
    {
        SaveObject.GarageData.Vehicles[index].InstalledParts = Parts;

        var vtype = SaveObject.GarageData.Vehicles[index].Type;
        var typelist = SaveObject.GarageData.VehicleTypes.First(x=>x.Type==vtype).OwnedParts;
        

        foreach (var item in Parts)
        {
            if (!typelist.Contains(item))
            {
                typelist.Add(item);
            }
        }
        SaveData();
    }

    public static int GetSelectedVehicleIndex()
    {
        return SaveObject.GarageData.SelectedVehicle;
    }
    public static void SetSelectedVehicleIndex(int index)
    {
        SaveObject.GarageData.SelectedVehicle=index;
        SaveData();
    }

    public static void SaveData()
    {
        string data = JsonUtility.ToJson(SaveObject);
        //Debug.Log(data);
        PlayerPrefs.SetString("Save",data);
    }

    public static int GetHighscore(bool hardMode)
    {
        if (hardMode)
        {
            return SaveObject.PlayerData.HighscoreHard;
        }
        else
        {
            return SaveObject.PlayerData.HighscoreNormal;
        }
    }
    public static void SetHighscore(int value,bool hardMode)
    {
        if (hardMode)
        {
            SaveObject.PlayerData.HighscoreHard=value;
        }
        else
        {
            SaveObject.PlayerData.HighscoreNormal=value;
        }
        SaveData();
    }
}
namespace Save
{
    [Serializable]
    public class Save
    {
        public int Version;
        public string PlayerID;
        public PlayerData PlayerData;
        public GarageData GarageData;
        public SettingsData SettingsData;
        public List<LogEntry> VersionLog = new List<LogEntry>();
        public List<LogEntry> SaveLog = new List<LogEntry>();
        public Save()
        {
            Version = 1;
            PlayerID = GameLoader.GetUniqueKey(8);
            PlayerData = new PlayerData();
            GarageData = new GarageData();
            SettingsData = new SettingsData();
        }
        public void LoadCheck()
        {
            if (SettingsData==null)
            {
                SettingsData = new SettingsData();
            }
            Resources.LoadAll<VehicleInfo>("ModSystem/Objects");

            var vehicleInfoList = new List<VehicleInfo>(Resources.FindObjectsOfTypeAll<VehicleInfo>());
            foreach (var item in GarageData.VehicleTypes)
            {
                var v = vehicleInfoList.FirstOrDefault(x=>x.name==item.Type+"I");
                var list = new List<string>();
                foreach (var part in v.DefaultParts)
                {
                    if (!item.OwnedParts.Contains(part))
                    {
                        item.OwnedParts.Add(part);
                        list.Add(part);
                    }
                }
                foreach (var car in GarageData.Vehicles)
                {
                    if (car.Type==item.Type)
                    {
                        foreach (var p in list)
                        {
                            car.InstalledParts.Add(p);
                        }
                    }
                }
            }
        }
        public static Save FromOldSave()
        {
            Save save = new Save();
            save.PlayerID = PlayerPrefs.GetString("PlayerID",save.PlayerID);
            save.PlayerData.Cash = PlayerPrefs.GetInt("Cash");
            save.VersionLog.Add(new LogEntry("SaveSystem","Converted from old format to SaveSystem"));
            Modifications oldmods = JsonUtility.FromJson<Modifications>(PlayerPrefs.GetString("Modifications2"));
            int addedcash = 0;
            try
            {
                foreach (var item in oldmods.OwnedEngines)
                {
                    addedcash += oldmods.GetPrice(item);
                }
                foreach (var item in oldmods.OwnedFrontSpoilers)
                {
                    addedcash += oldmods.GetPrice(item);
                }
                foreach (var item in oldmods.OwnedPaintColors)
                {
                    addedcash += oldmods.GetPrice(item);
                }
                foreach (var item in oldmods.OwnedPaintTypes)
                {
                    addedcash += oldmods.GetPrice(item);
                }
                foreach (var item in oldmods.OwnedSpoilers)
                {
                    addedcash += oldmods.GetPrice(item);
                }
                foreach (var item in oldmods.OwnedStickers)
                {
                    addedcash += oldmods.GetPrice(item);
                }
            }
            catch
            {

            }
            save.PlayerData.Cash += (int)(0.75f * addedcash);
            save.PlayerData.HighscoreNormal = PlayerPrefs.GetInt("Highscore");
            Debug.Log($"Data restored: cash:{save.PlayerData.Cash}, highscore:{save.PlayerData.HighscoreNormal}");
            return save;
        }
    }
    [Serializable]
    public class PlayerData
    {
        public int Cash = 2000;
        public int HighscoreNormal;
        public int HighscoreHard;
        public bool HardModeSelected;
    }
    [Serializable]
    public class SettingsData
    {
        public bool EnableAudio = true;
        public int HQReflections = 0;
        public int ResolutionScaling = 0;
        public int MotionBlur = 0;
        public int AmbientOcclusion = 0;
        public int ChromaticAberration = 0;
        public int LensBlur = 0;
        public int ShadowQuality=0;
        public int AntiAliasing = 0;
        public int UIScale = 0;
        public bool ShowFPS = true; 
    }
    [Serializable]
    public class LogEntry
    {
        public long Time;
        public string Subsystem;
        public string Message;

        public LogEntry(string subsystem, string message)
        {
            Time = DateTime.Now.ToFileTimeUtc();
            Subsystem = subsystem;
            Message = message;
            Debug.Log($"[{subsystem}] {message}");
        }
    }
    [Serializable]
    public class GarageData
    {
        public List<VehicleType> VehicleTypes = new List<VehicleType>();
        public List<Vehicle> Vehicles = new List<Vehicle>();
        public int SelectedVehicle=0;
        public GarageData()
        {
            Vehicle v1 = new Vehicle();
            v1.Type = "muscle";
            v1.InstalledParts = new List<string> { "Wheel", "PaintBlue", "StickerBlack", "NormalHeight","36v6" };
            Vehicles.Add(v1);

            VehicleType type = new VehicleType();
            type.Type = "muscle";
            type.OwnedParts = new List<string> { "Wheel", "PaintBlue",  "StickerBlack", "NormalHeight", "36v6" };
            VehicleTypes.Add(type);
        }
    }
    [Serializable]
    public class Vehicle
    {
        public string Type;
        public List<string> InstalledParts = new List<string>();
        public int Mileage;
    }
    [Serializable]
    public class VehicleType
    {
        public string Type;
        public List<string> OwnedParts = new List<string>();
    }
}