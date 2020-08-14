using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspensionHeight : MonoBehaviour,IModSystemSlot
{
    public void PartAdded(string Type, string Part)
    {
        try
        {
            if (Type == "SuspensionHeight")
            {
                float height = 0.20f;
                switch (Part)
                {
                    case "HighHeight":
                        height = 0.30f;
                        break;
                    case "LowHeight":
                        height = 0.14f;
                        break;
                    default:
                        break;
                }

                foreach (var item in GetComponentsInChildren<WheelCollider>())
                {
                    item.suspensionDistance = height;
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        }
        
    }

    public void PartNulled(string Type)
    {
    }
}
