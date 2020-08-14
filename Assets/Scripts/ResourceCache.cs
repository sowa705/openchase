using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ResourceCache : MonoBehaviour
{
    public class CacheObject
    {
        public string Name;
        public UnityEngine.Object Asset;
        public CacheObject(string name, UnityEngine.Object asset)
        {
            Name = name;
            Asset = asset;
        }
    }

    public static List<CacheObject> cacheObjects;

    void Start()
    {
        if (cacheObjects!=null)
        {
            return;
        }
        
    }

    public static async Task<T> LoadAsync<T>(string path) where T: UnityEngine.Object
    {
        if (cacheObjects == null)
        {
            cacheObjects = new List<CacheObject>();
        }
        
        foreach (var item in cacheObjects)
        {
            if (item.Name==path)
            {
                Debug.Log("Cache hit "+path);
                return (T)item.Asset;
            }
        }
        var asset = await Resources.LoadAsync<T>(path);
        Debug.Log("Cache miss "+path);
        cacheObjects.Add(new CacheObject(path,asset));

        return (T)asset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
