using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslationSystem : MonoBehaviour
{
    public static Translation translation;
    static string selectedlang="EN";
    static bool initialized;
    void Start()
    {
        if (initialized)
        {
            Destroy(gameObject);
            return;
        }
        initialized = true;
        DontDestroyOnLoad(this);
        translation = Resources.Load<Translation>("Translations/"+selectedlang);
    }

    // Update is called once per frame
    public static string GetString(string key)
    {
        //Debug.Log($"Request {key}");
        try
        {
            return translation.Keys[key];
        }
        catch
        {
            return "#"+key;
        }
    }
}
