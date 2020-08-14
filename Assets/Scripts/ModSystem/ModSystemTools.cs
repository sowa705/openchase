using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class ModSystemTools
{
    class ModPartCache
    {
        public string Name;
        public ModPart Part;
        public ModPartCache(string name,ModPart part)
        {
            Name = name;
            Part = part;
        }
    }
    static List<ModPartCache> partCache;
    public static ModPart GetPart(string Name)
    {
        foreach (var item in Resources.FindObjectsOfTypeAll<ModPart>())
        {
            if (item.Name == Name)
            {
                return item;
            }
        }
        //throw new System.Exception("Part not found: "+Name);
        return null;
    }
    public static ModPart GetPartN(string name)
    {
        if (partCache==null)
        {
            Debug.Log("PartCache building");
            partCache = new List<ModPartCache>();
            LoadParts();
            foreach (var item in Resources.FindObjectsOfTypeAll<ModPart>())
            {
                partCache.Add(new ModPartCache(item.Name,item));
            }

        }
        foreach (var item in partCache)
        {
            if (item.Name == name)
            {
                //Debug.Log("PartCache hit");
                return item.Part;
            }
        }
        //throw new System.Exception("Part not found: "+Name);
        return null;
    }
    public static List<string> GetCompatibleParts(string Name, string Type)
    {
        var list = new List<string>();
        list.Add("null");
        foreach (var item in Resources.FindObjectsOfTypeAll<ModPart>())
        {
            if (item.Type == Type && item.Compatibility.Contains(Name) && !list.Contains(item.Name)&&!item.Hidden)
            {
                list.Add(item.Name);
            }
        }
        return list;
    }
    public static void LoadParts()
    {
        Resources.LoadAll<ModPart>("ModSystem/Parts");
    }
    public static async Task LoadPartsAsync()
    {
        foreach (var item in ResourceList.GetAllAssetsAtPath("ModSystem/Parts"))
        {
            await Resources.LoadAsync<ModPart>(item);
        }
    }
    public static bool IsTypeNullable(string category)
    {
        switch (category)
        {
            case "Wheels":
                return false;
            case "BodyPaint":
                return false;
            case "StickerPaint":
                return false;
            case "SuspensionHeight":
                return false;
            case "Engine":
                return false;
            case "Drivetrain":
                return false;
            default:
                break;
        }

        return true;
    }
}