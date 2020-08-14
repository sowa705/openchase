using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ModObject : MonoBehaviour
{
    public string Name;
    public async Task Create(string name)
    {
        Name = name;
        GameObject gm = await ResourceCache.LoadAsync<GameObject>("ModSystem/Objects/"+name);
        Instantiate(gm,transform);
        
    }
    
    public List<string> GetCompatiblePartTypes(string category)
    {
        var list = new List<string>();

        foreach (var item in transform.GetComponentsInChildren<ModSlot>())
        {
            if (item.Category==category)
            {
                if (!list.Contains(item.Type))
                {
                    list.Add(item.Type);
                }
            }
        }
        return list;
    }
    public List<string> GetCompatibleCategories()
    {
        var list = new List<string>();

        foreach (var item in transform.GetComponentsInChildren<ModSlot>())
        {
            if (!list.Contains(item.Category)&&item.Category!="Hidden")
            {
                list.Add(item.Category);
            }
        }
        return list;
    }

    public string GetInstalledPart(string Type)
    {
        foreach (var item in transform.GetComponentsInChildren<ModSlot>())
        {
            if (item.Type == Type)
            {
                if (item.installedPart==null)
                {
                    return "null";
                }
                return item.installedPart.Name;
            }
        }
        return "null";
    }
    public List<string> GetInstalledParts()
    {
        var list = new List<string>();
        foreach (var item in transform.GetComponentsInChildren<ModSlot>())
        {
            if (item.installedPart!=null)
            {
                if (!list.Contains(item.installedPart.Name))
                {
                    list.Add(item.installedPart.Name);
                }
            }
        }
        return list;
    }
    
    public void RemovePart(string Type)
    {
        foreach (var item in transform.GetComponentsInChildren<ModSlot>())
        {
            if (item.Type == Type)
            {
                if (item.installedPart != null)
                {
                    Destroy(item.installedPart.gameObject);
                    if (item.gameObject.GetComponent<IModSystemSlot>() != null)
                    {
                        item.gameObject.GetComponent<IModSystemSlot>().PartNulled(Type);
                    }
                }
            }
        }
    }
    public void AttachPart(string Name)
    {
        ModPart part = ModSystemTools.GetPartN(Name);
        if (part==null)
        {
            return;
        }
        foreach (var item in transform.GetComponentsInChildren<ModSlot>())
        {
            if (item.Type==part.Type)
            {
                if (item.installedPart!=null)
                {
                    Destroy(item.installedPart.gameObject);
                }
                GameObject gm = Instantiate(part.gameObject, item.transform);
                item.installedPart = gm.GetComponent<ModPart>();
                if (item.gameObject.GetComponent<IModSystemSlot>()!=null)
                {
                    item.gameObject.GetComponent<IModSystemSlot>().PartAdded(part.Type,Name);
                }
            }
        }
    }
}
